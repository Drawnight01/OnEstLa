using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Animations;

public class ScaleInstruments : MonoBehaviour
{
    public AudioMixer _Mixer;
    public AudioSource _Source;

    public float smoothTime;
    public AnimationCurve smoothCurve;
    public Animator animController;

    public string _NumInstrument;

    public float multiplicateur = 1f;

    private Player player;
    private float newdata;

    private void Start()
    {
        _Source = GetComponent<AudioSource>();
        player = GameObject.Find("Player").GetComponent<Player>();
        animController = GetComponent<Animator>();
    }

    public void RefreshVal()
    {
        _Source.volume = (player.GetMasterLevel("Volume_"+_NumInstrument) + 80f) / 100f;
        animController.SetFloat("Volume", _Source.volume);
        
        _Source.pitch = player.GetMasterLevel("Pitch_"+_NumInstrument);
        animController.SetFloat("Pitch", _Source.pitch);
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

    }

    private void AutoScale()
    {
        
        Vector3 vect = Vector3.Lerp(Vector3.one, (new Vector3(newdata, newdata, newdata)), smoothCurve.Evaluate(Time.deltaTime * smoothTime));
        transform.localScale = vect;
    }
}
