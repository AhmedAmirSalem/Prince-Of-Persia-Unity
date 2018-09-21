using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class keyCollect : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}

	void OnTriggerEnter(Collider c){
	
		GameMaster.keys = true;
		Destroy (gameObject);
	
	}


	// Update is called once per frame
	void Update () {
		
	}
}
