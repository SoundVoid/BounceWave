using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class generateTerrain : MonoBehaviour {

	public int  n;
	public GameObject block;
	//public wave block;
	public int size;

	public List<List<GameObject>> arena;
	public List<GameObject> temp;

	// Use this for initialization
	void Start () {
		size = n * n;
		//arena = new List<GameObject> ();
		for (int x = 0; x < n; x++) {
			for (int z = 0; z < n; z++) {
				GameObject b = Instantiate (block, new Vector3 (x, 0, z), Quaternion.identity) as GameObject;
				//block.terrain = transform.parent;
				temp.Add (b);
				b.transform.parent = transform;
			}
			arena.Add (temp);
			temp.Clear ();
		}
	}
}
