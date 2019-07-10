using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelFade : MonoBehaviour {

    public Animator fade;
    private bool trigger = false;
    private int level = 0;

	// Use this for initialization
	void Start () {
        fade.SetBool("transition", false);
	}

    public void FadeLevel(int lvl)
    {
       fade.SetBool("transition", true);
        level = lvl;
        trigger = true;
    }

    public void Update()
    {
        if (trigger == true && fade.GetCurrentAnimatorStateInfo(0).IsName("SceneOut"))
        { 
            SceneManager.LoadScene(level);
        }
    }
}
