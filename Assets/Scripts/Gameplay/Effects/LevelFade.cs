using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelFade : MonoBehaviour {

    public Animator fade;

	// Use this for initialization
	void Start () {
        fade.SetBool("transition", false);
	}

    public void FadeLevel(int lvl)
    {
        fade.SetBool("transition", true);
        SceneManager.LoadScene(lvl);
    }
}
