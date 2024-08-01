using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button_Manager : MonoBehaviour
{
    [SerializeField] GameObject RefUI;

    public bool Trigger;
    public bool fromPlayer;
    int index_CI = -1;
    public int index_RT = -1;


    void Start()
    {
        RefUI = GameObject.FindGameObjectWithTag("Ref_UI");
    }

    public void Raycast_Trigger()
    {
        index_RT += 1;
        Debug.Log(index_RT);
        if(index_RT % 2 == 0)
        {
            Trigger = true;
        }
        else
        {
            Trigger = false;
        }
    }

    public void Change_Inventory()
    {
        index_CI += 1;
        if(index_CI % 2 == 0)
        {
            fromPlayer = true;
        }
        else
        {
            fromPlayer = false;
        }
    }

    public void CloseRefUI()
    {
        RefUI.SetActive(false);
    }

}
