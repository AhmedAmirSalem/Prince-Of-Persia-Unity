using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : MonoBehaviour {
	Animator anim;
	public bool danger = false;
	// Use this for initialization
	void Awake () {
		anim = GameObject.Find("PrinceAnimator").GetComponent<Animator> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (anim.GetCurrentAnimatorStateInfo (0).IsName ("Attack1") || anim.GetCurrentAnimatorStateInfo (0).IsName ("Attack2") || anim.GetCurrentAnimatorStateInfo (0).IsName ("Attack3")) {
			danger = true;
		} else {
			danger = false;
		}
	}
}
