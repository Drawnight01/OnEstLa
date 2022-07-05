using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputsManager : MonoBehaviour
{
    private Vector2 mousePos1, mousePos2;
    private float xDiff, yDiff;
    private RaycastHit hit;
    private Camera mainCam;
    private int pbIndex;
    public VolumePitchManager scriptCollider;

    private void Start()
    {
        mainCam = GameObject.Find("Main Camera").GetComponent<Camera>();
    }

    private void FixedUpdate ()
    {
        if (Input.GetMouseButton(0))
            StartCoroutine(OnMouseLeft());
        
        CheckVictory();
    }

    public IEnumerator OnMouseLeft ()
    {
        if (Physics.Raycast(mainCam.ScreenToWorldPoint(Input.mousePosition), new Vector3(-100,0,0), out hit, Mathf.Infinity))
        {
            scriptCollider = hit.collider.gameObject.GetComponent<VolumePitchManager>();
            if(scriptCollider.isMistaking) //condition sur le fait que le groupe soit dans l'erreur
            {
                mousePos1 = Input.mousePosition;
        
                yield return new WaitForSeconds(0.03f);

                mousePos2 = Input.mousePosition;

                xDiff = mousePos2.x - mousePos1.x;
                yDiff = mousePos2.y - mousePos1.y;

                pbIndex = scriptCollider.CheckProblem();
                Debug.Log(pbIndex);

                switch (pbIndex)
                {
                    default:
                        Debug.Log("They ain't making any mistake");
                    break;

                    case 1 :
                        if (xDiff < 0 && Mathf.Abs(yDiff) < Mathf.Abs(xDiff))
                            scriptCollider.ResolveProblem();
                        break;

                    case 2 :
                        if (xDiff > 0 && Mathf.Abs(yDiff) < Mathf.Abs(xDiff))
                            scriptCollider.ResolveProblem();
                        break;

                    case 3 : 
                        if (yDiff < 0 && Mathf.Abs(yDiff) > Mathf.Abs(xDiff))
                            scriptCollider.ResolveProblem();
                        break;

                    case 4 :
                        if (yDiff > 0 && Mathf.Abs(yDiff) > Mathf.Abs(xDiff))
                            scriptCollider.ResolveProblem();
                        break;
                } 
            }
            else Debug.Log("The group ain't making any mistake");
        }
    }

    public void CheckVictory()
    {

    }
}
