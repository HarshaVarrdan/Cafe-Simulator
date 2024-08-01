using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Inventory : MonoBehaviour
{
    #region Variables

    //Variables
    public Dictionary<Item,int> Player_Inv = new Dictionary<Item, int>();

    //Scripts
    public Item_Database item_Database;
    public UI_Inventory ui_Inventory;

    [SerializeField] Player_Controller PC;
    #endregion

    void Start()
    {
        Receive_Items(3,null,"Tea_Powder");
        Receive_Items(2,null,"Tea_Plant");
        Receive_Items(1,null,"Milk");
        Receive_Items(2,null,"Sugar");
        Receive_Items(3,null,"Coffee_Powder");
    }

    public void Receive_Items(int Qty,Item item = null,string itemName = null)
    {
        if(item != null)
        {
            if(!Player_Inv.ContainsKey(item))
            {
                Player_Inv.Add(item,Qty);
                Debug.Log("Item added to Inventory = " + item.Item_Title + " : " + Player_Inv[item]);
                ui_Inventory.Inventory_UI_Icon_Update();
            }
            else
            {
                Player_Inv[item] += Qty;
                Debug.Log("Qty Change for Item = " + item.Item_Title + " : " + Player_Inv[item]);
                ui_Inventory.Inventory_UI_Icon_Update(); 
            }
        }
        else if(itemName !=null)
        {
            Item itemToAdd = Check_Database_For_Item(itemName);
            if(itemToAdd != null)
            {
                if(!Player_Inv.ContainsKey(itemToAdd))
                {
                    Player_Inv.Add(itemToAdd,Qty);
                    Debug.Log("Item added to Inventory = " + itemToAdd.Item_Title + " : " + Player_Inv[itemToAdd]);
                    ui_Inventory.Inventory_UI_Icon_Update();
                }
                else
                {
                    Player_Inv[itemToAdd] += Qty;
                    Debug.Log("Qty Change for Item = " + itemToAdd.Item_Title + " : " + Player_Inv[itemToAdd]);
                    ui_Inventory.Inventory_UI_Icon_Update(); 
                }
            }
            else
            {
                Debug.Log("");
            }
        }

    }

    public Item Check_Database_For_Item(string itemName)
    {
        List<Item> databaseItems = new List<Item>(item_Database.items);
        if(databaseItems.Find(items => items.Item_Title == itemName) == null) return null;
        else return databaseItems.Find(items => items.Item_Title == itemName);
    }  

    public Item Check_Player_For_Item(string itemName)
    {
        List<Item> Player_Items = new List<Item>(Player_Inv.Keys);
        if(Player_Items.Find(items => items.Item_Title == itemName) == null)
        {
            return null;
        }
        else return Player_Items.Find(items => items.Item_Title == itemName);
    }                         

    public void Remove_Item(int qty,Item item = null,string itemName = null)
    {
        if(item !=null)
        {
            if(Player_Inv.ContainsKey(item))
            {
                if(Player_Inv[item] >= qty)
                {
                    Player_Inv[item] -= qty;
                    Debug.Log("Qty Change for Item = " + item.Item_Title + " : " + Player_Inv[item]);
                    check_qty(item);
                    ui_Inventory.Inventory_UI_Icon_Update(); 
                }
            }
        }
        else if(itemName !=null)
        {
            Item itemToRemove = Check_Player_For_Item(itemName);
            if(itemToRemove !=null)
            {
                if(Player_Inv[itemToRemove] >= qty)
                {
                    Player_Inv[itemToRemove] -= qty;
                    Debug.Log("Qty Change for Item = " + item.Item_Title + " : " + Player_Inv[item]);
                    check_qty(itemToRemove);
                }
            }
        }
        
    }      

    void check_qty(Item itemToCheck)
    {
        if(Player_Inv[itemToCheck] == 0)
        {
            Debug.Log("Item Removed  = " + itemToCheck.Item_Title);
            Player_Inv.Remove(itemToCheck);
            ui_Inventory.Inventory_UI_Icon_Update(); 
            if(PC.selected == itemToCheck)
            {
                PC.SelectedItem(null);
            }
        }
        else ui_Inventory.Inventory_UI_Icon_Update();  
    }                                          
}
