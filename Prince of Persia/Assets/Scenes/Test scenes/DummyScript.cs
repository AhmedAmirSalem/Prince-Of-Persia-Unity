using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DummyScript : MonoBehaviour {
	public int HP = 100;
	// Use this for initialization
	void Start () {
		
	}
	void FixedUpdate(){
		transform.Rotate (new Vector3 (0, 1, 0));
	}
	// Update is called once per frame
	void Update () {
		if (HP <= 0) {
			Destroy (this.gameObject);
		}
	}

	void OnTriggerEnter(Collider c){
		if (c.name.Equals ("sword")) {
			if (c.gameObject.GetComponent<Sword> ().danger) {
				HP = HP - 10;
			}
		}
		if (c.name.Equals ("LeftHit")||c.name.Equals ("RightHit")) {
			if (c.gameObject.GetComponent<Feet> ().danger) {
				HP = HP - 10;
			}
		}
	}
}
