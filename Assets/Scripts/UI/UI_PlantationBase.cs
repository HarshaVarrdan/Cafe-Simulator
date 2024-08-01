using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_PlantationBase : MonoBehaviour
{
    [SerializeField] GameObject UI;
 
    public void close_B()
    {
        UI.SetActive(false);
    }

}
