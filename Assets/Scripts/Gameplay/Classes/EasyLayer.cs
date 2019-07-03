using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EasyLayer : MonoBehaviour
{

    private float BorderLeft = 0f;
    public float BorderRight = 0f;
    public float verticalAdjust = 0f;

    private GameObject worldObject, wOGizmo;
    private float HelperGroundLine = 0f;
    private Camera camera;
    private float cbr;

    public bool LowerButScrollIndeed = false;

    void Start()
    {
        //Move Layer to the correct position
        worldObject = GameObject.Find("World");
        this.transform.position = worldObject.transform.position;
        this.transform.position += new Vector3(this.GetComponent<Renderer>().bounds.size.x/2, -verticalAdjust);
        cbr = worldObject.GetComponent<WorldManager>().CameraBorderRight;

        camera = Camera.main;

        //Children should be on same layer
        foreach (Transform child in transform)
        {
            if (child.GetComponent<SpriteRenderer>() != null) { 
                child.GetComponent<SpriteRenderer>().sortingLayerName = this.GetComponent<SpriteRenderer>().sortingLayerName;
            }

            child.gameObject.layer = this.gameObject.layer;
        }
    }


    //Parallalalalax
    void Update()
    {
         //Camera Bounds
        float screenAspect = (float)Screen.width / (float)Screen.height;
        float camHalfHeight = camera.orthographicSize;
        float camHalfWidth = screenAspect * camHalfHeight;
        float camWidth = 2.0f * camHalfWidth;

        float progress = (camera.transform.position.x - camWidth/2) / (cbr);

        if (BorderRight > cbr)
        {
            this.transform.position = worldObject.transform.position;
            this.transform.position += new Vector3(this.GetComponent<Renderer>().bounds.size.x / 2, -verticalAdjust);
            this.transform.position -= new Vector3(progress * (BorderRight - cbr), 0f);
        }

        if (BorderRight < cbr && LowerButScrollIndeed == true)
        {
            progress = (camera.transform.position.x - camWidth / 2) / (cbr);
            this.transform.position = worldObject.transform.position;
            this.transform.position += new Vector3(this.GetComponent<Renderer>().bounds.size.x / 2, -verticalAdjust);
            this.transform.position -= new Vector3(progress * (BorderRight - cbr), 0f);
        }
    }


    void OnDrawGizmos()
    {
        if (wOGizmo == null) wOGizmo = GameObject.Find("World");
    

        Gizmos.color = Color.blue;
        Gizmos.DrawLine(new Vector3(BorderLeft, 50),  new Vector3(BorderLeft, -150));
        Gizmos.DrawLine(new Vector3(BorderRight, 50), new Vector3(BorderRight, -150));

        if (wOGizmo != null)
        {
            HelperGroundLine = wOGizmo.GetComponent<WorldManager>().HelperGroundLine + this.transform.position.y + verticalAdjust;
            Gizmos.color = Color.magenta;
            float r = wOGizmo.GetComponent<WorldManager>().CameraBorderRight;
            Gizmos.DrawLine(new Vector3(r, 50), new Vector3(r, -150));
        }

        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(new Vector3(BorderLeft, HelperGroundLine), new Vector3(BorderRight, HelperGroundLine));

        
    }
}

