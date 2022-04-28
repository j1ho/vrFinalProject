using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;


public class SprayPaint : MonoBehaviour
{
    public Transform nozzleDirect;

    private Transform laserTransform;
    private Vector3 hitPoint;

    public ParticleSystem paintParticles;

    [SerializeField] private Material paintMaterial;
    [SerializeField] private string selectableTag = "Paintable";


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

            RaycastHit hit;

            if (Physics.Raycast(nozzleDirect.position, nozzleDirect.forward, out hit, 100))
            {
                hitPoint = hit.point;

                var selection = hit.transform;
                var selectionRenderer = selection.GetComponent<Renderer>();
                selectionRenderer.material = paintMaterial;

                hitPoint = hit.point;
                if(selectionRenderer != null  && selection.CompareTag(selectableTag))
                {
                    ShowLaser(hit);
                }       
                 else
                {
                    paintParticles.Stop();
                }
            }
    }

    private void ShowLaser(RaycastHit hit)
    {
        paintParticles.Play();
    }

}
