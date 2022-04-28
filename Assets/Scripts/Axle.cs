using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Axle : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void updateAxle(float angle)
    {
        transform.eulerAngles = new Vector3(0, angle, 0);
    }
}
