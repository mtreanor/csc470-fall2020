using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChildOfPlayerScript : MonoBehaviour
{

	public float someNumber = 0;
	
	
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
		//Debug.Log(someNumber);    
    }
    
    public void IdleEnded()
	{
		Debug.Log("IDLE ENDED");
	}
}
