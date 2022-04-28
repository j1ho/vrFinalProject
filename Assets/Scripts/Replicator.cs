using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Replicator : MonoBehaviour
{
    public GameObject sobject;
    public Transform spawnArea;
    private float spawnSpeed;
    // Start is called before the first frame update
    void Start()
    {
        spawnSpeed = 10.0f;
    }

    // Update is called once per frame
    void Update()
    {
        //spawnObject();
    }

    public void spawnObject()
    {
        Instantiate(sobject, spawnArea.position, spawnArea.rotation);
    }

    public void setSpawnSpeed(float spawnSpeed)
    {
        if(this.spawnSpeed != spawnSpeed){
            this.spawnSpeed = spawnSpeed;
            CancelInvoke();
            InvokeRepeating("spawnObject", spawnSpeed*0.9f, spawnSpeed);
            Debug.Log("InvokeRepeating spawnSpeed: " + spawnSpeed);
        }
    }
}
