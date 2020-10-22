using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitScript : MonoBehaviour
{
	// How fast the Unit will move forward.
	float speed = 5f;
	// How fast the Unit will rotate toward its targetPosition.
	float rotateSpeed = 4f;
	
	// Units will always rotate toward this position unless they are already close to it.
	Vector3 targetPosition;

	// These two booleans are used to track the state based on the mouse (see the mouse functions below).
	public bool selected = false;
	public bool hover = false;

	// These colors are given values via the Unity inspector.
	public Color defaultColor;
	public Color hoverColor;
	public Color selectedColor;

	// This gets its value from the Unity inspector. We dragged the "Mesh Renderer" of the Prefab to do that.
	public Renderer rend;

	public CharacterController cc;

	// Start is called before the first frame update
	void Start()
	{
		// Set the color of the rendere's material based on the mouse state variables.
		UpdateVisuals();

		// Initialize the targetPosition so that the Unit is initially close enough to its target to not want
		// to move and rotate toward it.
		targetPosition = transform.position;
	}
	
	// Update is called once per frame
	void Update()
	{
		if (selected) {
			if (Input.GetMouseButtonDown(0)) {
				// We will get in here if the Unit is selected, and the player has clicked the mouse.
				
				// The following code create's a "ray" at the position that the mouse is on the screen, and performs
				// a Raycast. This is a Physics utility function that will move in a direction and populate the 
				// values of a RaycastHit object if something was hit. If the Raycast doesn't hit anything (i.e. the
				// player clicks into the nothingness - where there are no GameObjects), the Physics.Raycast
				// returns null, and thus we will not go in the if statement's body.
				Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
				RaycastHit hit;
				if (Physics.Raycast(ray, out hit)) {
					// If we get in here, it means that the mouse was "over" a GameObject when the player clicked.
					// Check to see if what we clicked on the the "Ground" via this layer check.
					if (hit.collider.gameObject.layer == LayerMask.NameToLayer("Ground")) {
						// If we get in here, we have hit the ground, and we should update the Unit's targetPosition to 
						// be the point on the ground that the player clicked on.
						targetPosition = hit.point;
					}
				}
			}
		}

		// MOVEMENT
		// If we are not close to our target position, rotate toward the targetPosition, and move forward.
		if (Vector3.Distance(transform.position, targetPosition) > 0.5f) {
			Vector3 vectorToTarget = targetPosition - transform.position;
			vectorToTarget = vectorToTarget.normalized;

			float step = rotateSpeed * Time.deltaTime;

			Vector3 newDirection = Vector3.RotateTowards(transform.forward, vectorToTarget, step, 1);
			transform.rotation = Quaternion.LookRotation(newDirection);
			
			cc.Move(transform.forward * speed * Time.deltaTime);
		}
	}
	
	// This function is called manually by the mouse event functions whenever
	// the hover, or selection bools are modified.
	public void UpdateVisuals()
	{
		if (selected) {
			rend.material.color = selectedColor;
		} else {
			if (hover) {
				rend.material.color = hoverColor;
			} else {
				rend.material.color = defaultColor;
			}
		}
	}

	private void OnMouseEnter()
	{
		hover = true;
		UpdateVisuals();
	}
	
	private void OnMouseExit()
	{
		hover = false;
		UpdateVisuals();
	}

	private void OnMouseDown()
	{
		selected = !selected;
		UpdateVisuals();
	}
}
