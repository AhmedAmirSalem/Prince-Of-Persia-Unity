﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitBox : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}


	void OnTriggerEnter(Collider c){
	
		if (c.name.CompareTo("sword") == 0 && c.gameObject.CompareTag ("PrinceWeapon") ) {

			if (c.GetComponent<Sword> ().danger) {

				gameObject.GetComponentInParent<NPC_patrol> ().hit ();
				return;
			}

		}

		if((c.name.CompareTo("RightHit") == 0 || c.name.CompareTo("LeftHit") == 0)&& c.gameObject.CompareTag ("PrinceWeapon")){

			if (c.GetComponent<Feet>().danger) {

				gameObject.GetComponentInParent<NPC_patrol> ().hit ();
				return;
			}
		}
	}

	
	// Update is called once per frame
	void Update () {
		
	}
}