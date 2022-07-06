using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PublicBehaviour : MonoBehaviour
{
    private int health;
    public EventManager scriptEvent;
    private GameObject public1, public2, public3, public4, public5, public6, public7;
    public bool isFullLife, isLoosing, hasBeenDown;
    private float timeLeft;

    void Awake()
    {
        health = 4;
        isFullLife = true;
        scriptEvent = GameObject.Find("Player").GetComponent<EventManager>();

        public1 = GameObject.Find("Le public");
        public2 = GameObject.Find("Le public (1)");
        public3 = GameObject.Find("Le public (2)");
        public4 = GameObject.Find("Le public (3)");
        public5 = GameObject.Find("Le public (4)");
        public6 = GameObject.Find("Le public (5)");
        public7 = GameObject.Find("Le public (6)");

        hasBeenDown=false;
        timeLeft=0;
    }

    void FixedUpdate()
    {
        CheckMistakes();
        if (isLoosing) 
        {
            
            Timer(3);
        }
    }

    public void TakeDamage()
    {
        health -= 1;
        timeLeft=0;

        switch (health)
        {
            case 3 :
                isFullLife = false;

                public1.SetActive(false);
                public3.SetActive(false);
                break;

            case 2 :
                public7.SetActive(false);
                public5.SetActive(false);
                break;

            case 1 :
                public4.SetActive(false);
                public6.SetActive(false);
                break;

            case 0 :
                public2.SetActive(false);
                Defaite();
                break;
        }
    }

   /* public void GainHealth()
    {
        health += 1;

        switch (health)
        {
            case 2 :
                public4.SetActive(true);
                public6.SetActive(true);
                break;

            case 3 :
                public7.SetActive(true);
                public5.SetActive(true);
                break;

            case 4 :
                isFullLife = true;
                public1.SetActive(true);
                public3.SetActive(true);
                break;
        }
    }*/

    void Defaite()
    {
        Debug.Log("T'as perdu lol");
    }    

    void Timer(float finalTime)
    {
        //if (!hasBeenDown)
        //{
            hasBeenDown=true;
            //float timeLeft = 0;
            timeLeft += Time.deltaTime;
            if ( timeLeft > finalTime )
            {
                TakeDamage();
            }
        //}
    }

    void CheckMistakes()
    {
        Debug.Log("has entered");
        foreach (VolumePitchManager script in scriptEvent.listScripts)
        {
            Debug.Log("isMistaking = "+script.isMistaking);
            if(script.isMistaking) 
            {
                isLoosing=true;
            }
        }
    }
}
