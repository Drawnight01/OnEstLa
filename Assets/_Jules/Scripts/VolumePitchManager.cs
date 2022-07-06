using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class VolumePitchManager : MonoBehaviour
{
    public bool isMistaking;
    public int index;
    public AudioMixer _Mixer;
    public Material redMat, greenMat;
    
    private void Start()
    {
        isMistaking = false;
        index = 0;
    }

    private void FixedUpdate()
    {
        if(isMistaking == false)
            GetComponent<Renderer>().material = greenMat;
        else if (isMistaking == true)
            GetComponent<Renderer>().material = redMat;
        
    }

    public void ResolveProblem(string parameter)
    {
        //parmetre son = 0 ou 0.8 environ, param pitch = 100%
        if(parameter.Length <= 7) //if pitch 
            SetParam(parameter, 1);
        else SetParam(parameter, 0); // if volume

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
        _Mixer.SetFloat(name, val);
    }

    public void SetParam(string name, float value)
    {        
        _Mixer.SetFloat(name, value);
    }
}
