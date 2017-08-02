using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class trigger : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		int inc = 0;
		if (Input.GetKey (KeyCode.Space)) {
			inc++;
			transform.localScale = new Vector3 (inc,1,inc);
		}
	}
}
