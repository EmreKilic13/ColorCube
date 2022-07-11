using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/*
 * Sensor object is an propert that is added to Hole object.
 * This object is used for detecting obstacle in the scene. 
 * If Sensor object approaches to the valid obstacle object, 
 * the RigidBody property is added to valid obstacle object.
 */

public class Sensor : MonoBehaviour
{
    int speedOfRay = StaticDefinitions.SENSOR_FREQUENCY;
    float distanceOfRay = StaticDefinitions.SENSOR_RAY_DISTANCE;

    /* Represents the layer of the obstacle. */
    LayerMask targetLayer;

    RaycastHit hit;

    delegate void DetectValidObstacle(GameObject go);
    DetectValidObstacle detective;

    void Start()
    {
        targetLayer = LayerMask.GetMask(StaticDefinitions.OBSTACLE_LAYER_NAME);

        /* It is used to access the child object of the parent object given as a parameter to the function. */
        detective += GameManager.Instance.getInnerObject;
    }

    void FixedUpdate()
    {
        transform.RotateAround(transform.position, transform.up, Time.deltaTime * speedOfRay);
        
        if (Physics.Raycast(transform.position, transform.forward, out hit, distanceOfRay, targetLayer))
        {
            Debug.DrawLine(transform.position, hit.point, Color.white);
            if (hit.transform.parent.gameObject.layer == Mathf.Log(targetLayer.value, 2))
            {
                detective(hit.transform.parent.gameObject);
            } 
            else if(hit.transform.gameObject.layer == Mathf.Log(targetLayer.value, 2) && hit.transform.childCount == 0)
            {
                detective(hit.transform.gameObject);
            }
                
        }
    }
}
