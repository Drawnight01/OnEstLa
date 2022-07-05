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
        Debug.DrawRay(mainCam.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y)), mainCam.transform.forward);
        if (Physics.Raycast(mainCam.ScreenPointToRay(Input.mousePosition), out hit))
        {
                Debug.Log(hit.collider.name);

                scriptCollider = hit.collider.gameObject.GetComponent<VolumePitchManager>();

                if(scriptCollider.isMistaking == true) //condition sur le fait que le groupe soit dans l'erreur
                {
                    Debug.Log("troisieme if");
                    mousePos1 = Input.mousePosition;
            
                    yield return new WaitForSeconds(0.03f);

                    mousePos2 = Input.mousePosition;

                    xDiff = mousePos2.x - mousePos1.x;
                    yDiff = mousePos2.y - mousePos1.y;

                    pbIndex = scriptCollider.index;
                    Debug.Log("on mouse = "+pbIndex);

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
