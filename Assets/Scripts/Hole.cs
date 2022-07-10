using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using static GameManager;
using static StaticDefinitions;

public class Hole : MonoBehaviour
{
    public GameObject gameObjectOfGate;
    public GameObject gameObjectOfHoleParent;
    public Camera MainCamera;
    public GameObject sensor;
    float timerForGoToSecondGround = 0.1f, holeWaitingWhileOpeningTheGate = 0.5f;
    Touch touch;
   
    

    void Start()
    {
        
        transform.position = new Vector3(BEGIN_XPOS_OF_HOLE, 0.01f, 0);
        
    }

    void FixedUpdate()
    {
        
        if (Input.touchCount > 0)
        {
            touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Moved)
            {
                if(checkBorder(GameManager.Instance.openGateFlag) == true)
                {

                transform.position = new Vector3(transform.position.x + touch.deltaPosition.y * 0.01f, transform.position.y, 
                                                transform.position.z - touch.deltaPosition.x * 0.01f);

                }

            }
        }

        if (GameManager.Instance.openGateFlag && transform.position.x < BEGIN_POSITION_OF_SECOND_GROUND)
        {

            holeWaitingWhileOpeningTheGate -= Time.deltaTime;
            timerForGoToSecondGround -= Time.deltaTime;

            if (holeWaitingWhileOpeningTheGate < Time.deltaTime)
            {
                if (timerForGoToSecondGround <= Time.deltaTime)
                {
                    // Normalized the z position.
                    if (transform.position.z != 0)
                    {
                        transform.position = new Vector3(transform.position.x, transform.position.y, -0.15f);
                    }

                    transform.position += new Vector3(SPEED_OF_HOLE, 0, 0);
                    MainCamera.transform.position += new Vector3(SPEED_OF_CAMERA, 0, 0);
                    GameManager.Instance.isCameraMoveToSecondGround = true;
                    timerForGoToSecondGround = 0.1f;
                }
                holeWaitingWhileOpeningTheGate = 0f;
                GameManager.Instance.currentPosOfCamera = MainCamera.transform.position;
            }
            
        }


    }

    private bool checkBorder(bool _openGateFlag) 
    {

        if (!GameManager.Instance.openGateFlag)
        {

            if (transform.position.x < FG_BACK_BORDER ) 
            {
                transform.position += new Vector3(0.04f, 0, 0); 
                return false;
            }
            else if (transform.position.x > FG_TOP_BORDER) 
            {
                transform.position += new Vector3(-0.04f, 0, 0); 
                return false;
            }
            else if (transform.position.z < FG_RIGHT_BORDER) 
            {
                transform.position += new Vector3(0, 0, 0.04f);
                return false;
            }
            else if (transform.position.z > FG_LEFT_BORDER ) 
            {
                transform.position += new Vector3(0, 0, -0.04f);
                return false;
            }
        }
        else
        {
            if (transform.position.x < SG_BACK_BORDER)
            {
                transform.position += new Vector3(0.04f, 0, 0);
                return false;
            }
            else if (transform.position.x > SG_TOP_BORDER)
            {
                transform.position += new Vector3(-0.04f, 0, 0);
                return false;
            }
            else if (transform.position.z < SG_RIGHT_BORDER)
            {
                transform.position += new Vector3(0, 0, 0.04f);
                return false;
            }
            else if (transform.position.z > SG_LEFT_BORDER)
            {
                transform.position += new Vector3(0, 0, -0.04f);
                return false;
            }
            
            
        }
        return true;
    }
}
