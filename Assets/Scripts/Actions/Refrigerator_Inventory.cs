using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Refrigerator_Inventory : MonoBehaviour
{
    #region Variables

    //Scripts
    [SerializeField] Item_Database ID;
    [SerializeField] Player_Inventory PI;
    [SerializeField] UI_Ref_Inventory Ref_UI;
    [SerializeField] UI_Tranfer T_UI;
    
    //Variables
    public Dictionary<Item,int> Ref_Inv = new Dictionary<Item,int>();

    #endregion


    public Item Check_Ref_For_Item(string itemName)
    {
        List<Item> Ref_Items = new List<Item>(Ref_Inv.Keys);
        return Ref_Items.Find(items => items.Item_Title == itemName);
    }
    
    public void Receive_Item(Item item,int qty)
    {
        if(Ref_Inv.ContainsKey(item))
        {
            Ref_Inv[item] += qty;
            Ref_UI.Ref_UI_Change("qty",item);
        }
        else
        {
            Ref_Inv.Add(item,qty);
            Ref_UI.Ref_UI_Change("all");
            T_UI.Transfer_UI_Update();
        }
    }

    public void Remove_Item(Item item,int qty)
    {
        if(Ref_Inv.ContainsKey(item))
        {
            if(Ref_Inv[item] >= qty)
            {
                Ref_Inv[item] -= qty;
                check_qty(item);
            }
        }
    }

    void check_qty(Item itemToCheck)
    {
        if(Ref_Inv[itemToCheck] == 0)
        {
            Ref_Inv.Remove(itemToCheck);
        }
    }

}
