using System.Collections;
using System.Linq;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_Inventory : MonoBehaviour
{
    #region Variables

    //Scripts
    [SerializeField] Player_Inventory PI;
    [SerializeField] Item_Database item_Database;

    //UI_Elements
    [SerializeField] GameObject[] itemContainers;
    [SerializeField] Text[] Count_Container;

    //Variables
    List<string> Item_Name = new List<string>();
    //Dictionary<string,int> itemIndex = new Dictionary<string, int>();
    
    #endregion

    public void Inventory_UI_Icon_Update()
    {
        int index = -1;
        foreach(Item i in PI.Player_Inv.Keys)
        {
            index += 1;
            itemContainers[index].transform.GetChild(0).GetComponent<Image>().sprite = i.Item_Icon;
            itemContainers[index].transform.GetChild(0).GetChild(0).GetComponent<Text>().text = PI.Player_Inv[i].ToString();
            //itemContainers[index].sprite = i.Item_Icon;
            var tempColor = itemContainers[index].transform.GetChild(0).GetComponent<Image>().color;
            tempColor.a = 1f;
            itemContainers[index].transform.GetChild(0).GetComponent<Image>().color = tempColor;
            //Debug.Log($"UI Updated for {i.Item_Title} with {i.Item_Icon.name} and Text {PI.Player_Inv[i].ToString()}");
        }
        for(int x = 0; x < 7; x++)
        {
            if(x > index)
            {
                itemContainers[x].transform.GetChild(0).GetComponent<Image>().sprite = null;
                var tempColor = itemContainers[x].transform.GetChild(0).GetComponent<Image>().color;
                tempColor.a = 0f;
                itemContainers[x].transform.GetChild(0).GetComponent<Image>().color = tempColor;
                itemContainers[x].transform.GetChild(0).GetChild(0).GetComponent<Text>().text = "0";
                //Debug.Log($"UI Updated to null for index {x}");
            }
        }
        foreach(GameObject GB in itemContainers)
        {
            if(GB.transform.GetChild(0).GetComponent<Image>().sprite == null)
            {
                var tempColor = GB.transform.GetChild(0).GetComponent<Image>().color;
                tempColor.a = 0f;
                GB.transform.GetChild(0).GetComponent<Image>().color = tempColor;
            }
        }
    }
}
