using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ConveyorBelt : MonoBehaviour
{

    public float speed;
    public Vector3 direction;

    public GameObject endpoint;

    public GameObject rotator1;
    public GameObject rotator2;
    private float rotate;
    // Start is called before the first frame update
    void Start()
    {
        rotate = 359;
    }

    // Update is called once per frame
    void Update()
    {
        rotator1.transform.localEulerAngles = new Vector3(0, 0, rotate);
        rotator2.transform.localEulerAngles = new Vector3(0, 0, rotate);
        rotate -= 0.5f;
        if(rotate <= 0)
        {
            rotate = 359;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        collision.gameObject.GetComponent<Rigidbody>().velocity = speed * direction * Time.deltaTime * 50;
    }

    private void OnTriggerStay(Collider other)
    {
        other.transform.position = Vector3.MoveTowards(other.transform.position, endpoint.transform.position, speed * Time.deltaTime);
    }


}
