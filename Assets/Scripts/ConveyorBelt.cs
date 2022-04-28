using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ConveyorBelt : MonoBehaviour
{

    public float speed;
    public Vector3 direction;
    public List<GameObject> onBelt;

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
        rotator1.transform.eulerAngles = new Vector3(0, 0, rotate);
        rotator2.transform.eulerAngles = new Vector3(0, 0, rotate);
        for (int i = 0; i <= onBelt.Count - 1; i++)
        {
            onBelt[i].GetComponent<Rigidbody>().transform.Translate(speed * direction * Time.deltaTime);
            //print(onBelt[i].GetComponent<Rigidbody>().velocity);
        }
        rotate -= 0.5f;
        if(rotate <= 0)
        {
            rotate = 359;
        }
    }

    //When something collides with the onBelt
    private void OnCollisionEnter(Collision collision)
    {
        onBelt.Add(collision.gameObject);
        
    }

    //When something leaves the belt
    private void OnCollisionExit(Collision collision)
    {
        collision.gameObject.GetComponent<Rigidbody>().velocity = speed * direction * Time.deltaTime * 50;
        onBelt.Remove(collision.gameObject);
    }


}
