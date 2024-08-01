using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Action_Database : MonoBehaviour
{
    [SerializeField] Actions act;
    //public string[] Required_Items;
    public Dictionary<string,Dictionary<string,int>> actions = new Dictionary<string, Dictionary<string,int>>(); 

    void Start()
    {    
        actions.Add("Tea Maker",new Dictionary<string, int>(){{"Tea_Powder",3},{"Milk",1},{"Sugar",2}});
        actions.Add("Coffee Maker",new Dictionary<string, int>(){{"Coffee_Powder", 2},{"Milk",1},{"Sugar",2}});
    }
}
