using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;


public class LaserPointer : MonoBehaviour
{

    public SteamVR_Input_Sources handType;
    public SteamVR_Behaviour_Pose controllerPose;
    public SteamVR_Action_Boolean teleportAction;

    public GameObject laserPrefab; // 1
    private GameObject laser; // 2
    private Transform laserTransform; // 3
    private Vector3 hitPoint; // 4

    public Transform cameraRigTransform; 
    public GameObject teleportReticlePrefab;
    private GameObject reticle;
    private Transform teleportReticleTransform; 
    public Transform headTransform; 
    public Vector3 teleportReticleOffset; 
    public LayerMask teleportMask; 
    private bool shouldTeleport; 

    public SteamVR_Action_Boolean gripAction;
    [SerializeField] private Material highlightMaterial;
    [SerializeField] private string selectableTag = "Selectable";
    private Material defaultMaterial;
    // Transform deselectedObject = null;
    private Renderer deselectionRenderer;
    public SteamVR_Action_Boolean thumbPressAction;
    private bool isThumbstickPressed;
    public Transform selectedObject;

    // Start is called before the first frame update
    void Start()
    {
        laser = Instantiate(laserPrefab);
        laserTransform = laser.transform;

        reticle = Instantiate(teleportReticlePrefab);
        teleportReticleTransform = reticle.transform;

        isThumbstickPressed = false;
    }

    // Update is called once per frame
    void Update()
    {
        // 1
        if (teleportAction.GetState(handType))
        {
            RaycastHit hit;

            // 2
            if (Physics.Raycast(controllerPose.transform.position, transform.forward, out hit, 100, teleportMask))
            {
                hitPoint = hit.point;
                ShowLaser(hit);

                reticle.SetActive(true);
                teleportReticleTransform.position = hitPoint + teleportReticleOffset;
                shouldTeleport = true;
            }
        } else if (gripAction.GetState(handType))
        {
            RaycastHit hit;

            if (Physics.Raycast(controllerPose.transform.position, transform.forward, out hit, 100))
            {
                hitPoint = hit.point;
                ShowLaser(hit);
                var selection = hit.transform;
                var selectionRenderer = selection.GetComponent<Renderer>();
                if(!isThumbstickPressed){
                    if(deselectionRenderer != null && defaultMaterial != null) {
                        deselectionRenderer.material = defaultMaterial;
                        deselectionRenderer = null;
                        defaultMaterial = null;
                    }
                    if(selectionRenderer != null  && selection.CompareTag(selectableTag))
                    {
                        if(deselectionRenderer == null && defaultMaterial == null && selectionRenderer.material != highlightMaterial){
                            defaultMaterial = selectionRenderer.material;
                            deselectionRenderer = selection.GetComponent<Renderer>();
                        }
                        selectionRenderer.material = highlightMaterial;
                    } 
                }

            }
        }
        else if (thumbPressAction.GetState(handType))
        {
            RaycastHit hit;

            if (Physics.Raycast(controllerPose.transform.position, transform.forward, out hit, 100))
            {
                hitPoint = hit.point;
                ShowLaser(hit);
                var selection = hit.transform;
                var selectionRenderer = selection.GetComponent<Renderer>();
                if(deselectionRenderer != null && defaultMaterial != null) {
                    deselectionRenderer.material = defaultMaterial;
                    deselectionRenderer = null;
                    defaultMaterial = null;
                    isThumbstickPressed = false;
                    selectedObject = null;
                }
                if(selectionRenderer != null  && selection.CompareTag(selectableTag))
                {
                    if(deselectionRenderer == null && defaultMaterial == null && selectionRenderer.material != highlightMaterial){
                        defaultMaterial = selectionRenderer.material;
                        deselectionRenderer = selection.GetComponent<Renderer>();
                    }
                    selectionRenderer.material = highlightMaterial;
                    isThumbstickPressed = true;
                    selectedObject = selection;
                    // Debug.Log("selectedObject: " + selectedObject);
                } 

            }
        }
        else // 3
        {
            laser.SetActive(false);
            reticle.SetActive(false);
        }

        if (teleportAction.GetStateUp(handType) && shouldTeleport)
        {
            Teleport();
        }

    }

    private void ShowLaser(RaycastHit hit)
    {
        // 1
        laser.SetActive(true);
        // 2
        laserTransform.position = Vector3.Lerp(controllerPose.transform.position, hitPoint, .5f);
        // 3
        laserTransform.LookAt(hitPoint);
        // 4
        laserTransform.localScale = new Vector3(laserTransform.localScale.x,
                                                laserTransform.localScale.y,
                                                hit.distance);
    }

    private void Teleport()
    {
        // 1
        shouldTeleport = false;
        // 2
        reticle.SetActive(false);
        // 3
        Vector3 difference = cameraRigTransform.position - headTransform.position;
        // 4
        difference.y = 0;
        // 5
        cameraRigTransform.position = hitPoint + difference;
    }

}
