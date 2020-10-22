using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaneScript : MonoBehaviour
{
	public GameObject playerPrefab;

	Rigidbody rb;
	
    // Start is called before the first frame update
    void Start()
    {
		rb = gameObject.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
		Vector3 cameraPosition = transform.position - transform.forward * 12 + Vector3.up * 5;
		Camera.main.transform.position = cameraPosition;
		Vector3 lookAtPos = transform.position + transform.forward * 8;
		
		// Rotate the camera so that it looks always in front of the plane.
		Camera.main.transform.LookAt(lookAtPos, Vector3.up);
	}

	private void OnCollisionEnter(Collision collision)
	{
		Debug.Log("Collided with ground");
		Instantiate(playerPrefab, transform.position, Quaternion.identity);
		
		//Destroy(gameObject);
	}
}
