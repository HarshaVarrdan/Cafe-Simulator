using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;


[RequireComponent(typeof(CharacterController))]
public class Player_Controller : MonoBehaviour
{ 
    
    [SerializeField] Player_Inventory PI;
    [SerializeField] Button_Manager BM;
    [SerializeField] Actions act;

    //[SerializeField] GameObject UI;
    [SerializeField] GameObject refUI;


    [HideInInspector] public CharacterController mCharacterController;
    //[HideInInspector] public Animator mAnimator;

    public GameObject Joystick;
    public FixedJoystick mJoystick;
    
    public Item selected;

    public bool plantable;
    public bool placeable;
    public bool useable;
    public bool addable;

    
    public float holdingTime = 10f;
    public bool toHold = true;

    public bool Player_Crouch;
    public bool Player_Shoot;
    public float mWalkSpeed = 1.5f;
    public float mRunSpeed = 3.0f;
    public float mRotationSpeed = 50.0f;
    public float mGravity = -30.0f;
    public string Enemy_PV_ID;
    public int Received_PV;

    [Tooltip("Only useful with Follow and Independent Rotation - third - person camera control")]
    public bool mFollowCameraForward = false;
    public float mTurnRate = 200.0f;
    
    private Vector3 mVelocity = new Vector3(0.0f, 0.0f, 0.0f);

        
    Touch previousTouch; 


    // Start is called before the first frame update
    void Start()
    {
        mCharacterController = GetComponent<CharacterController>();
        //mAnimator = GetComponent<Animator>();
        PI = GetComponent<Player_Inventory>();
        refUI = GameObject.FindGameObjectWithTag("Ref_UI");
        BM = GameObject.FindWithTag("BM").GetComponent<Button_Manager>();
        refUI.SetActive(false);
        if(Joystick == null)
        {
            Joystick = GameObject.FindGameObjectWithTag("Joystick");
            mJoystick = GameObject.FindGameObjectWithTag("Joystick").GetComponent<FixedJoystick>();
        }
    }

    void Update()
    {
        Move();
        //Raycast();
        if(Input.touchCount > 0  && !EventSystem.current.IsPointerOverGameObject(Input.GetTouch(0).fingerId))
        {
            TouchRay();
            if(previousTouch.phase != Input.GetTouch(0).phase && toHold)
            {
                holdRay();
                
            }
        }
    }

    void Move()
    {   

        float Horizontal = mJoystick.Horizontal;
        float Vertical = mJoystick.Vertical;
        float speed = mRunSpeed;
        

        if (mFollowCameraForward)
        {
            // Only allow aligning of player's direction when there is a movement.
            //if (Vertical > 0.1 || Vertical < -0.1 || Horizontal > 0.1 || Horizontal < -0.1){
            
                // rotate player towards the camera forward.
                Vector3 eu = Camera.main.transform.rotation.eulerAngles;
                transform.rotation = Quaternion.RotateTowards( transform.rotation , Quaternion.Euler(0.0f, eu.y, 0.0f) , mTurnRate * Time.deltaTime );
            //}
        }
        else
        {
            transform.Rotate(0.0f, Horizontal * mRotationSpeed * Time.deltaTime, 0.0f);
        }

        mCharacterController.Move(transform.forward * Vertical * speed * Time.deltaTime);

        // Move forward / backward
        Vector3 forward = transform.TransformDirection(Vector3.forward).normalized;
        forward.y = 0.0f;
        Vector3 right = transform.TransformDirection(Vector3.right).normalized;
        right.y = 0.0f;
#region Animation_Control
/*        mAnimator.SetFloat("Horizontal", Horizontal * speed / mRunSpeed);
        mAnimator.SetFloat("Vertical", Vertical * speed / mRunSpeed);
        if (mAnimator != null)
        {
            /*if (mFollowCameraForward)
            {
                mCharacterController.Move(forward * Vertical * speed * Time.deltaTime + right * Horizontal * speed * Time.deltaTime);
               
            }
            else
            {
                //mCharacterController.Move(forward * Vertical * speed * Time.deltaTime);
                mAnimator.SetFloat("Horizontal", 0);
                mAnimator.SetFloat("Vertical", Vertical * speed / mRunSpeed);
            }
            if(mAnimator.GetFloat("Horizontal") == 0.0 && mAnimator.GetFloat("Vertical") == 0.0)
            {
                mAnimator.SetBool("Idle", true);
            }
            else
            {
                mAnimator.SetBool("Idle", false);
            }

            if (Player_Crouch)
            {
                mAnimator.SetBool("Crouch", true);
            }
            else
            {
                mAnimator.SetBool("Crouch", false);
            }

            if(Player_Shoot)
            {
                mAnimator.SetBool("Shoot", true);
            }
            else
            {
                mAnimator.SetBool("Shoot", false);
            } 
            
        */
#endregion 
        
        // apply gravity.
        mVelocity.y += mGravity * Time.deltaTime;
        mCharacterController.Move(mVelocity * Time.deltaTime);

        if (mCharacterController.isGrounded && mVelocity.y < 0)
        {
            mVelocity.y = 0f;
        }
        else return;
    }

