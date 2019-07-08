﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour {

    public float WalkingSpeed = 0.01f;
    private InputManager inp;
    private bool LeftDirection = false;
    private GameObject model;
    private WorldManager world;
    private Camera camera;
    private Vector3 offset;
    private PostProcessTweak ppt;
    private Pause _paused;
    public float offsetStart = 0f;

    [HideInInspector]
    public bool blockMove = false;

    [HideInInspector]
    public float moveTo;

    [HideInInspector]
    public bool MoveDir;

	void Start () {
        inp     = GameObject.Find("Movement").GetComponent<InputManager>();
        model   = GameObject.Find("Model");
        world   = GameObject.Find("World").GetComponent<WorldManager>();
        ppt     = GameObject.Find("World").GetComponent<PostProcessTweak>();
        _paused = GameObject.Find("Ingame Menu").GetComponent<Pause>();
        camera  = Camera.main;
        offset  = camera.transform.position - model.transform.position;
    }
	

	void Update () {

        if (offsetStart != 0)
        {
            model.transform.position += new Vector3(offsetStart, 0);
            offsetStart = 0;
        }

        if (blockMove == true)
        {
            if (MoveDir == false) {
                model.transform.position += new Vector3(1, 0) * WalkingSpeed * Time.deltaTime * 50;
                if (LeftDirection)
                {
                    LeftDirection = false;
                    model.transform.localScale = new Vector2(1, 1);
                }
                if (model.transform.position.x >= moveTo){
                    blockMove = false;
                }
            }
            else
            {
                model.transform.position += new Vector3(-1, 0) * WalkingSpeed * Time.deltaTime * 50;
                if (!LeftDirection)
                {
                    LeftDirection = true;
                    model.transform.localScale = new Vector2(-1, 1);
                }
                if (model.transform.position.x <= moveTo){
                    blockMove = false;
                }
            }
        }

        //Camera Bounds
        float screenAspect = (float)Screen.width / (float)Screen.height;
        float camHalfHeight = camera.orthographicSize;
        float camHalfWidth = screenAspect * camHalfHeight;
        float camWidth = 2.0f * camHalfWidth;

        camera.transform.position = model.transform.position + offset;
        if (camera.transform.position.x <= world.CameraBorderLeft + camHalfWidth) camera.transform.position = new Vector3(world.CameraBorderLeft + camHalfWidth, camera.transform.position.y, camera.transform.position.z);
        if (camera.transform.position.x > world.CameraBorderRight - camHalfWidth) camera.transform.position = new Vector3(world.CameraBorderRight - camHalfWidth, camera.transform.position.y, camera.transform.position.z);


        //Pause -> Block Input
        if (_paused.Paused == true || _paused.Message == true || blockMove == true) return;

        //Character Movement
        if (inp.Left)
        {
            if (!LeftDirection)
            {
                LeftDirection = true;
                model.transform.localScale = new Vector2(-1, 1);
            }
            model.transform.position += new Vector3(-1, 0) * WalkingSpeed * Time.deltaTime * 50;
            //ppt.bloom(1f);
        }
        else if(inp.Right)
        {
            if (LeftDirection)
            {
                LeftDirection = false;
                model.transform.localScale = new Vector2(1, 1);
            }
            model.transform.position += new Vector3(1, 0) * WalkingSpeed * Time.deltaTime * 50;
            //ppt.bloom(1f);
        }

        //Character Bounds
        if (model.transform.position.x < world.CameraBorderLeft )
        {
            model.transform.position = new Vector3(world.CameraBorderLeft, model.transform.position.y, model.transform.position.z);
        }

        if (model.transform.position.x > world.CameraBorderRight)
        {
            model.transform.position = new Vector3(world.CameraBorderRight, model.transform.position.y, model.transform.position.z);
        }


        if(world.CharacterBounds == true)
        {
            if (model.transform.position.x <= world.CharacterBoundLeft + camHalfWidth) model.transform.position = new Vector3(world.CharacterBoundLeft + camHalfWidth, model.transform.position.y, model.transform.position.z);
            if (model.transform.position.x > world.CharacterBoundRight - camHalfWidth) model.transform.position = new Vector3(world.CharacterBoundRight - camHalfWidth, model.transform.position.y, model.transform.position.z);
        }

    }
}
