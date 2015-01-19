using UnityEngine;
using System.Collections;

public class Ice : MonoBehaviour 
{

	GameObject Car;
	float iceSpeedModifier;
	
	// Use this for initialization
	void Start () {
		Car = GameObject.Find("car");
		iceSpeedModifier = 0;
	}
	
	// Update is called once per frame
	void Update () {
		Car.GetComponent<driving>().drivingSpeed += iceSpeedModifier;
	}
	
	void OnTriggerEnter(Collider c)
	{
		print(c.name + "Has entered the trigger");
		
		iceSpeedModifier = 0.8f;
		Car.GetComponent<driving>().turningSpeed = 5f;
		Car.rigidbody.drag = 1;
	}
	
	void OnTriggerExit()
	{
		iceSpeedModifier = 0;
		Car.GetComponent<driving>().turningSpeed = 30;
		Car.GetComponent<driving>().drivingSpeed = 30;
		Car.rigidbody.drag = 3;
	}
}