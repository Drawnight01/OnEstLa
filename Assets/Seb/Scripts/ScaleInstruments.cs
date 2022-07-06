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
    
    
    public string _NumInstrument;
    public VolumePitchManager[] volList;

    public GameObject fx_Note_Bonne;
    public GameObject fx_Note_PitchTrop;
    public GameObject fx_Note_PitchPas;
    public GameObject fx_Note_VolumeTrop;
    public GameObject fx_Note_VolumePas;

    public Light _Light;
    public Material _LightMat;

    public float multiplicateur = 1f;

    public float valData;
    private Player player;
    private float newdata;


    public float waitSeconds;
    public int compteur;
    public float compare;
    private VolumePitchManager volPitchManager;

    private void Start()
    {
        _Source = GetComponent<AudioSource>();
        player = GameObject.Find("Player").GetComponent<Player>();
        volPitchManager = transform.GetChild(0).GetComponent<VolumePitchManager>();
        FX_Selected = fx_Note_Bonne;
        
    }

    private GameObject FX_Selected;
    
    public void SpawnFX()
    {
        if(compteur == 0 && valData > compare)
        {
            compteur = 1;

            switch (volPitchManager.index)
            {
                case 0:
                    FX_Selected = fx_Note_Bonne;
                    _Light.intensity = 10f;
                    _LightMat.SetFloat("EdgeFallOf", -2f);
                    break;
                case 1:
                    //pitchTrop
                    FX_Selected = fx_Note_PitchTrop;
                    _Light.intensity = 5f;
                    _LightMat.SetFloat("EdgeFallOf", 1f);
                    break;
                case 2:
                    //pitchpasAssez
                    FX_Selected = fx_Note_PitchPas;
                    _Light.intensity = 5f;
                    _LightMat.SetFloat("EdgeFallOf", 1f);
                    break;
                case 3:
                    //VolumeTrop
                    FX_Selected = fx_Note_VolumeTrop;
                    _Light.intensity = 5f;
                    _LightMat.SetFloat("EdgeFallOf", 1f);
                    break;
                case 4:
                    //volumepasassez
                    FX_Selected = fx_Note_VolumePas;
                    _Light.intensity = 5f;
                    _LightMat.SetFloat("EdgeFallOf", 1f);
                    break;
            }

            if (volPitchManager.isMistaking)
            {
                GameObject obj = Instantiate(FX_Selected);
                obj.transform.position = transform.position;
                Destroy(obj, 1f);
                StartCoroutine(TimerSpawn());
            }
            else
            {
                GameObject obj = Instantiate(fx_Note_Bonne);
                obj.transform.position = transform.position;
                Destroy(obj, 1f);
                StartCoroutine(TimerSpawn());
            }
            
            
        }     
        
    }



    IEnumerator TimerSpawn()
    {
        yield return new WaitForSeconds(waitSeconds);
        compteur = 0;
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
            valData = data[i]; 
            newdata = 1 + (valData * multiplicateur);
        }        
    }

    private void Update()
    {
        RefreshVal();
        AutoScale();
        SpawnFX();
    }

    
    private void AutoScale()
    {        
        Vector3 vect = Vector3.Slerp(transform.localScale, new Vector3(newdata, newdata, newdata), smoothCurve.Evaluate(Time.deltaTime * valData * smoothTime));
        transform.localScale = vect;               
    }
}
