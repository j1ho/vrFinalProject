using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class SpawningLever : MonoBehaviour
{

    public Transform leverHandle;
    private float leverPosition;
    private float origin;
    public Replicator replicator;
    private float spawnSpeed;

    // Start is called before the first frame update
    void Start()
    {
        origin = leverHandle.position.x;
        spawnSpeed = 10.0f;
    }

    // Update is called once per frame
    void Update()
    {
        leverPosition = leverHandle.position.x;

        if(leverPosition > (origin+(origin*0.01)) ) {
            spawnSpeed = 10.0f;
        } 
        else if(leverPosition > (origin+(origin*0.005)) ) {
            spawnSpeed = 6.0f;
        }
        else if(leverPosition > (origin+(origin*0.001)) ) {
            spawnSpeed = 5.0f;
        }
        else if(leverPosition < (origin-(origin*0.01)) ) {
            spawnSpeed = 2.0f;
        }
        else if(leverPosition < (origin-(origin*0.005)) ) {
            spawnSpeed = 3.0f;
        }
        else if(leverPosition < (origin-(origin*0.001)) ) {
            spawnSpeed = 4.0f;
        }
        replicator.setSpawnSpeed(spawnSpeed);
    }
}
