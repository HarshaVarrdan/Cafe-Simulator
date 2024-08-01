using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item
{
    public int Item_ID;
    public string Item_Title;
    public string Item_Desc;
    public Sprite Item_Icon;
    public bool isPlaceable;
    public bool isPlantable;
    public bool isUsable;
    public bool isAddable;


    
    //public Dictionary<string,int> Item_Stats = new Dictionary<string, int>();

    public Item(int Item_ID,string Item_Title, string Item_Desc,int Item_Count,bool isUsable = false, bool isPlaceable = false, bool isPlantable = false, bool isAddable = false)
    {
        this.Item_ID = Item_ID;
        this.Item_Title = Item_Title;
        this.Item_Desc = Item_Desc;
        this.Item_Icon = Resources.Load<Sprite>("Images/Items/" + Item_Title);
        this.isPlaceable = isPlaceable;
        this.isPlantable = isPlantable;
        this.isUsable = isUsable;
        this.isAddable = isAddable;
        //this.Item_Stats = Item_Stats;
        
    }

    public Item(Item items)
    {
        this.Item_ID = items.Item_ID;
        this.Item_Title = items.Item_Title;
        this.Item_Desc = items.Item_Desc;
        this.Item_Icon = Resources.Load<Sprite>("Images/Items/" + items.Item_Title);
        this.isPlaceable = items.isPlaceable;
        this.isPlantable = items.isPlantable;
        this.isUsable = items.isUsable;
        this.isAddable = items.isAddable;
        //this.Item_Stats = items.Item_Stats;
        
    }

}
