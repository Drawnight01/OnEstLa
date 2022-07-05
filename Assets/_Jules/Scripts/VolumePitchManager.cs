using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VolumePitchManager : MonoBehaviour
{
    public bool isMistaking;
    private int index;
    
    private void Start()
    {
        isMistaking = true;
        int index = Random.Range(1, 4);
    }

    public int CheckProblem()
    {  
        return index;
    }

    public void ResolveProblem()
    {
        isMistaking = false;
        Debug.Log("REUSSIIIIIIIII !!!!!!!!");
    }
}
