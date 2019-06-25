using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pause : MonoBehaviour {
    private InputManager inp;
    public bool Paused = false;
    public bool Message = false;

    void Start () {
        inp = GameObject.Find("Movement").GetComponent<InputManager>();
    }
	

	void Update () {
		if (inp.Pause)
        {
            _Pause(Paused);
            Paused = !Paused;
        }
	}

    void _Pause(bool state)
    {
        if (state == true)
        {
            Time.timeScale = 0f;
        }
        else
        {
            Time.timeScale = 1f;
        }
    }
}
