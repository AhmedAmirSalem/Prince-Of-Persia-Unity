using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SandsCollect : MonoBehaviour {

	// Use this for initialization


	void Start () {

	}


	void OnTriggerEnter(Collider c){
		if (!enabled)
			return;
		if(c.gameObject.CompareTag("PrinceWeapon")){
			Destroy (gameObject);
			GameMaster.sands+=1;
			if(GameMaster.collect != null)
				GameMaster.collect.Play ();
		}

	
	}
	
	// Update is called once per frame
	void Update () {

		transform.Rotate (0, 1.5f, 0);

	}
}