    public void SelectedItem(Item sel)
    {
        selected = sel;
        if(selected != null)
        {
            Debug.Log($"{selected.Item_Title} is Selected");
        }
    }

    public void Raycast()
    {
        Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);
        RaycastHit hit;
        
        if(Physics.Raycast(ray, out hit,20f))
        {
            var Object_Hit = hit.collider.gameObject.name;
            Debug.Log("Raycast Hit : " + Object_Hit);
            PickUpObj PUO = hit.collider.gameObject.GetComponent<PickUpObj>();

            if(PUO != null)
            {
                Debug.Log("PickUpB Hit");
                PUO.pickUp();
            }
            /*if(BM.Trigger)
            {
                Debug.Log("Triggered");
                BM.index_RT += 1;
                BM.Trigger = false;
                if(Object_Hit == "Burner_R_F")
                {
                    Debug.Log("On Tea Maker");
                    act.Call_Actions("Tea Maker");
                }
                else if(Object_Hit == "Plantation_Base")
                {
                    if(plantable)
                    {
                        Plantation_Base PB = hit.collider.gameObject.GetComponent<Plantation_Base>();
                        Debug.Log("Plantation Base Hit");
                        PB.Plantitem(selected);
                    }
                    else Debug.Log("Not Plantable");
                }
            }
            else 
            {
                Debug.Log(Object_Hit + " Has been hit.");
            }*/
            
        }        
    }

    public void TouchRay()
    {
        if (Input.GetTouch(0).phase == TouchPhase.Ended)
        {
            if(holdingTime > 0)
            {
                Ray raycast = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
                RaycastHit raycastHit;
                if (Physics.Raycast(raycast, out raycastHit))
                {
                    Debug.Log(raycastHit.collider.gameObject.name);
                    if (raycastHit.collider.name == "Refrigerator" && !refUI.activeSelf)
                    {
                        refUI.SetActive(true);
                    }
                    if(selected != null)
                    {
                        if(selected.isPlantable && raycastHit.collider.name == "Plantation_Base")
                        {
                            Plantation_Base PB = raycastHit.collider.gameObject.GetComponent<Plantation_Base>();   
                            Debug.Log("Planting : " + selected.Item_Title);
                            PB.Plantitem(selected);
                            SelectedItem(null);
                        }
                        else if(selected.isAddable && raycastHit.collider.tag == "AI_Utensils")
                        {
                            AI_Utensils AIU = raycastHit.collider.gameObject.GetComponent<AI_Utensils>();
                            AIU.addItem(selected);
                            PI.Remove_Item(1,selected);
                        }
                        else Debug.Log("Nothing");
                    }
                    if(raycastHit.collider.name == "Burner_R_F")
                    {
                        Debug.Log("On Tea Maker");
                        act.Call_Actions("Tea Maker");
                    }
                    holdingTime = 2f;
               }
            }
        }
    }

    void holdRay()
    {
        if(Input.GetTouch(0).phase == TouchPhase.Stationary)
        {
            Debug.Log("Total : " + holdingTime);
            float remainingDuration = holdingTime -= Time.deltaTime;
            if(holdingTime <=0)
            {
                //previousTouch = Input.GetTouch(0);
                Ray raycast = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
                RaycastHit raycastHit;
                if (Physics.Raycast(raycast, out raycastHit))
                {
                    if(raycastHit.collider.tag == "AI_Utensils")
                    {
                        Debug.Log("You are Holding at AI_Utensils");
                        AI_Utensils AIM = raycastHit.collider.gameObject.GetComponent<AI_Utensils>();
                        AIM.onHoldObject();
                        //toHold =false;
                    }
                    else if(raycastHit.collider.tag == "PB")
                    {
                        Debug.Log("You are Holding at PB");
                        Plantation_Base pBase = raycastHit.collider.gameObject.GetComponent<Plantation_Base>();
                        pBase.onHoldObject();
                        //toHold =false;
                    }
                }
                holdingTime = 2f;
            }
        }
    }
}
