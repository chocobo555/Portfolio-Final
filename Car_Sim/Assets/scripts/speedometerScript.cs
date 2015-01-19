using UnityEngine;
using System.Collections;

public class speedometerScript : MonoBehaviour {
	
	float mph;
	
	Camera playerCamera;
	
	GameObject speedometer;
	
	GUIText speedometerDisplay;
	
	// Use this for initialization
	void Start () {
		
		playerCamera = Camera.main;
		
		speedometer = GameObject.Find ("speedometer");
		
		speedometerDisplay = speedometer.GetComponent<GUIText>();
		
	}
	
	// Update is called once per frame
	void Update () {
	
		mph = playerCamera.velocity.magnitude*2;
		
		speedometerDisplay.text = mph + " MPH";
		
	}
}
