using System.Collections;
using System.Collections.Generic;
using UnityEngine.Video;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Credits : MonoBehaviour {
    VideoPlayer start;

    // Use this for initialization
    void Start () {
        start = GameObject.Find("Outro").GetComponent<VideoPlayer>();
    }
	
	// Update is called once per frame
	void Update () {
        if (!start.isPlaying)
        {
            GameM.skipintro = true;
            SceneManager.LoadScene(0);
        }
	}
}
