using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class wave : MonoBehaviour {

	public float scale;
	public float height;

	public int  n;
	public GameObject block;

	public List<GameObject> arena;

//	public List<List<GameObject>> arena;
//	public List<GameObject> temp;

	void Start () {
		//arena = new List<GameObject> ();
		for (int i = 0; i < n; i++) {
			for (int j = 0; j < n; j++) {
				GameObject b = Instantiate (block, new Vector3 (i, Random.value, j), Quaternion.identity) as GameObject;
				arena.Add (b);
				b.transform.parent = transform;

			}
		}

//		for (int x = 0; x < n; x++) {
//			for (int z = 0; z < n; z++) {
//				GameObject b = Instantiate (block, new Vector3 (x, 0, z), Quaternion.identity) as GameObject;
//				//block.terrain = transform.parent;
//				temp.Add (b);
//				b.transform.parent = transform;
//			}
//			arena.Add (temp);
//			temp.Clear ();
//		}
	}

	void Update () {
		for (int i = 0; i < arena.Count; i = (int)Random.Range(0, n*n +1)) {
			arena[i].transform.position = new Vector3 (arena[i].transform.position.x, height * Mathf.PerlinNoise (Time.time * scale, 0f), arena[i].transform.position.z);

		}
		/*
		foreach (Transform child in transform) {
			//child.transform.position = new Vector3 (child.transform.position.x, height * Random.value, child.transform.position.z);
			child.transform.position = new Vector3 (child.transform.position.x, height * Mathf.PerlinNoise (Time.time * scale, 0f), child.transform.position.z);

			//child.transform.position = new Vector3 (child.transform.position.x, height * Mathf.Sin (Time.time + (transform.position.x * scale)), child.transform.position.z);
			//child.transform.position.y = height * Mathf.PerlinNoise (Time.time + (transform.position.x * scale), Time.time + (transform.position.z + scale));
		}
		*/
	}
}
