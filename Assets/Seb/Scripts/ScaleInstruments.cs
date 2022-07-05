using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class ScaleInstruments : MonoBehaviour
{
    public AudioMixer _Mixer;
    public AudioSource _Source;

    public float smoothTime;
    public AnimationCurve smoothCurve;

    public string _NumInstrument;

    public float multiplicateur = 1f;

    private Player player;
    private float newdata;

    private void Start()
    {
        _Source = GetComponent<AudioSource>();
        player = GameObject.Find("Player").GetComponent<Player>();
    }

    public void RefreshVal()
    {
        _Source.volume = (player.GetMasterLevel("Volume_"+_NumInstrument) + 80f) / 100f;
        
        _Source.pitch = player.GetMasterLevel("Pitch_"+_NumInstrument);
    }
    
    void OnAudioFilterRead(float[] data, int channels)
    {        
        for (int i = 0; i < data.Length; ++i)
        {
            newdata = 1 -(data[i] * multiplicateur);
        }        
    }

    private void Update()
    {
        RefreshVal();
        Vector3 vect = Vector3.Lerp(Vector3.one, (new Vector3(newdata, newdata, newdata) ), smoothCurve.Evaluate(Time.deltaTime * smoothTime));
        transform.localScale = vect;
        //Debug.Log(newdata);
    }
}
