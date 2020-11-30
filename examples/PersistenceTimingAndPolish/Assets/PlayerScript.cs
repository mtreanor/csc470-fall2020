using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;
using UnityEngine.Rendering.PostProcessing;

public class PlayerScript : MonoBehaviour
{
	public CharacterController cc;
	float moveSpeed = 5f;
	float rotateSpeed = 90f;

	public PostProcessProfile ppp;

	public enum STATES { DEFAULT, DAMAGED, ATTACKING };
	public STATES state;

	public MeterScript meter;
	int health = 100;

	public GameObject attackCube;

	Coroutine attackRoutine;

	public GameObject damagePrefab;

	// Postprocessing stuff, they get theiur values in start
	//Vignette vig;
	//float vigIntensity = 0;
	LensDistortion lensDist;


	// Start is called before the first frame update
	void Start()
	{
		//ppp.TryGetSettings<Vignette>(out vig);
		ppp.TryGetSettings<LensDistortion>(out lensDist);
	}


	// Update is called once per frame
	void Update()
	{

		// This is just an example of a way to better organize your code when it gets complicated
		//switch (state) {
		//	case STATES.DEFAULT:
		//		default();
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

		//Debug.DrawRay(transform.position + Vector3.up * 2, transform.forward * 3, Color.white);


		//if (Input.GetKey(KeyCode.P)) {
		//	vigIntensity += 2 * Time.deltaTime;
		//	vig.intensity.Override(vigIntensity);
		//}


		if (GameManager.instance.mode == GameManager.MODES.GAMEPLAY) {

			float hAxis = Input.GetAxis("Horizontal");
			float vAxis = Input.GetAxis("Vertical");

			transform.Rotate(0, hAxis * rotateSpeed * Time.deltaTime, 0);

			cc.Move(transform.forward * vAxis * moveSpeed * Time.deltaTime);

			// Attack
			if (Input.GetKeyDown(KeyCode.Space)) {
				if (attackRoutine != null) {
					StopCoroutine(attackRoutine);
				}
				attackRoutine = StartCoroutine(Attack());
			}
		}
	}

	IEnumerator Attack()
	{
		attackCube.SetActive(true);
		yield return new WaitForSeconds(1);
		attackCube.SetActive(false);
	}

	private void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.CompareTag("Enemy")) {
			GameObject damageObject = Instantiate(damagePrefab, transform.position, transform.rotation);
			Destroy(damageObject, 5);
			health -= 10;
			meter.SetMeter(health / 100f);

			StartCoroutine(DamageEffect());
		}
	}

	IEnumerator DamageEffect()
	{
		//1. Turn on LensDistorion
		lensDist.intensity.Override(60);
		//2. Wait 1 second
		yield return new WaitForSeconds(1);
		//3. Turn off LensDistorion
		lensDist.intensity.Override(0);
	}
}
