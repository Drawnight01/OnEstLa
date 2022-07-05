using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class Player : MonoBehaviour
{
    public AudioMixer _Mixer;
    
    


    private void Update()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            SetParam("Pitch_1", 1f);
        }
        else
        {
            Down("Pitch_1", 0.001f);
        }
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
