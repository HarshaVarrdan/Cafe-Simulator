using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class UI_Ref_Inventory : MonoBehaviour
{
    #region Variables
    
    //Scripts
    [SerializeField] Refrigerator_Inventory RI;
    [SerializeField] Item[] item;

    //GameObjects
    [SerializeField] GameObject Item_Prefab;
    [SerializeField] GameObject prefab;
    [SerializeField] GameObject[] UI_Container;
    
    //Variables
    [SerializeField] int[] qty;
    int index = -1;
    
    #endregion

    public void Ref_UI_Change(string check,Item qtyOfItem = null)
    {
        if(check == "all")
        {
            index += 1;
            int i = -1;
            UI_Container = new GameObject[RI.Ref_Inv.Count];
            item = new Item[RI.Ref_Inv.Count];
            qty = new int[RI.Ref_Inv.Count];
        

            foreach(KeyValuePair<Item,int> it in RI.Ref_Inv)
            {
                i += 1;
                item[i] = it.Key;
                qty[i] = it.Value;
            }

            prefab = Instantiate(Item_Prefab,transform);
            UI_Container[index] = prefab;
            Image image = prefab.transform.GetChild(0).GetComponent<Image>();
            image.sprite = item[index].Item_Icon;
            Text text = image.transform.GetChild(0).GetComponent<Text>();
            text.text = qty[index].ToString();
            Debug.Log("Updated Ref_UI with " + item[index].Item_Title);
        }
        else if(check == "qty" && qtyOfItem != null)
        {
            foreach(GameObject GO in UI_Container)
            {
                if(GO.transform.GetChild(0).GetComponent<Image>().sprite == qtyOfItem.Item_Icon)
                {
                    GO.transform.GetChild(0).GetChild(0).GetComponent<Text>().text = RI.Ref_Inv[qtyOfItem].ToString();
                    Debug.Log($"Changed qty of {qtyOfItem.Item_Title}");
                }
            }
        }
    }
}
