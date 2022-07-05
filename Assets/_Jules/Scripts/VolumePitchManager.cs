using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class VolumePitchManager : MonoBehaviour
{
    public bool isMistaking;
    public int index;
    public AudioMixer _Mixer;
    
    private void Start()
    {
        isMistaking = false;
        index = 0;
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

    public float GetMasterLevel(string name)
    {
        float value;
        bool result = _Mixer.GetFloat(name, out value);
        if (result)
        {
            return value;
        }
        else
        {
            return 0f;
        }
    }

    public void Up(string name, float value)
    {
        float val = GetMasterLevel(name);
        val += value;
        _Mixer.SetFloat(name, val);
    }

    public void Down(string name, float value)
    {
        float val = GetMasterLevel(name);
        val -= value;
        _Mixer.SetFloat(this.name, val);
    }
}
