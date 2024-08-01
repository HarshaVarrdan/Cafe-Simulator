using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UI_AddedItems : MonoBehaviour
{
    Item item;
    [SerializeField] Image addedItemImage;
    [SerializeField] TMP_Text addedItemName;
    [SerializeField] UI_Makeable UIM;

    void Start()
    {
        UIM = transform.parent.parent.parent.GetComponent<UI_Makeable>();
    }

    public void onInstantiatePrefab(Item itemAdded)
    {
        item = itemAdded;
        addedItemImage.sprite = itemAdded.Item_Icon;
        addedItemName.text = itemAdded.Item_Title;
    }

    public void onRemoveB_Clicked()
    {
        UIM.onAIPrefabDestroy(item);
    }

}
