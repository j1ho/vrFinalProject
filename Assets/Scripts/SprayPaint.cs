using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;


public class SprayPaint : MonoBehaviour
{
    public Transform nozzleDirect;

    public GameObject laserPrefab; // 1
    private GameObject laser; // 2
    private Transform laserTransform; // 3
    private Vector3 hitPoint; // 4

    [SerializeField] private Material paintMaterial;
    [SerializeField] private string selectableTag = "Paintable";


    // Start is called before the first frame update
    void Start()
    {
        laser = Instantiate(laserPrefab);
        laserTransform = laser.transform;

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
                    laser.SetActive(false);
                }
            }
    }

    private void ShowLaser(RaycastHit hit)
    {
        // 1
        laser.SetActive(true);
        // 2
        laserTransform.position = Vector3.Lerp(nozzleDirect.position, hitPoint, .5f);
        // 3
        laserTransform.LookAt(hitPoint);
        // 4
        laserTransform.localScale = new Vector3(laserTransform.localScale.x,
                                                laserTransform.localScale.y,
                                                hit.distance);
    }

}
