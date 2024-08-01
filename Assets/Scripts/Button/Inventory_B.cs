using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Inventory_B : MonoBehaviour
{
#region Variables
    
    public static Item itemSelected;
    
    //[SerializeField] Button thisButton;
    //[SerializeField] Button R_Trigger_B;
    [SerializeField] Sprite imageSprite;

    [SerializeField] Item_Database ID;
    [SerializeField] Player_Controller PC;

    public static bool plantableItem = false;
    public static bool placeableItem = false;
    public static bool useableItem = false;

#endregion


    void Start()
    {   
        //R_Trigger_B = GameObject.Find("RaycastTrigger_B").GetComponent<Button>();
        ID = GameObject.FindWithTag("ID_S").GetComponent<Item_Database>();
        PC = GameObject.FindWithTag("Player").GetComponent<Player_Controller>();
    }

    public void onClick()
    {
        imageSprite = transform.parent.gameObject.transform.GetChild(0).GetComponent<Image>().sprite;
        if (imageSprite != null)
        {
            Debug.Log(imageSprite.name);
        
            itemSelected = ID.items.Find(x => x.Item_Icon == imageSprite);
            /*Debug.Log(itemSelected.Item_Title);
            if(itemSelected.isUsable)
            {
                placeableItem = false;
                plantableItem = false;
                useableItem = true;
                Debug.Log(itemSelected.Item_Title + " is Usable");
                R_Trigger_B.transform.GetChild(0).GetComponent<TMP_Text>().text = "Use";
                //break;
            }
            else if(itemSelected.isPlaceable)
            {
                placeableItem = true;
                plantableItem = false;
                useableItem = false;
                Debug.Log(itemSelected.Item_Title + " is Placable");
                R_Trigger_B.transform.GetChild(0).GetComponent<TMP_Text>().text = "Place";
                //break;
            }
            else if(itemSelected.isPlantable)
            {
                placeableItem = false;
                plantableItem = true;
                useableItem = false;
                Debug.Log(itemSelected.Item_Title + " is Plantable");
                R_Trigger_B.transform.GetChild(0).GetComponent<TMP_Text>().text = "Plant";  
                //break;
            }*/
            PC.SelectedItem(itemSelected);
        }
        else
        {
            Debug.Log("Item Not Found");
            PC.SelectedItem(null);
        }
        
    }

    public static Item GetItem()
    {
        return itemSelected;
    }
}

