using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class BotScript : MonoBehaviour
{
	public NavMeshAgent agent;



	// Start is called before the first frame update
	void Start()
	{
		StartCoroutine(NavigationBehavior());
	}

	// Update is called once per frame
	void Update()
	{

	}

	IEnumerator NavigationBehavior()
	{
		while (true) {
			Vector3 pos = new Vector3(Random.Range(-10, 10), 0, Random.Range(-10, 10));
			agent.SetDestination(pos);

			yield return new WaitForSeconds(4);
		}
	}
}
