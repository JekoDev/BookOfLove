using System.Collections;
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
    private Pause _paused;
    public float offsetStart = 0f;

    [HideInInspector]
    public bool blockMove = false;

    [HideInInspector]
    public float moveTo;

    [HideInInspector]
    public bool MoveDir;

    [HideInInspector]
    public bool TurnDir;

    [HideInInspector]
    public bool OwnWalk = false;

    private Animator ElliotAnim;
    private SpriteRenderer ElliotSprite;

    [HideInInspector]
    public bool BlockDialogue;
    [HideInInspector]
    public bool BlockDialogueB;
         
	void Start () {
        inp     = GameObject.Find("Movement").GetComponent<InputManager>();
        model   = GameObject.Find("Model");
        world   = GameObject.Find("World").GetComponent<WorldManager>();
        _paused = GameObject.Find("Ingame Menu").GetComponent<Pause>();
        camera  = Camera.main;
        offset  = camera.transform.position - model.transform.position;
        ElliotAnim = model.GetComponent<Animator>();
        ElliotSprite = model.GetComponent<SpriteRenderer>();
    }
	

	void Update () {

        if (offsetStart != 0)
        {
            model.transform.position += new Vector3(offsetStart, 0);
            offsetStart = 0;
        }

        ElliotAnim.SetBool("Walking", false);
        if (blockMove == true)
        {
            if (MoveDir == false)
            {
                if (LeftDirection)
                {
                    LeftDirection = false;
                    ElliotSprite.flipX = false;
                }
                if (Vector3.Distance(model.transform.position, new Vector3(moveTo, model.transform.position.y)) < 0.2f)
                {
                    blockMove = false;
                    if (OwnWalk == true)
                    {
                        if (!LeftDirection && TurnDir == false)
                        {
                            LeftDirection = true;
                            ElliotSprite.flipX = true;
                        }
                    }
                    OwnWalk = false;
                }
                else
                {
                    ElliotAnim.SetBool("Walking", true);
                    model.transform.position += new Vector3(1, 0) * WalkingSpeed * Time.deltaTime * 50;
                }
            }
            else
            {
                if (!LeftDirection)
                {
                    LeftDirection = true;
                    ElliotSprite.flipX = true;
                }
                if (Vector3.Distance(model.transform.position, new Vector3(moveTo, model.transform.position.y)) < 0.2f)
                {
                    blockMove = false;
                    if (OwnWalk == true)
                    {
                        if (LeftDirection && TurnDir == true)
                        {
                            LeftDirection = false;
                            ElliotSprite.flipX = false;
                        }
                    }
                    OwnWalk = false;
                }
                else
                {
                    ElliotAnim.SetBool("Walking", true);
                    model.transform.position += new Vector3(-1, 0) * WalkingSpeed * Time.deltaTime * 50;
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
                ElliotSprite.flipX = true;
            }
            model.transform.position += new Vector3(-1, 0) * WalkingSpeed * Time.deltaTime * 50;
            ElliotAnim.SetBool("Walking", true);
        }
        else if(inp.Right)
        {
            if (LeftDirection)
            {
                LeftDirection = false;
                ElliotSprite.flipX = false;
            }
            ElliotAnim.SetBool("Walking", true);
            model.transform.position += new Vector3(1, 0) * WalkingSpeed * Time.deltaTime * 50;
        }

        //Character Bounds
        if (model.transform.position.x < world.CameraBorderLeft )
        {
            model.transform.position = new Vector3(world.CameraBorderLeft, model.transform.position.y, model.transform.position.z);
            ElliotAnim.SetBool("Walking", false);
        }

        if (model.transform.position.x > world.CameraBorderRight)
        {
            model.transform.position = new Vector3(world.CameraBorderRight, model.transform.position.y, model.transform.position.z);
            ElliotAnim.SetBool("Walking", false);
        }


        if(world.CharacterBounds == true)
        {
            if (model.transform.position.x <= world.CharacterBoundLeft + camHalfWidth){
                ElliotAnim.SetBool("Walking", false);
                model.transform.position = new Vector3(world.CharacterBoundLeft + camHalfWidth, model.transform.position.y, model.transform.position.z);
            }
            if (model.transform.position.x > world.CharacterBoundRight - camHalfWidth){
                model.transform.position = new Vector3(world.CharacterBoundRight - camHalfWidth, model.transform.position.y, model.transform.position.z);
                ElliotAnim.SetBool("Walking", false);
            }
        }

    }
}
