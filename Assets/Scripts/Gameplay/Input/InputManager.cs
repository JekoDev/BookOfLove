using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InputManager : MonoBehaviour {

    public GameObject _cursor;


    public bool Left;
    public bool Right;
    public bool Top;
    public bool Down;
    public bool Action;
    public bool Pause;
    public int  PointX;
    public int  PointY;
    public bool PointLeft;
    public bool PointRight;
    public Vector2 pos;

    public float gamepadSensitivity = 9f;
    public bool gamepad;
    private Vector2 curpos;

    public bool StickTop;
    public bool StickDown;
    public bool StickRight;
    public bool StickLeft;

    private quit q;


	void Start () {
        Reset();
        GameObject cache = GameObject.Find("quitgame");
        if (cache != null) q = cache.GetComponent<quit>();
    }
	
	void Update () {
        Cursor.visible = false;
        Reset();

        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))   Left  = true;
        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))  Right = true;
        if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))   Down  = true;
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))     Top   = true;

        if (Input.GetKeyDown(KeyCode.Space)  || Input.GetKeyDown(KeyCode.Return)) Action   = true;
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (q != null)
            {
                if (Pause == false)
                {
                    q.QuitTrigger = true;
                    //Pause = true;
                }else {
                    q.QuitTrigger = true;
                    //Pause = false;
                }

            }
        }

        if (Input.GetKeyDown(KeyCode.Mouse0))  PointLeft    = true;
        if (Input.GetKeyDown(KeyCode.Mouse1))  PointRight   = true;

        if (Left || Right || Down || Top || Action || Pause || PointLeft || PointRight) gamepad = false;
        if (Input.GetButtonDown("AButton")) {     gamepad = true; Action = true; }
        if (Input.GetButtonDown("BButton")) {     gamepad = true; Action = true; }
        if (Input.GetButtonDown("PauseButton")) { gamepad = true; Pause = true; }
        if (Input.GetButtonDown("XButton")) {     gamepad = true; PointLeft  = true; }
        if (Input.GetButtonDown("YButton")) {     gamepad = true; PointRight = true; }
        

        if (Input.GetAxis("MoveX") != 0f || Input.GetAxis("MoveY") != 0f)
        {
            
            gamepad = true;
            if (Input.GetAxis("MoveX") < 0) Left  = true;
            if (Input.GetAxis("MoveX") > 0) Right = true;
            if (Input.GetAxis("MoveY") < 0) Top   = true;
            if (Input.GetAxis("MoveY") > 0) Down  = true;
        }

        if (Input.GetAxis("Horizontal") != 0f || Input.GetAxis("Vertical") != 0f)
        {

            gamepad = true;
            if (Input.GetAxis("Horizontal") < 0) StickLeft = true;
            if (Input.GetAxis("Horizontal") > 0) StickRight = true;
            if (Input.GetAxis("Vertical") < 0)   StickTop = true;
            if (Input.GetAxis("Vertical") > 0)   StickDown = true;
        }

        if (!gamepad)
        {
            pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            curpos = Input.mousePosition;
        }
        else
        {
            float dirHor = (StickLeft ? 1 : 0) * -1 + (StickRight ? 1 : 0) *  1;
            float dirVer = (StickTop  ? 1 : 0) *  1 + (StickDown  ? 1 : 0) * -1;
            dirHor *= gamepadSensitivity;
            dirVer *= gamepadSensitivity;

            curpos += new Vector2(dirHor, dirVer);
            pos = Camera.main.ScreenToWorldPoint(curpos);
        }

        _cursor.transform.position = pos;
    }

    void Reset()
    {
        Left       = false;
        Right      = false;
        Top        = false;
        Down       = false;
        Action     = false;
        Pause      = false;
        PointLeft  = false;
        PointRight = false; 
        StickTop   = false; 
        StickDown  = false; 
        StickLeft  = false; 
        StickRight = false; 
    }
}
