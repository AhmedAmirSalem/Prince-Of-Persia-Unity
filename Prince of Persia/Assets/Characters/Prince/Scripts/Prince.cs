using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Prince : MonoBehaviour
{
	float maxspeed = 3.0f;
	float movedirection;
	float rotation;
	public bool grounded = true;
	Rigidbody myrigid;
	Animator myanim;
	public LayerMask whatIsGround;
	public Transform groundCheck;
	GameObject pole;
	GameObject noWall;
	Transform face;
	bool attack1;
	bool attack2;
	bool attack3;
	bool hit;
	public bool guarding;
	bool jfw;
	bool spin;
	bool jfp;
	float sideTimer;
	float upTimer;
	float ignoreTimer;
	float walltimer;
	float iframes;
	float pauseTimer;
	public bool test;
	float noMoreDeath;
	bool dead;
	AudioSource footStep;
	AudioSource timeStop;
	AudioSource hitSound;
	AudioSource deathSound;
	AudioSource guardSound;
	// Use this for initialization
	void Awake ()
	{
		myanim = transform.Find ("PrinceAnimator").GetComponent<Animator> ();
		myrigid = GetComponent<Rigidbody> ();
		groundCheck = GameObject.Find ("Groundcheck").transform;
		face = GameObject.Find ("Face").transform;
		if (GetComponents<AudioSource> ().Length>1) {
			footStep = GetComponents<AudioSource> () [0];
			timeStop = GetComponents<AudioSource> () [1];
			guardSound = GetComponents<AudioSource> () [2];
			deathSound = GetComponents<AudioSource> () [3];
			hitSound = GetComponents<AudioSource> () [4];
		}
	}

	void FixedUpdate ()
	{
		grounded = (bool)Physics.CheckSphere (groundCheck.position, 0.3f, whatIsGround);
		if ((myanim.GetCurrentAnimatorStateInfo (0).IsName ("Standing Idle") || myanim.GetCurrentAnimatorStateInfo (0).IsName ("Walking") || myanim.GetCurrentAnimatorStateInfo (0).IsName ("Running"))
		    || myanim.GetCurrentAnimatorStateInfo (0).IsName ("Standing Walk Back") || myanim.GetCurrentAnimatorStateInfo (0).IsName ("Standing Run Back")) {
			myrigid.velocity = new Vector3 (movedirection * maxspeed * transform.forward.x, myrigid.velocity.y, movedirection * maxspeed * transform.forward.z);
			transform.Rotate (0, rotation, 0);
		}
		if (myanim.GetCurrentAnimatorStateInfo (0).IsName ("Sprinting Forward Roll")) {
			myrigid.velocity = new Vector3 (8 * transform.forward.x, myrigid.velocity.y, 8 * transform.forward.z);
		}
		if (myanim.GetCurrentAnimatorStateInfo (0).IsName ("Backflip")) {
			myrigid.velocity = new Vector3 (-8 * transform.forward.x, myrigid.velocity.y, -8 * transform.forward.z);
		}
		if (myanim.GetCurrentAnimatorStateInfo (0).IsName ("Up Wall Run")) {
			myrigid.velocity = new Vector3 (myrigid.velocity.x, 5, myrigid.velocity.z);
		}
		if (myanim.GetCurrentAnimatorStateInfo (0).IsName ("Side Wall Run")) {
			myrigid.velocity = new Vector3 (5*transform.right.x, 0.5f, 5*transform.right.z);
		}
		if (myanim.GetCurrentAnimatorStateInfo (0).IsName ("Side Wall Run_M")) {
			myrigid.velocity = new Vector3 (-5*transform.right.x, 0.5f, -5*transform.right.z);
		}
		if (jfw) {
			if (myanim.GetCurrentAnimatorStateInfo (0).IsName ("Side Wall Run") || myanim.GetCurrentAnimatorStateInfo (0).IsName ("Side Wall Run_M")
			    ) {
				myrigid.velocity = new Vector3 (transform.forward.x * 5, 3, transform.forward.z * 5);
			} else if (myanim.GetCurrentAnimatorStateInfo (0).IsName ("Climbing Idle")) {
				
				ignoreTimer = Time.deltaTime;
				Physics.IgnoreCollision (transform.Find ("unamed").GetComponent<Collider> (), pole.GetComponent<Collider> ());
				myrigid.velocity = new Vector3 (transform.forward.x * 5, 3, transform.forward.z * 5);
			} else if(myanim.GetCurrentAnimatorStateInfo (0).IsName ("Hanging Idle(1)")){
				myrigid.velocity = new Vector3 (transform.forward.x * 5, 5, transform.forward.z * 5);
			}else{
				jfw = false;
			}
		}
		if (myanim.GetCurrentAnimatorStateInfo (0).IsName ("Hanging Idle")&&!spin&&!jfp) {
			transform.position = new Vector3 (pole.transform.position.x, pole.transform.position.y - 2.4f, pole.transform.position.z);
		}
		if (spin&&myanim.GetCurrentAnimatorStateInfo (0).IsName ("Hanging Idle")) {
			transform.RotateAround (pole.transform.position, -transform.right, 5);
		}
		float rot = transform.eulerAngles.x;
		if (rot < 0) {
			rot = rot + 360;
		}
		if(!Input.GetButton("Acrobatics")&&(rot>350||rot<10)&&(transform.eulerAngles.z<0.1&&transform.eulerAngles.z>-0.1)&&!jfp){
			spin = false;
			Vector3 angle = transform.eulerAngles;
			angle.x =0;
			transform.eulerAngles = angle;
		}
		if((rot>350||rot<10)&&(transform.eulerAngles.z<0.1&&transform.eulerAngles.z>-0.1)&&jfp){
			spin = false;
			Vector3 angle = transform.eulerAngles;
			angle.x =0;
			transform.eulerAngles = angle;
			myanim.SetBool ("OnPole", false);
			Physics.IgnoreCollision (transform.Find ("unamed").GetComponent<Collider> (), pole.GetComponent<Collider> ());
			print (transform.Find ("unamed").GetComponent<Collider> ());
			print (pole.GetComponent<Collider> ());
			myrigid.velocity = new Vector3(transform.forward.x * 10, 150, transform.forward.z * 10);
			jfp = false;
			ignoreTimer = Time.deltaTime;
		}
		if (!jfw&&(myanim.GetCurrentAnimatorStateInfo (0).IsName ("Climbing") || myanim.GetCurrentAnimatorStateInfo (0).IsName ("Climbing Idle"))) {
			if (transform.position.y < (pole.transform.position.y+pole.transform.lossyScale.y/2)) {
				print (transform.position.y < (pole.transform.position.y));
				myrigid.velocity = new Vector3 (0, 2 * Input.GetAxis ("Vertical"), 0);
			} else {
				if (Input.GetAxis ("Vertical")<0) {
					myrigid.velocity = new Vector3 (0, 2 * Input.GetAxis ("Vertical"), 0);
				}
			}
			transform.RotateAround (pole.transform.position, pole.transform.up, 2 * Input.GetAxis ("Horizontal"));
		}
	}
	// Update is called once per frame
	void Update ()
	{
		//print (myanim.GetCurrentAnimatorStateInfo (0).IsName ("Standing Idle") || myanim.GetCurrentAnimatorStateInfo (0).IsName ("Walking") || myanim.GetCurrentAnimatorStateInfo (0).IsName ("Running"));
		if (Input.GetAxis ("Vertical") >= 0) {
			myanim.SetFloat ("Speed", myrigid.velocity.magnitude);
		} else {
			myanim.SetFloat ("Speed", myrigid.velocity.magnitude * -1);
		}
		myanim.SetBool ("Rolled", false);
		myanim.SetBool ("Grounded", grounded);
		myanim.SetBool ("StandJumped", false);
		myanim.SetBool ("Counter", false);
		myanim.SetBool ("Hit", hit);
		myanim.SetBool ("Guarding", guarding);
		hit = false;
		myanim.SetBool ("Attack1", attack1);
		myanim.SetBool ("Attack2", attack2);
		myanim.SetBool ("Attack3", attack3);
		movedirection = Input.GetAxis ("Vertical");
		rotation = Input.GetAxis ("Horizontal")*4;
		if (Input.GetButtonDown ("Run")) {
			maxspeed = 5.0f;
		}
		if (Input.GetButtonUp ("Run")) {
			maxspeed = 3.0f;
		}
		if (Input.GetButtonDown ("Guard")) {
			myanim.SetBool ("Guard", true);
		}
		if (Input.GetButtonUp ("Guard")) {
			myanim.SetBool ("Guard", false);
		}
		if (myanim.GetCurrentAnimatorStateInfo (0).IsName ("Standing Block React Large") || myanim.GetCurrentAnimatorStateInfo (0).IsName ("Standing Block Idle")) {
			guarding = true;
		} else {
			guarding = false;
		}
		if (Input.GetButtonDown ("Jump") && grounded && (myanim.GetCurrentAnimatorStateInfo (0).IsName ("Standing Idle") || myanim.GetCurrentAnimatorStateInfo (0).IsName ("Walking") || myanim.GetCurrentAnimatorStateInfo (0).IsName ("Running"))) {
			if (!myanim.GetCurrentAnimatorStateInfo (0).IsName ("Standing Idle")) {
				myrigid.AddForce (new Vector3 (transform.forward.x * 20, 5, transform.forward.z * 20), ForceMode.Impulse);
				myanim.SetBool ("Jumped", true);
			} else {
				myrigid.AddForce (new Vector3 (0, 3, 0), ForceMode.Impulse);
				myanim.SetBool ("StandJumped", true);
			}
		}
		if(Input.GetButtonDown ("Jump") && !jfw && !myanim.GetCurrentAnimatorStateInfo (0).IsName ("Climbing")&&(myanim.GetCurrentAnimatorStateInfo (0).IsName ("Side Wall Run")||myanim.GetCurrentAnimatorStateInfo (0).IsName ("Side Wall Run_M")
			||myanim.GetCurrentAnimatorStateInfo (0).IsName ("Up Wall Run")||myanim.GetCurrentAnimatorStateInfo (0).IsName ("Hanging Idle(1)")
			||myanim.GetCurrentAnimatorStateInfo (0).IsName ("Climbing Idle"))){
			Vector3 rotation = transform.eulerAngles;
			rotation.y += 180;
			transform.eulerAngles = rotation;
			rotation = transform.Find ("Centre").eulerAngles;
			rotation.y += 180;
			transform.Find ("Centre").eulerAngles = rotation;
			myanim.SetBool ("Jumped", true);
			myanim.SetBool ("SideWallRun",false);
			myanim.SetBool ("SideWallRunM",false);
			myanim.SetBool ("UpWallRun",false);
			jfw = true;
			if (!myanim.GetCurrentAnimatorStateInfo (0).IsName ("Hanging Idle(1)")) {
				myrigid.velocity = new Vector3 (transform.forward.x * 10, 100, transform.forward.z * 10);
			}
		}
		if (Input.GetButtonDown ("Roll") && grounded && (myanim.GetCurrentAnimatorStateInfo (0).IsName ("Standing Idle") || myanim.GetCurrentAnimatorStateInfo (0).IsName ("Walking") ||
		    myanim.GetCurrentAnimatorStateInfo (0).IsName ("Running") || myanim.GetCurrentAnimatorStateInfo (0).IsName ("Standing Walk Back") || myanim.GetCurrentAnimatorStateInfo (0).IsName ("Standing Run Back"))) {
			myanim.SetBool ("Rolled", true);
		}

		if (myanim.GetCurrentAnimatorStateInfo (0).IsName ("Standing Idle") || myanim.GetCurrentAnimatorStateInfo (0).IsName ("Walking") || myanim.GetCurrentAnimatorStateInfo (0).IsName ("Running")
		    || myanim.GetCurrentAnimatorStateInfo (0).IsName ("Standing Walk Back") || myanim.GetCurrentAnimatorStateInfo (0).IsName ("Standing Run Back") || myanim.GetCurrentAnimatorStateInfo (0).IsName ("Standing Block Idle")) {
			attack1 = false;
			attack2 = false;
			attack3 = false;
		}
		if (Input.GetButtonDown ("Attack")) {
			if (myanim.GetCurrentAnimatorStateInfo (0).IsName ("Attack1")) {
				attack2 = true;
			} else if (myanim.GetCurrentAnimatorStateInfo (0).IsName ("Attack2")) {
				attack3 = true;
			} else if (myanim.GetCurrentAnimatorStateInfo (0).IsName ("Standing Block React Large")) {
				myanim.SetBool ("Counter", true);
			} else {
				attack1 = true;
			}
		}
		if (myanim.GetCurrentAnimatorStateInfo (0).IsName ("Up Wall Run")) {
			upTimer += Time.deltaTime;
		} else {
			upTimer = 0;
		}
		if (myanim.GetCurrentAnimatorStateInfo (0).IsName ("Up Wall Run")&&(upTimer>0.7||Input.GetButtonUp("Acrobatics"))) {
			myanim.SetBool ("UpWallRun",false);
		}
		if (myanim.GetCurrentAnimatorStateInfo (0).IsName ("Side Wall Run")||myanim.GetCurrentAnimatorStateInfo (0).IsName ("Side Wall Run_M")) {
			sideTimer += Time.deltaTime;
		} else {
			sideTimer = 0;
		}
		if ((myanim.GetCurrentAnimatorStateInfo (0).IsName ("Side Wall Run")||myanim.GetCurrentAnimatorStateInfo (0).IsName ("Side Wall Run_M"))&&(sideTimer>2||Input.GetButtonUp("Acrobatics"))) {
			myanim.SetBool ("SideWallRun",false);
			myanim.SetBool ("SideWallRunM",false);
		}
		if (!jfw&&(myanim.GetCurrentAnimatorStateInfo (0).IsName ("Hanging Idle(1)")||myanim.GetCurrentAnimatorStateInfo (0).IsName ("Side Wall Run")||myanim.GetCurrentAnimatorStateInfo (0).IsName ("Side Wall Run_M")
			||myanim.GetCurrentAnimatorStateInfo (0).IsName ("Hanging Idle")||myanim.GetCurrentAnimatorStateInfo (0).IsName ("Climbing Idle")||myanim.GetCurrentAnimatorStateInfo (0).IsName ("Climbing"))) {
			myrigid.velocity = new Vector3 (myrigid.velocity.x, 0, myrigid.velocity.z);
			myrigid.useGravity = false;
		} else {
			myrigid.useGravity = true;
		}
		if(Input.GetButton("Acrobatics")&&myanim.GetCurrentAnimatorStateInfo (0).IsName ("Hanging Idle")&&!jfp){
			spin = true;
		}
		if (!spin & Input.GetAxis ("Vertical") < 0 & Input.GetButtonDown("Vertical")&&myanim.GetCurrentAnimatorStateInfo (0).IsName ("Hanging Idle")) {
			Vector3 rotation = transform.Find ("Centre").eulerAngles;
			transform.Rotate (new Vector3 (0, 180, 0));
			transform.Find ("Centre").eulerAngles = rotation;
		}
		if(spin&&Input.GetButtonDown("Jump")&&myanim.GetCurrentAnimatorStateInfo (0).IsName ("Hanging Idle")){
			jfp = true;
		}
		if (ignoreTimer > 0) {
			ignoreTimer += Time.deltaTime;
		}
		if (ignoreTimer > 0.3f) {
			ignoreTimer = 0;
			Physics.IgnoreCollision (transform.Find ("unamed").GetComponent<Collider> (), pole.GetComponent<Collider> (),false);
		}
		if (GameMaster.hp <= 0 && !dead) {
			myanim.SetBool ("Death", true);
			noMoreDeath+=Time.deltaTime;
			dead = true;
			deathSound.Play ();
		}
		if (iframes > 0) {
			iframes += Time.deltaTime;
		}
		if (iframes > 1) {
			iframes = 0;
		}
		if (pauseTimer > 5) {
			pauseTimer = 0;
			GameMaster.Unpause ();
		}
		if (pauseTimer > 0) {
			pauseTimer += Time.deltaTime;
		}
		if(Input.GetButtonDown("Time Pause")&&GameMaster.sands>0&&!GameMaster.paused){
			GameMaster.Pause ();
			pauseTimer += Time.deltaTime;
			GameMaster.sands--;
			timeStop.Play ();
		}
	}
	void LateUpdate(){
		if (myanim.GetBool ("Walled")) {
			walltimer += Time.deltaTime;
		}
		if (walltimer > 0.1) {
			myanim.SetBool ("Walled", false);
			walltimer = 0;
		}
		myanim.SetBool ("OnPoleV", false);
		myanim.SetBool ("Jumped", false);
		if (noMoreDeath > 0) {
			noMoreDeath += Time.deltaTime;
		}
		if(noMoreDeath>0.5){
			myanim.SetBool ("Death", false);
		}
		myanim.SetBool ("Dead", dead);
		if ((myanim.GetCurrentAnimatorStateInfo (0).IsName ("Walking") || myanim.GetCurrentAnimatorStateInfo (0).IsName ("Running"))
		    || myanim.GetCurrentAnimatorStateInfo (0).IsName ("Standing Walk Back") || myanim.GetCurrentAnimatorStateInfo (0).IsName ("Standing Run Back")) {
			if (!footStep.isPlaying) {
				footStep.Play ();
			}
		} else {
			footStep.Stop ();
		}
	}
	void OnTriggerEnter (Collider c)
	{
		if (c.CompareTag ("EnemyWeapon")) {
			if (c.gameObject.GetComponent<EnemyWeaponScript> ().danger&&!(myanim.GetCurrentAnimatorStateInfo (0).IsName ("Sprinting Forward Roll")
				||myanim.GetCurrentAnimatorStateInfo (0).IsName ("Backflip")||myanim.GetCurrentAnimatorStateInfo (0).IsName ("Butterfly Twirl")||myanim.GetCurrentAnimatorStateInfo (0).IsName ("Standing React Large Gut"))) {
				if (guarding && c.gameObject.GetComponent<EnemyWeaponScript> ().unblock&& iframes == 0) {
					myanim.SetTrigger ("Pierce");
				} else {
					if (iframes == 0) {
						hit = true;
					}
				}
				if ((!guarding || c.gameObject.GetComponent<EnemyWeaponScript> ().unblock) && iframes == 0) {
					GameMaster.hp -= 10;
					iframes += Time.deltaTime;
					if (GameMaster.hp > 0) {
						hitSound.Play ();
					}
				} else {
					if (iframes == 0) {
						guardSound.Play ();
					}
				}
			}
			print (c.gameObject.GetComponent<EnemyWeaponScript> ().unblock);
		}
	}
	void OnCollisionEnter(Collision c){
		if (c.gameObject.CompareTag ("swing") &&(myanim.GetCurrentAnimatorStateInfo (0).IsName ("Falling Idle") || myanim.GetCurrentAnimatorStateInfo (0).IsName ("Jump"))) {

			float incline = transform.eulerAngles.y - c.transform.eulerAngles.y;
			Vector3 rotation = transform.Find ("Centre").eulerAngles;
			if (incline < 0) {
				incline = incline + 360;
			}
			Vector3 angles = transform.eulerAngles;
			if (incline > 180 || incline < 0) {
				angles.y = c.transform.eulerAngles.y+270;
				transform.eulerAngles = angles;
			} else {
				angles.y = c.transform.eulerAngles.y+90;
				transform.eulerAngles = angles;
			}
			transform.Find ("Centre").eulerAngles = rotation;
			transform.SetPositionAndRotation(new Vector3 (c.transform.position.x, c.transform.position.y - 2, c.transform.position.z),transform.rotation);
			//transform.SetParent (c.transform);
			pole = c.gameObject;
			myanim.SetBool ("OnPole",true);
			myrigid.velocity = new Vector3 (0, 0, 0);
			myrigid.useGravity = false;
		}
		 if (c.gameObject.CompareTag ("Wall") && (myanim.GetCurrentAnimatorStateInfo (0).IsName ("Falling Idle")||myanim.GetCurrentAnimatorStateInfo (0).IsName ("Jump From Wall"))) {
			myanim.SetBool ("Walled", true);
			Vector3 angles = transform.eulerAngles;
			angles.y = c.transform.eulerAngles.y;
			transform.eulerAngles = angles;
		}
		if(c.gameObject.CompareTag("climb")){
			Vector3 orgrot = transform.eulerAngles;
			transform.LookAt (c.transform);
			orgrot.y = transform.eulerAngles.y;
			transform.eulerAngles = orgrot;
			myanim.SetBool ("OnPoleV", true);
			transform.Translate (new Vector3 (0, 0.5f, 0));
			myrigid.velocity = new Vector3 (0, 0, 0);
			pole = c.gameObject;
			test = !test;
		}
	}
	void OnCollisionStay(Collision c){
		if (c.gameObject.CompareTag ("Wall")&&Input.GetButtonDown("Acrobatics")&&(myanim.GetCurrentAnimatorStateInfo (0).IsName ("Standing Idle") || myanim.GetCurrentAnimatorStateInfo (0).IsName ("Walking") || myanim.GetCurrentAnimatorStateInfo (0).IsName ("Running")
			|| myanim.GetCurrentAnimatorStateInfo (0).IsName ("Standing Walk Back") || myanim.GetCurrentAnimatorStateInfo (0).IsName ("Standing Run Back"))
			&&!(myanim.GetBool("UpWallRun")||myanim.GetBool("SideWallRun")||myanim.GetBool("SideWallRunM"))) {
			float incline = transform.eulerAngles.y - c.transform.eulerAngles.y;
			Vector3 rotation = transform.Find ("Centre").eulerAngles;
			if (incline < 0) {
				incline = incline + 360;
			}
			Vector3 angles = transform.eulerAngles;
			if (incline >= 330 || incline <= 30) {
				angles.y = c.transform.eulerAngles.y;
				transform.eulerAngles = angles;
				myanim.SetBool ("UpWallRun",true);
				transform.Find ("Centre").eulerAngles = rotation;
			} else if (incline > 30 && incline < 90) {
				angles.y = c.transform.eulerAngles.y;
				transform.eulerAngles = angles;
				myanim.SetBool ("SideWallRun",true);
				transform.Find ("Centre").eulerAngles = rotation;
			} else if (incline < 330 && incline > 270) {
				angles.y = c.transform.eulerAngles.y;
				transform.eulerAngles = angles;
				myanim.SetBool ("SideWallRunM",true);
				transform.Find ("Centre").eulerAngles = rotation;
			}
		}
	}
	void OnCollisionExit(Collision c){
		if (c.gameObject.CompareTag ("Wall")) {
			myanim.SetBool ("SideWallRun",false);
			myanim.SetBool ("SideWallRunM",false);
		}
	}
}
