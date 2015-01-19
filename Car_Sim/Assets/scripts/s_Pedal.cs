using UnityEngine;
using System.Collections;

public class s_Pedal : MonoBehaviour {
	
	public GameObject Brake;
	public GameObject Accelerator;
	
	public string keyBrake;
	public string keyAccelerate;
	
	public float brakeLimitMax;
	public float brakeLimitMin;
	public float acceleratorLimitMax;
	public float acceleratorLimitMin;

	void Update () {
		
		PushPedal();
	
	}
	
	
	void PushPedal () {
		
		if (Input.GetKeyDown (keyBrake)){
			if (Brake.transform.position.z < brakeLimitMax)
			{
				MovePedal(Brake, true);
				
				if (Input.GetKeyUp (keyBrake)){
					if (Brake.transform.position.z > brakeLimitMin)
					{
						MovePedal(Brake, false);
					}
				}
			}
		}
		
		
		
		if (Input.GetKeyDown (keyAccelerate)){
			if (Accelerator.transform.position.z < acceleratorLimitMax)
			{
				MovePedal(Accelerator, true);
				
				if (Input.GetKeyUp (keyAccelerate)){
					if (Accelerator.transform.position.z > acceleratorLimitMin)
					{
						MovePedal(Brake, false);
					}
				}
			}
		}
	}
	
	
	public static void MovePedal (GameObject pedal, bool keyState) {
		
		//Vector3 PedalPosition = new Vector3(pedal.transform.position.x, pedal.transform.position.y, pedal.transform.position.z);
		float MoveSpeed = 1f;
		float Movement = MoveSpeed * Time.deltaTime;
		Transform pedalTransform = pedal.transform;
		
		if (keyState == true){
			pedalTransform.Translate(Vector3.forward * Movement);
		}
		
		if (keyState == false){
			pedalTransform.Translate(Vector3.back * Movement);
		}
		
	}
}
