using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
	public GameObject followObject;

    // Start is called before the first frame update
    void Start()
    {

	}

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q)) {
			followObject = GameObject.Find("Platform");
		}
    
		// Position the camera
		Vector3 cameraPosition = followObject.transform.position - followObject.transform.forward * 12 + Vector3.up * 5;
		transform.position = cameraPosition;
		Vector3 lookAtPos = followObject.transform.position +followObject.transform.forward * 8;
		
		// Rotate the camera so that it looks always in front of the plane.
		transform.LookAt(lookAtPos, Vector3.up);
    }
}
