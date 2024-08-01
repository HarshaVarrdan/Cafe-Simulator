using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UI_Task : MonoBehaviour
{
    [SerializeField] TMP_Text taskName;
    [SerializeField] Slider progressBar; 
    
    GameObject pBaseGO;
    Plantation_Base pBase;

    Dictionary<string,string> taskName_Dict = new Dictionary<string, string>(){{"Tea_Plant","Tea Plant Planted"},{"Coffee_Plant","Coffee Plant Planted"}}; 

    public void OnInstantiate(float PB_Timer, string Task_Name, GameObject pbGO) 
    {
        pBaseGO = pbGO;
        pBase = pBaseGO.GetComponent<Plantation_Base>();
        StartCoroutine(ProgressBar(PB_Timer));
        taskName.text = taskName_Dict[Task_Name];
    }

    IEnumerator ProgressBar(float Time)
    {
        for(int i = 0;i <= Time; i++)
        {
            Debug.Log(i);
            yield return new WaitForSeconds(1);
            progressBar.value = (i*100/Time) ;
        }
        pBase.OnCompleted(taskName.text);
        yield return true;
    }

    public void deleteTaskOnOver()
    {
        Destroy(gameObject);
    }

}
