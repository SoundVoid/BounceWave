using UnityEngine;
using System.Collections;

public class controller : MonoBehaviour {

	private Rigidbody rb;
	public float moveSpeed;
	public float jumpStrength;

	public bool playerToggle = true;

	public bool grounded = false;

	void Start () {
	
		rb = GetComponent<Rigidbody>();

	}
		
	void Update () {

		if (playerToggle) {
			if (Input.GetKey (KeyCode.A))
				transform.RotateAround (transform.position, transform.up, -5);

			if (Input.GetKey (KeyCode.D))
				transform.RotateAround (transform.position, transform.up, 5);
		} else {
			if (Input.GetKey (KeyCode.J))
				transform.RotateAround (transform.position, transform.up, -5);

			if (Input.GetKey (KeyCode.L))
				transform.RotateAround (transform.position, transform.up, 5);
		}


	
	}

	void OnCollisionEnter(Collision col)
	{
		if (col.collider.tag == "Ground") grounded = true;
	}

	void OnCollisionStay(Collision col)
	{
		if (col.collider.tag == "Ground") grounded = true;
	}

	void OnCollisionExit(Collision col)
	{
		if (col.collider.tag == "Ground") grounded = false;
	}

	//FixedUpdate is called once per physics step
	void FixedUpdate()
	{
		if (playerToggle) {
			if (Input.GetKeyDown (KeyCode.LeftCommand)) {
				if (grounded) {
					rb.AddForce (transform.up * jumpStrength, ForceMode.VelocityChange);
				}
			}

			if (Input.GetKey (KeyCode.W))
				rb.AddForce (transform.forward * moveSpeed);

			if (Input.GetKey (KeyCode.S))
				rb.AddForce (-transform.forward * moveSpeed);
		} else {
			if (Input.GetKeyDown (KeyCode.RightCommand)) {
				if (grounded) {
					rb.AddForce (transform.up * jumpStrength, ForceMode.VelocityChange);
				}
			}

			if (Input.GetKey (KeyCode.I))
				rb.AddForce (transform.forward * moveSpeed);

			if (Input.GetKey (KeyCode.K))
				rb.AddForce (-transform.forward * moveSpeed);
		}

	}
}
