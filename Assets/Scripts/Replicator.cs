using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Replicator : MonoBehaviour
{
    public GameObject sobject;
    public Transform spawnArea;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //spawnObject();
    }

    public void spawnObject()
    {
        Instantiate(sobject, spawnArea);
    }
}