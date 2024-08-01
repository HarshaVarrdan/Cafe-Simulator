using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Item_Transfer_B : MonoBehaviour
{
    #region Variables
    
    //UI_Elements
    [SerializeField] TMP_Text Item_Text;
    [SerializeField] TMP_InputField Item_IF;

    
    //Scripts
    [SerializeField] Button_Manager BM;
    [SerializeField] Player_Inventory PI;
    [SerializeField] Refrigerator_Inventory RI;

    //Variables
    [SerializeField] List<Item> Inventory_Items_L = new List<Item>();

    #endregion


    void Start()
    {
        BM = GameObject.FindWithTag("BM").GetComponent<Button_Manager>();
        PI = GameObject.FindWithTag("Player").GetComponent<Player_Inventory>();
        RI = GameObject.FindWithTag("Refrigerator").GetComponent<Refrigerator_Inventory>();
    }

    public void Trigger_Transfer()
    {
        //Item_Text = transform.parent.GetChild(0).GetComponent<TMP_Text>();
        int QTY = int.Parse(Item_IF.text);
        if(BM.fromPlayer)
        {
            Inventory_Items_L = new List<Item>(PI.Player_Inv.Keys);
            Item transferPlayerItem = PI.Check_Player_For_Item(Item_Text.text);
            if(PI.Player_Inv[transferPlayerItem] >= QTY)
            {
                Transfer(transferPlayerItem,QTY);
            }
        }
        else
        {
            Inventory_Items_L = new List<Item>(RI.Ref_Inv.Keys);
            Item transferRefItem = RI.Check_Ref_For_Item(Item_Text.text);
            if(RI.Ref_Inv[transferRefItem] >= QTY)
            {
                Transfer(transferRefItem,QTY);
            }
        }
    }

    void Transfer(Item item,int qty)
    {
        if(BM.fromPlayer)
        {
            PI.Remove_Item(qty,item);
            RI.Receive_Item(item,qty);
            Debug.Log($"Tranfered {item.Item_Title} from Player_Inv to Ref");
        }
        else
        {
            RI.Remove_Item(item,qty);
            PI.Receive_Items(qty,item);
            Debug.Log($"Tranfered {item.Item_Title} from Ref_Inv to Player");
        }
    }
}
