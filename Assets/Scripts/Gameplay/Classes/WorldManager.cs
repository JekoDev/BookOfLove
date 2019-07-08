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

    public bool CharacterBounds = false;
    public float CharacterBoundLeft = 0f;
    public float CharacterBoundRight = 0f;

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

        if (CharacterBounds == true) { 
            Gizmos.color = Color.red;
            Gizmos.DrawLine(new Vector3(CharacterBoundLeft + 6f, 50), new Vector3(CharacterBoundLeft + 6f, -150));
            Gizmos.DrawLine(new Vector3(CharacterBoundRight - 9f, 50), new Vector3(CharacterBoundRight - 9f, -150));
        }
    }

    void Start()
    {
        Destroy(GameObject.Find("Helpers"));    
    }
}

