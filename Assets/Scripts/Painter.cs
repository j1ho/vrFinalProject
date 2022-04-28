using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Painter : MonoBehaviour
{
    public Color color;
    public MeshRenderer[] colorObjects;

    public ParticleSystem blast;

    public List<GameObject> blasted;

    void Start()
    {
        for (int i = 0; i < colorObjects.Length; i++)
        {
            colorObjects[i].material.color = color;
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void shootColor()
    {
        blast.Play();
        for(int i = 0; i < blasted.Count; i++)
        {
            if (blasted[i].gameObject.tag == "Paintable")
            {
                blasted[i].GetComponent<MeshRenderer>().material.color += color;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Paintable")
        {
            blasted.Add(other.gameObject);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        blasted.Remove(other.gameObject);
    }
}
