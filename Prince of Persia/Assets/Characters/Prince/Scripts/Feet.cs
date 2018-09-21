using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Feet : MonoBehaviour {
	Animator anim;
	public bool danger = false;
	float dangerTimer;
	// Use this for initialization
	void Awake () {
		anim = GameObject.Find("PrinceAnimator").GetComponent<Animator> ();
	}

	// Update is called once per frame
	void Update () {
		if (dangerTimer > 3) {
			dangerTimer = 0;
		}
		if (dangerTimer > 0.7) {
			danger = false;
		}
		if (dangerTimer > 0) {
			dangerTimer += Time.deltaTime;
		}
		if (anim.GetCurrentAnimatorStateInfo (0).IsName ("Butterfly Twirl")&&danger==false&&dangerTimer==0) {
			danger = true;
			dangerTimer += Time.deltaTime;
		} 
	}

}
