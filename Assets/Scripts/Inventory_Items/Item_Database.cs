using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item_Database : MonoBehaviour
{
    public List<Item> items = new List<Item>();

    void Awake()
    {
        Build_Database();
    }

    public Item GetItem(int ID)
    {
        return items.Find(items => items.Item_ID == ID);
    }

    public Item GetItem(string Name)
    {
        return items.Find(items => items.Item_Title == Name);
    }

    void Build_Database()
    {
        items = new List<Item>{
            new Item(0,"Coffee_Powder","Powder that is used to Make Coffee",0,true,false,false,true),
            new Item(1,"Tea_Powder","Powder that is used to Make Tea",0,true,false,false,true),
            new Item(2,"Milk","Milk that is used to Make Tea and Coffee",0,true,false,false,true),
            new Item(3,"Sugar","Sugar that is used to add Sweetness",0,true,false,false,true),
            new Item(4,"Tea","Drink that keeps you Awake",0,true),
            new Item(5,"Coffee","Drink that gives you Energy",0,true),
            new Item(6,"Plantation_Base","Place used to Plant Saplings",0,false,true),
            new Item(7,"Tea_Plant","Plant that provides you with Tea Powder",0,false,false,true),
            new Item(8,"Coffee_Plant","Plant that provides you with Coffee Powder",0,false,false,true)
        };
    }
}
