using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldManager : MonoBehaviour {

    [HideInInspector]
    public float CameraBorderLeft  = 0f;
    public float CameraBorderRight = 0f;

    public float HelperGroundLine = 80f;

    public bool DrawCameraHelper = true;
    public float CameraHelper = 0f;
    private Camera camera;

    void OnDrawGizmos()
    {
        camera = Camera.main;

        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(camera.transform.position + new Vector3(CameraHelper,0), new Vector3(3f,15f));

        Gizmos.color = Color.blue;
        Gizmos.DrawLine(new Vector3(CameraBorderLeft,  50),   new Vector3(CameraBorderLeft,  -150));
        Gizmos.DrawLine(new Vector3(CameraBorderRight, 50),   new Vector3(CameraBorderRight, -150));

        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(new Vector3(CameraBorderLeft, HelperGroundLine + this.transform.position.y), new Vector3(CameraBorderRight, HelperGroundLine + this.transform.position.y));



    }

    void Start()
    {
        Destroy(GameObject.Find("Helpers"));    
    }
}

