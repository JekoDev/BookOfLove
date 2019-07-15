using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class quitButton : MonoBehaviour {

    private bool MouseIn;
    private InputManager inp;
    private quit q;


    private void Start()
    {
        inp = GameObject.Find("Movement").GetComponent<InputManager>();
        q = GameObject.Find("quitgame").GetComponent<quit>();
    }

    private void Update()
    {
        if (MouseIn == true && inp.PointLeft)
        {
            if (this.name == "QYes") SceneManager.LoadScene(0);
            if (this.name == "QNo") {
                q.QuitTrigger = true;
                inp.Pause = false;
                
            }  
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.name == "cursor") MouseIn = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.name == "cursor") MouseIn = false;
    }

}

