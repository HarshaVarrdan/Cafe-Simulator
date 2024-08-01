using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using TMPro;

public class UI_Makeable : MonoBehaviour
{
    [SerializeField] AI_Utensils AI_U;

    [SerializeField] GameObject parentObject;
    
    [SerializeField] GameObject makeableItem_B;
    [SerializeField] GameObject addedItems_B;
    [SerializeField] GameObject removeItems_B;
    [SerializeField] GameObject close_B;

    [SerializeField] GameObject removeItemsUI;
    [SerializeField] GameObject addedItemsUI;
    [SerializeField] GameObject makeableItemUI;

    [SerializeField] GameObject NextUI_B;
    [SerializeField] GameObject PreviousUI_B;


    Canvas canvas;

    int showingAI_Index = 0;

    bool showingMI;
    bool showingAI;
    bool showingRI;

    bool isLast;

    GameObject makeableItem;
    [SerializeField] List<GameObject> addedItems_P = new List<GameObject>();


    void Update()
    {
        if(showingAI_Index > 0) PreviousUI_B.SetActive(true);
        else PreviousUI_B.SetActive(false);
        
        if(addedItems_P.Count > 0 && showingAI_Index + 1 != addedItems_P.Count) NextUI_B.SetActive(true);
        else NextUI_B.SetActive(false);
    }

    public void instantiateAddedItems(Item itemAdded)
    {
        Object prefab = Resources.Load("Prefabs/UI/AddedItems");
        addedItems_P.Add(Instantiate(prefab as GameObject,addedItemsUI.transform.GetChild(0).transform));
        addedItems_P[addedItems_P.Count - 1].GetComponent<UI_AddedItems>().onInstantiatePrefab(itemAdded);
        if(addedItems_P.Count > 1)
        {
            addedItems_P[addedItems_P.Count - 1].SetActive(false);
        }
    }
    public void onAIPrefabDestroy(Item itemtoRemove)
    {
        AI_U.removeItem(itemtoRemove);
        if(showingAI_Index == addedItems_P.Count - 1) isLast = true;
        Destroy(addedItems_P[showingAI_Index]);
        addedItems_P.Remove(addedItems_P[showingAI_Index]);
        if(isLast)
        {
            addedItems_P[addedItems_P.Count - 1].SetActive(true);
            showingAI_Index = addedItems_P.Count - 1;
        }
        else addedItems_P[showingAI_Index].SetActive(true);
        Debug.Log(showingAI_Index);
    }

    public void instantiateMakeable(string makeableName)
    {
        Object prefab = Resources.Load("Prefabs/UI/Makeable");
        makeableItem = Instantiate(prefab as GameObject,makeableItemUI.transform.GetChild(0).transform);
        makeableItem.transform.GetChild(1).GetComponent<TMP_Text>().text = makeableName.ToString();
    }

#region Button_Function

    public void ShowAddedItems()
    {       
        showingAI = true;
        makeableItem_B.SetActive(false);
        addedItems_B.SetActive(false);
        removeItems_B.SetActive(false);
        addedItemsUI.SetActive(true);
        close_B.SetActive(false);
        Debug.Log("Showing Added Items");
    }

    public void MakeableItem()
    {
        showingMI = true;
        makeableItem_B.SetActive(false);
        addedItems_B.SetActive(false);
        removeItems_B.SetActive(false);
        makeableItemUI.SetActive(true);
        close_B.SetActive(false);
        Debug.Log("Showing Makeable items");
    }

    public void RemoveItems()
    {
        showingRI = true;
        makeableItem_B.SetActive(false);
        addedItems_B.SetActive(false);
        removeItems_B.SetActive(false);
        removeItemsUI.SetActive(true);
        close_B.SetActive(false);
        Debug.Log("RemoveItems");
    }

    public void PickupObj()
    {
        AI_U.pickUpMe();
    }

    public void Close_B()
    {
        showingAI = false;
        showingMI = false;
        showingRI = false;
        // makeableItem_B.SetActive(false);
        // addedItems_B.SetActive(false);
        // removeItems_B.SetActive(false);
        // close_B.SetActive(false);
        parentObject.SetActive(false);
        Debug.Log("Closed");
    }

    public void CloseUI_B()
    {
        if(showingAI) { addedItemsUI.SetActive(false); showingAI = false; }
        if(showingMI) { makeableItemUI.SetActive(false); showingMI = false; }
        if(showingRI) { removeItemsUI.SetActive(false); showingRI = false; }

        makeableItem_B.SetActive(true);
        addedItems_B.SetActive(true);
        removeItems_B.SetActive(true);
        close_B.SetActive(true);
    }

    public void NextAI_B()
    {
        if(addedItems_P.Count - 1 > showingAI_Index)
        {
            addedItems_P[showingAI_Index].SetActive(false);
            showingAI_Index += 1;
            addedItems_P[showingAI_Index].SetActive(true);
            Debug.Log("next item");
        }
        else Debug.Log("No more items Added");
    }

    public void PreviousAI_B()
    {
        if(showingAI_Index >0)
        {
            addedItems_P[showingAI_Index].SetActive(false);
            showingAI_Index -= 1;
            addedItems_P[showingAI_Index].SetActive(true);
            Debug.Log("previous item");
        }
        else Debug.Log("No more items Added");
    }

#endregion
}
