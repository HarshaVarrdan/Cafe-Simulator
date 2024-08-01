 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI_Utensils : MonoBehaviour
{
    [SerializeField] Dictionary<Item,int> vesselItem = new Dictionary<Item, int>();
    Dictionary<string,Dictionary<Item,int>> pdtMakeable = new Dictionary<string,Dictionary<Item,int>>();

    [SerializeField] Item_Database item_Database;
    [SerializeField] UI_Makeable uiM;
    [SerializeField] GameObject panelGB;

    void Start()
    {
        // Object prefab = Resources.Load("Prefabs/UI/MakeablePanel");
        // if(prefab != null) Debug.Log("Yes");
        // else Debug.Log("No");
        // panelGB = Instantiate(prefab as GameObject,transform);
        item_Database = GameObject.FindWithTag("ID_S").GetComponent<Item_Database>();
        pdtMakeable.Add("Tea Maker",new Dictionary<Item, int>(){{Check_Database_For_Item("Tea_Powder"),3},{Check_Database_For_Item("Milk"),1},{Check_Database_For_Item("Sugar"),2}});
        pdtMakeable.Add("Coffee Maker",new Dictionary<Item, int>(){{Check_Database_For_Item("Coffee_Powder"),3},{Check_Database_For_Item("Milk"),1},{Check_Database_For_Item("Sugar"),2}});  
    }

    Item Check_Database_For_Item(string itemName)
    {
        List<Item> databaseItems = new List<Item>(item_Database.items);
        if(databaseItems.Find(items => items.Item_Title == itemName) == null) return null;
        else return databaseItems.Find(items => items.Item_Title == itemName);
    }  

    public void addItem(Item addItem)
    {
        if(vesselItem.ContainsKey(addItem))
        {
            vesselItem[addItem] += 1;
            Debug.Log(addItem.Item_Title + " changed Qty in vessel");
            uiM.instantiateAddedItems(addItem);
        }
        else
        {
            vesselItem.Add(addItem,1);
            Debug.Log(addItem.Item_Title + " Added in Vessel");
            uiM.instantiateAddedItems(addItem);
        }
        CheckPdt(); 
    }

    public void removeItem(Item removeItem)
    {
        if(vesselItem.ContainsKey(removeItem))
        {
            vesselItem[removeItem] -= 1;
            Debug.Log(removeItem.Item_Title + " changed Qty");
            if(vesselItem[removeItem] == 0) vesselItem.Remove(removeItem);
        }
    }

    public void CheckPdt()
    {
        foreach(KeyValuePair<string,Dictionary<Item,int>> pdt in pdtMakeable)
        {
            int Count = 0;
            foreach(KeyValuePair<Item,int> need in pdtMakeable[pdt.Key])
            {
                if(vesselItem.ContainsKey(need.Key))
                {
                    if(vesselItem[need.Key] >= need.Value)
                    {
                        Debug.Log(need.Key);
                        Count +=1;
                        Debug.Log(Count);
                    }
                }
            }
        
            if(Count == pdtMakeable[pdt.Key].Count && vesselItem.Keys.Count == Count) 
            {
                UI_Makeable UIM = panelGB.GetComponent<UI_Makeable>();
                Debug.Log(pdt.Key + " is Makeable");
                UIM.instantiateMakeable(pdt.Key);
            }
        }
    }

    public void pickUpMe()
    {
        GameObject pickup = GameObject.FindWithTag("pickUpArea");
        gameObject.transform.parent.SetParent(pickup.transform);
        gameObject.transform.parent.position = pickup.transform.position;
    }

    public void onHoldObject()
    {
        //if(panelGB.activeSelf) 
        panelGB.SetActive(true);
    }
}