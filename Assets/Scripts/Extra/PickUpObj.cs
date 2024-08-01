using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpObj : MonoBehaviour
{
    GameObject PickUpArea;

    bool Picked = false;

    // Start is called before the first frame update
    void Start()
    {
        PickUpArea = GameObject.FindWithTag("PickupArea");        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void pickUp()
    {
        if(!Picked)
        {
            if(this.transform.parent != null)
            {
                this.transform.parent.transform.position = PickUpArea.transform.position;
                this.transform.parent.transform.SetParent(PickUpArea.transform);
                //this.transform = PickUpArea.transform;
                Picked = true; 
            }
        }
        else 
        {
            this.transform.parent = null;
            Picked = false;
        }

    }
}
