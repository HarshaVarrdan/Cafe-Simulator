using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UI_Tranfer : MonoBehaviour
{
    [SerializeField] Player_Inventory PI;
    [SerializeField] Refrigerator_Inventory RI;

    [SerializeField] GameObject Item_Prefab;
    [SerializeField] GameObject[] List_Container_Ref;
    [SerializeField] GameObject[] List_Container_Player;

    [SerializeField] Button_Manager BM;

    Item[] item;

    public bool fromPlayer = false;

    int index = -1;

    // Start is called before the first frame update
    void Start()
    {
        BM = GameObject.FindWithTag("BM").GetComponent<Button_Manager>();
    }

    // Update is called once per frame
    void Update()   
    {
        
    }

    public void Transfer_UI_Update()
    {
        index += 1;
        int i = -1;
        List_Container_Ref = new GameObject[RI.Ref_Inv.Count];
        item = new Item[RI.Ref_Inv.Count];
        foreach(KeyValuePair<Item,int> it in RI.Ref_Inv)
        {
            i += 1;
            item[i] = it.Key;
        }
        GameObject prefab = Instantiate(Item_Prefab,transform);
        List_Container_Ref[i] = prefab;
        TMP_Text text = prefab.transform.GetChild(0).GetComponent<TMP_Text>();
        text.text = item[i].Item_Title;
        Debug.Log("Updated Transfer_UI with " + item[i].Item_Title);
    }
}
