using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class quit : MonoBehaviour {


    private bool trigger = false;
    public bool  QuitTrigger = false;

    public GameObject Y, N;

    private Movement mov;
    private Vector3 offset;

    private void Start()
    {
        offset = this.transform.position - Camera.main.transform.position;
        mov = GameObject.Find("Character").GetComponent<Movement>();
    }


    // Update is called once per frame
    void Update () {
        this.transform.position = Camera.main.transform.position + offset;

        if (QuitTrigger == true) { 
            if (trigger == false)
            {
                trigger = true;
                this.GetComponent<SpriteRenderer>().enabled = true;
                Y.SetActive(true);
                N.SetActive(true);
                mov.BlockDialogueC = true;
    
            }else {
                trigger = false;
                this.GetComponent<SpriteRenderer>().enabled = false;
                Y.SetActive(false);
                N.SetActive(false);
                mov.BlockDialogueC = false;
            }
            QuitTrigger = false;
        }
    }
}
