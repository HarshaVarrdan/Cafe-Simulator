using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Actions : MonoBehaviour
{
    [SerializeField] Action_Database AD;
    [SerializeField] Item_Database ID;
    [SerializeField] Player_Inventory PI;
    [SerializeField] UI_Inventory UI_I;
    [SerializeField] Item[] Needed;

    public void Call_Actions(string Action_Name)
    {
        switch (Action_Name)
        {
            case "Tea Maker":
            Action("Tea Maker");
            break;
            case "Coffee Maker":
            Action("Coffee Maker");
            break;
        }
    }

    public void Action(string action)
    {
        Dictionary<string,int> requiredItems = new Dictionary<string, int>(AD.actions[action]);
        Dictionary<Item,int> Needed = new Dictionary<Item,int>();
        Dictionary<Item,int> playerItems = new Dictionary<Item,int>(PI.Player_Inv);
        foreach(KeyValuePair<string,int> item in requiredItems)
        {
            Debug.Log(item.Key+" : "+item.Value);
            if(playerItems.ContainsKey(ID.GetItem(item.Key)))
            {
                if(playerItems[ID.GetItem(item.Key)] >= item.Value)
                {
                    Needed.Add(ID.GetItem(item.Key),item.Value);
                    Debug.Log("Player has needed Item = " + Needed[ID.GetItem(item.Key)]+" - "+ item.Key);
                }
                else Debug.Log("Qty is Not sufficient for "+ item.Key + " Needed :" + Needed[ID.GetItem(item.Key)]);
            }
            else Debug.Log("Items not Present = " + item.Key);
        } 
        if(Needed.Keys.Count == requiredItems.Keys.Count)
        {
            Debug.Log("All items are there");
            foreach(KeyValuePair<Item,int> item in Needed)
            {
                PI.Remove_Item(item.Value,item.Key);
            }
            foreach(Item x in PI.Player_Inv.Keys)
            {
                Debug.Log("Item in Inventory = " + x.Item_Title);
            }
        }
    }
}

