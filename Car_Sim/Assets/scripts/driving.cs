using UnityEngine;
using System.Collections;

public class driving : MonoBehaviour 
{
	public float drivingSpeed;
	public float turningSpeed = 30;
	

	// Use this for initialization
	void Start () { }
	
	
	// Update is called once per frame
	void Update () 
	{	
		print ("Speed " + drivingSpeed);
		
		//move forward
		if (Input.GetKey("mouse 0")) 
		{
			rigidbody.AddForce(transform.forward * drivingSpeed, ForceMode.Acceleration);
			rigidbody.drag = 3;
		}
		
		
		//turning
		float turn = Input.GetAxis("Horizontal") * turningSpeed * Time.deltaTime;
		transform.Rotate(0, turn, 0);
		
		
		//neutral
		if (Input.GetKeyUp("mouse 0"))
		{
			rigidbody.drag = 1;
		}
		
		
		//braking
		if (Input.GetKeyDown("mouse 1")) 
		{
			rigidbody.drag = 4.5f;
		}
	}
}
