using UnityEngine;
using System.Collections;

public class MouseLock : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	public static bool lockCursor = true;
	void Update () 
	{
	    if (Screen.lockCursor != lockCursor)
    {
        if (lockCursor && Input.GetMouseButton(0))
            Screen.lockCursor = true;
        else if (!lockCursor)
            Screen.lockCursor = false;
    }
	}
}
