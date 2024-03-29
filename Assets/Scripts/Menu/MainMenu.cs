﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine.Video;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;


public class MainMenu : MonoBehaviour {

    VideoPlayer start;
    GameObject secondVideo;
    GameObject firstVideo;
    public GameObject ui;

    public bool skip;
    public bool trigger;

    public Button m_1Button, m_2Button, m_3Button;
    public GameObject inst;

    private bool triggerNew = false;
    private float timer;

    // Use this for initialization
    void Start () {
        if (skip != true) skip = GameM.skipintro;

        secondVideo = GameObject.Find("Menu1");
        firstVideo  = GameObject.Find("Menu0");
        start = GameObject.Find("Menu0").GetComponent<VideoPlayer>();
        secondVideo.GetComponent<VideoPlayer>().Prepare();

        m_1Button.onClick.AddListener(Newgame);
        m_2Button.onClick.AddListener(Continuegame);
        m_3Button.onClick.AddListener(Quit);

        
    }
	
	// Update is called once per frame
	void Update () {
		if((trigger == false && start.isPlaying == false) || (skip == true && trigger == false))
        {
            trigger = true;
            firstVideo.SetActive(false);
            secondVideo.SetActive(true);
            secondVideo.GetComponent<VideoPlayer>().Play();
            ui.SetActive(true);
            if (!GameM.IsLoad())
            {
                GameObject.Find("Quit").transform.position += new Vector3(0f, 0.70f);
                GameObject.Find("Continue").SetActive(false);
            }
        }

        if (triggerNew == true && Time.time - timer > 5f) 
        {
            SceneManager.LoadScene(1);
        }
	}



    void Newgame() {
        ui.SetActive(false);
        secondVideo.SetActive(false);
        inst.GetComponent<SpriteRenderer>().enabled = true;
        triggerNew = true;
    }


    void Continuegame() { GameM.LoadGame(); }

    void Quit() {
        #if UNITY_EDITOR
                // Application.Quit() does not work in the editor so
                // UnityEditor.EditorApplication.isPlaying need to be set to false to end the game
                UnityEditor.EditorApplication.isPlaying = false;
        #else
                 Application.Quit();
        #endif
    }


    }
