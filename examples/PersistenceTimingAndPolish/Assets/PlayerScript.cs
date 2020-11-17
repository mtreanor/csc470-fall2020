using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerScript : MonoBehaviour
{
	public CharacterController cc;
	float moveSpeed = 5f;

	public MeterScript meter;
	int health = 100;

	// Start is called before the first frame update
	void Start()
	{
		StartCoroutine(modifyMoveSpeed());
	}

	// Update is called once per frame
	void Update()
	{
		if (Input.GetKeyDown(KeyCode.Space)) {
			SceneManager.LoadScene("Level1");
		}

		if (Input.GetKeyDown(KeyCode.S)) {
			GameManager.instance.score++;
			Debug.Log(GameManager.instance.score);
			PlayerPrefs.SetInt("score", GameManager.instance.score);
		}

		cc.Move(transform.forward * moveSpeed * Time.deltaTime);


		if (Input.GetKeyDown(KeyCode.M)) {
			health -= 10;
			meter.SetMeter(health / 100f);
		}
	}


	IEnumerator modifyMoveSpeed()
	{
		while (true) {
			moveSpeed *= -1;
			yield return new WaitForSeconds(1);
		}
	}
}
