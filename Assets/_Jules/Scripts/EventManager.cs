using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    private List<VolumePitchManager> listScripts;
    private GameObject instruments;
    [SerializeField] private float val;

    private void Start ()
    {
        listScripts = new List<VolumePitchManager>();
        instruments = GameObject.Find("INSTRUMENTS");

        FillList();
    }

    private void FixedUpdate()
    {
        UpdateList();

        float rand = Random.Range(0.3f, 2f);
        InvokeRepeating("GenerateEvent", 1, rand);
    }

    private void FillList()
    {
        for (int i = 0; i < instruments.transform.childCount; i++)
        {
            listScripts.Add(instruments.transform.GetChild(i).gameObject.GetComponent<VolumePitchManager>());
        }
    }

    private void UpdateList()
    {
        foreach (VolumePitchManager script in listScripts)
        {
            if (script.GetMasterLevel(script.gameObject.name) >= -75) listScripts.Remove(script);
            else if (!listScripts.Contains(script)) listScripts.Add(script);
        }
    }

    private void GenerateEvent()
    {
        int indexScript = Random.Range(0, listScripts.Count);
        int indexProblem = Random.Range(1, 4);

        listScripts[indexScript].index = indexProblem;
        listScripts[indexScript].isMistaking = true;

        //celui ciiiiiiiiiiiiiiiiiii !!!!!!!!!!!!!!!!!!!!!!!
        switch (indexProblem)
        {
            default:
                Debug.Log("They ain't making any mistake");
                break;

            case 1 :
                listScripts[indexScript].Up("Pitch", val);
                break;

            case 2 :
                listScripts[indexScript].Down("Pitch", val);
                break;

            case 3 : 
                listScripts[indexScript].Up("Volume", val);
                break;

            case 4 :
                listScripts[indexScript].Down("Volume", val);
                break;
        }
    }
}
