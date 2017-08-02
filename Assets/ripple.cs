using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ripple : MonoBehaviour {

	private int[] buffer1;
	private int[] buffer2;
	public int[] blockIndices;

	private int[] vertices;
	private Vector3[] positions;

	public float dampner = 0.999f;
	public float maxWaveHeight = 2.0f;

	public int splashForce = 100;

	private bool swap = true;

	public wave w;


	// Use this for initialization
	void Start () {
		positions = new Vector3[w.n];
		for (int i = 0; i < w.arena.Count; i++){
			positions [i] = w.arena [i].transform.position;
		}
		buffer1 = new int[positions.Length];
		buffer2 = new int[positions.Length];

		float xStep = (w.arena [w.n].transform.position.x - w.arena [0].transform.position.x) / w.n;
		float zStep = (w.arena [w.n].transform.position.z - w.arena [0].transform.position.z) / w.n;

		blockIndices = new int[positions.Length];

		for (int i = 0; i < positions.Length; i++) {
			float colum = ((positions [i].x - w.arena [w.n].transform.position.x) / xStep);
			float row = ((positions [i].z - w.arena [w.n].transform.position.z) / zStep);
			float pos = (row * (w.n + 1)) + colum + 0.5f;
			if (blockIndices [(int)pos] >= 0)
				print ("smash");
			blockIndices [(int)pos] = i;
		}
		splashAtPoint (w.n / 2, w.n / 2);

	}

	void splashAtPoint(int x, int y) {
		int position = ((y * (w.n + 1)) + x);
		buffer1[position] = splashForce;
		buffer1[position - 1] = splashForce;
		buffer1[position + 1] = splashForce;
		buffer1[position + (w.n + 1)] = splashForce;
		buffer1[position + (w.n + 1) + 1] = splashForce;
		buffer1[position + (w.n + 1) - 1] = splashForce;
		buffer1[position - (w.n + 1)] = splashForce;
		buffer1[position - (w.n + 1) + 1] = splashForce;
		buffer1[position - (w.n + 1) - 1] = splashForce;
	}

	void checkInput() {	
		//if (Input.GetMouseButton (0)) {
		if (Input.GetKey(KeyCode.Space)) {
			//RaycastHit hit;
			//if (Physics.Raycast (Camera.main.ScreenPointToRay(Input.mousePosition), out hit)) {
				float xStep = (w.arena [w.n].transform.position.x - w.arena [0].transform.position.x)/w.n;
				float zStep = (w.arena [w.n].transform.position.z - w.arena [0].transform.position.z)/w.n;
			float xCoord = (w.arena [w.n].transform.position.x - w.arena [0].transform.position.x) - ((w.arena [w.n].transform.position.x - w.arena [0].transform.position.x) * transform.position.x);
			float zCoord = (w.arena [w.n].transform.position.z - w.arena [0].transform.position.z) - ((w.arena [w.n].transform.position.z - w.arena [0].transform.position.z) * transform.position.y);
				float column = (xCoord/xStep);// + 0.5;
				float row = (zCoord/zStep);// + 0.5;
				splashAtPoint((int)column,(int)row);
			//}
		}
	}

	void processRipples(int[] source, int[] dest) {
		int x = 0;
		int y  = 0;
		int position = 0;
		for ( y = 1; y < w.n - 1; y ++) {
			for ( x = 1; x < w.n ; x ++) {
				position = (y * (w.n + 1)) + x;
				dest [position] = (((source[position - 1] + 
					source[position + 1] + 
					source[position - (w.n + 1)] + 
					source[position + (w.n + 1)]) >> 1) - dest[position]);  
				dest[position] = (int)(dest[position] * dampner);
			}			
		}	
	}
	
	// Update is called once per frame
	void Update () {
		checkInput();

		int[] currentBuffer;
		if (swap) {
			// process the ripples for this frame
			processRipples(buffer1,buffer2);
			currentBuffer = buffer2;
		} else {
			processRipples(buffer2,buffer1);		
			currentBuffer = buffer1;
		}
		swap = !swap;
		// apply the ripples to our buffer
		Vector3[] theseVertices = new Vector3[vertices.Length];
		int vertIndex;
		for (int i = 0; i < currentBuffer.Length; i++)
		{
			vertIndex = blockIndices[i];
			//theseVertices[vertIndex] = vertices[vertIndex];
			theseVertices[vertIndex].y +=  (currentBuffer[i] * 1.0f/splashForce) * maxWaveHeight;
		}
		for (int j = 0; j < w.arena.Count; j++){
			w.arena [j].transform.position = theseVertices[j];
		}

	}

}
