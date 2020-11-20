using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;

public class PlayerScript : MonoBehaviour
{
	public CharacterController cc;
	float moveSpeed = 5f;
	float rotateSpeed = 90f;

	public enum STATES { DEFAULT, DAMAGED, ATTACKING };
	public STATES state;

	public MeterScript meter;
	int health = 100;

	// Start is called before the first frame update
	void Start()
	{

	}


	// Update is called once per frame
	void Update()
	{
		//switch (state) {
		//	case STATES.DEFAULT:
		//		break;
		//	case STATES.ATTACKING:
		//		attack();
		//		break;
		//	case STATES.DAMAGED:
		//		damaged();
		//		break;
		//	default:
		//		break;
		//}

		if (GameManager.instance.mode == GameManager.MODES.GAMEPLAY) {

			float hAxis = Input.GetAxis("Horizontal");
			float vAxis = Input.GetAxis("Vertical");

			transform.Rotate(0, hAxis * rotateSpeed * Time.deltaTime, 0);

			cc.Move(transform.forward * vAxis * moveSpeed * Time.deltaTime);
		}
	}

	private void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.CompareTag("Enemy")) {
			health -= 10;
			meter.SetMeter(health / 100f);
		}
	}
}
