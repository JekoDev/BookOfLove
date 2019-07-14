using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Save : MonoBehaviour {

    private float time;
    private Vector3 offset;

	// Use this for initialization
	void Start () {
        time = Time.time;
        offset = this.transform.position - Camera.main.transform.position;
    }
	
	// Update is called once per frame
	void Update () {
        this.transform.position = Camera.main.transform.position + offset;
        if (Time.time - time > 2.5f)
        {
            Destroy(this.gameObject);
        }
	}
}
