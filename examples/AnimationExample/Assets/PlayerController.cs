using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
	public Animator anim;
	CharacterController cc;


    // Start is called before the first frame update
    void Start()
    {
		cc = gameObject.GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
		float hAxis = Input.GetAxis("Horizontal");
		float vAxis = Input.GetAxis("Vertical");

		transform.Rotate(0, hAxis * 25 * Time.deltaTime, 0);

		anim.SetBool("shouldWalk", !(vAxis == 0));

		cc.Move(transform.forward * vAxis * 8 * Time.deltaTime);
    }
}
