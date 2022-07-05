using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    [SerializeField] private List<VolumePitchManager> listScripts;
    private GameObject instruments;
    [SerializeField] private float val;

    private void Start ()
    {
        listScripts = new List<VolumePitchManager>();
        instruments = GameObject.Find("Instrumentals");

        FillList();

        float rand = Random.Range(2f, 10f);
        InvokeRepeating("GenerateEvent", 1, rand);

        //InvokeRepeating("UpdateList", 1, 1);
    }

    

    private void FillList()
    {
        for (int i = 0; i < instruments.transform.childCount; i++)
        {
            VolumePitchManager scriptI_VP = instruments.transform.GetChild(i).GetChild(0).gameObject.GetComponent<VolumePitchManager>();
            ScaleInstruments scriptI_Scale = instruments.transform.GetChild(i).gameObject.GetComponent<ScaleInstruments>();

            //Debug.Log(scriptI_VP.GetMasterLevel("Volume_" + scriptI_Scale._NumInstrument));
            if (!listScripts.Contains(scriptI_VP) && scriptI_VP.GetMasterLevel("Volume_" + scriptI_Scale._NumInstrument) > 0)
                listScripts.Add(instruments.transform.GetChild(i).GetChild(0).gameObject.GetComponent<VolumePitchManager>());
        }
    }

    private void UpdateList()
    {
        foreach (VolumePitchManager script in listScripts)
        {
            if (script.GetMasterLevel("Volume_" + script.transform.parent.gameObject.GetComponent<ScaleInstruments>()._NumInstrument) <= 0) 
                listScripts.Remove(script);
        }
    }

    private void GenerateEvent()
    {
        UpdateList();

        FillList();

        int indexScript = Random.Range(0, listScripts.Count);
        int indexProblem = Random.Range(1, 4);

        listScripts[indexScript].index = indexProblem;
        listScripts[indexScript].isMistaking = true;

        Debug.Log("generate = "+indexProblem);

        switch (indexProblem)
        {
            default:
                Debug.Log("They ain't making any mistake");
                break;

            case 1 :
                string name1 = listScripts[indexScript].transform.parent.gameObject.GetComponent<ScaleInstruments>()._NumInstrument;
                listScripts[indexScript].Up("Pitch_" + name1, val);
                break;

            case 2 :
                string name2 = listScripts[indexScript].transform.parent.gameObject.GetComponent<ScaleInstruments>()._NumInstrument;
                listScripts[indexScript].Down("Pitch_" + name2, val);
                break;

            case 3 : 
                string name3 = listScripts[indexScript].transform.parent.gameObject.GetComponent<ScaleInstruments>()._NumInstrument;
                listScripts[indexScript].Up("Volume_" + name3, val);
                break;

            case 4 :
                string name4 = listScripts[indexScript].transform.parent.gameObject.GetComponent<ScaleInstruments>()._NumInstrument;
                listScripts[indexScript].Down("Volume_" + name4, val);
                break;
        }
    }
}
