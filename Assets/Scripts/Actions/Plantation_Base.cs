using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;



public class Plantation_Base : MonoBehaviour
{
    public Item itemPlanted;

    [SerializeField] GameObject[] TeaPlant;
    [SerializeField] GameObject[] CoffeePlant;
    [SerializeField] Player_Inventory PI;
    [SerializeField] GameObject TaskUIContent;

    [SerializeField] GameObject pBase_UI;

    void Start()
    {
        PI = GameObject.FindWithTag("Player").GetComponent<Player_Inventory>();
        TaskUIContent = GameObject.FindWithTag("TaskUIContent");
    }

    public void Plantitem(Item itemToPlant)
    {
        itemPlanted = itemToPlant;
        Debug.Log("!" + itemPlanted.Item_Title);
        //StartCoroutine(GrowPlant());
        GrowPlant();
    }

    public void GrowPlant()
    {
        if(itemPlanted.Item_Title == "Tea_Plant")
        {
            Object prefab = Resources.Load("Prefabs/UI/TaskDetailBG");
            GameObject taskPrefab = Instantiate(prefab as GameObject,TaskUIContent.transform);
            Debug.Log(itemPlanted.Item_Title + " is Planted");
            Debug.Log(taskPrefab.name +" "+ prefab.name);
            UI_Task uiTask = taskPrefab.GetComponent<UI_Task>();
            uiTask.OnInstantiate(10f,itemPlanted.Item_Title,this.gameObject);
        }
        /*else if(itemPlanted.Item_Title == "Coffee_Plant")
        {
            Debug.Log(itemPlanted.Item_Title + " is Planted");
            PI.Remove_Item(1,itemPlanted);
            GameObject CoffeePrefab = Instantiate(CoffeePlant[0],transform.position,Quaternion.identity);
            yield return new WaitForSeconds(20f);
            Debug.Log(itemPlanted.Item_Title + " is Growing");
            PI.Remove_Item(1,itemPlanted);
            PI.Receive_Items(1,null,"Tea_Powder");
            itemPlanted = null;
        }*/
    }

    public void OnCompleted(string itemName)
    {
        PI.Remove_Item(1,itemPlanted);
        PI.Receive_Items(1,null,itemName);
        //uiTask.deleteTaskOnOver();
    }

    public Item getItemPlant()
    {
        return itemPlanted;
    }

    public void onHoldObject()
    {
        pBase_UI.SetActive(true);
    }

}
