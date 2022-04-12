using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class SlidingLever : MonoBehaviour
{

    public Transform leverHandle;
    private float leverPosition;
    private float origin;
    private Transform selectedObject;
    private LaserPointer laserScript;
    

    // Start is called before the first frame update
    void Start()
    {
        origin = leverHandle.position.x;

    }

    // Update is called once per frame
    void Update()
    {
        GameObject controllerRight = GameObject.Find("Controller (right)");
        LaserPointer laserScript = controllerRight.GetComponent<LaserPointer>();
        selectedObject = laserScript.selectedObject;
        Debug.Log("selectedObject: " + selectedObject);
        leverPosition = leverHandle.position.x;
        // Debug.Log("leverPosition " + leverPosition);
        if(leverPosition > (origin+(origin*0.001)) ) {
            selectedObject.Translate(Vector3.forward * Time.deltaTime);
        } else if(leverPosition <  (origin-(origin*0.001)) ) {
            selectedObject.Translate(Vector3.back * Time.deltaTime);
        }
    }
}
