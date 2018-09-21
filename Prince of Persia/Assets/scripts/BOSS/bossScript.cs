using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class bossScript : MonoBehaviour {
    public static int hp = 200;
    Animator anim;
    NavMeshAgent agent;
    private float iframes;
    bool halfhp = false;
	Prince prince;
	public int decision;
	public float attackTimer;
	int revenge;
	bool dm;
	int dmcount;
	bool dead;
	EnemyWeaponScript right;
	EnemyWeaponScript left;
	EnemyWeaponScript leg;
	AudioSource deathSound;
	AudioSource footsteps;
	AudioSource hitSound;
	AudioSource dmSound;
    // Use this for initialization
	void Awake(){
		hp = 200;
	}
    void Start () {
        anim = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
		prince = GameObject.Find ("The_Prince").GetComponent<Prince> ();
		Transform[] ts = transform.GetComponentsInChildren<Transform> (true);
		foreach (Transform t in ts) {
			if (t.gameObject.name == "RightLeg") {
				leg = t.GetComponent<EnemyWeaponScript> ();
			}
			if (t.gameObject.name == "RightHand") {
				right = t.GetComponent<EnemyWeaponScript> ();
			}
			if (t.gameObject.name == "LeftHand") {
				left = t.GetComponent<EnemyWeaponScript> ();
			}
		}
		deathSound = GetComponents<AudioSource> () [0];
		footsteps = GetComponents<AudioSource> () [1];
		dmSound = GetComponents<AudioSource> () [2];
		hitSound = GetComponents<AudioSource> () [3];
    }

    // Update is called once per frame
    void Update() {
		if (dm) {
			
			if (agent.remainingDistance < 0.3 && dmcount<20) {
				transform.position = new Vector3 (-34.21f, 16.5701f, 160.768f);
				transform.LookAt (GameObject.Find ("The_Prince").transform);
				dmcount++;
				agent.destination = new Vector3 (transform.position.x + transform.forward.x * 15, transform.position.y, transform.position.z + transform.forward.z * 15);
				print (transform.position.x + transform.forward.x * 10 + " " +transform.position.z + transform.forward.z * 10);
				agent.speed++;
			}
			if (agent.remainingDistance < 0.3 && dmcount == 20) {
				transform.position = new Vector3 (-37.21f, 16.5701f, 160.768f);
				transform.LookAt (GameObject.Find ("The_Prince").transform);
				anim.SetBool ("Hurricane", false);
				attackTimer = 0;
				dmcount = 0;
				dm = false;
			}
			
		} else {
			if (!anim.GetCurrentAnimatorStateInfo (0).IsName ("punch") && !anim.GetCurrentAnimatorStateInfo (0).IsName ("punch2") &&
			   !anim.GetCurrentAnimatorStateInfo (0).IsName ("punch3") && !anim.GetCurrentAnimatorStateInfo (0).IsName ("kick")
			   && !anim.GetCurrentAnimatorStateInfo (0).IsName ("Hit Reaction")) {
				agent.isStopped = false;
			} else {
				agent.isStopped = true;
			}
			if (iframes > 0) {
				iframes += Time.deltaTime;
			}

			if (iframes > 0.5f) {

				iframes = 0;
			}
			if ((anim.GetCurrentAnimatorStateInfo (0).IsName ("punch") || anim.GetCurrentAnimatorStateInfo (0).IsName ("kick"))&&hp>=100) {
				revenge = 0;
			}
			attackTimer += Time.deltaTime;
			if (agent.remainingDistance < 4) {
				agent.speed = 3000;
			} else {
				agent.speed = 2;
			}

			if (agent.remainingDistance < 1.0f) {
				agent.isStopped = true;
				if (attackTimer > 2.5 && decision == 0) {
					if (prince.guarding) {
						decision = 2;
					} else {
						decision = 1;
					}
				}
				if (attackTimer > 3) {
					if (decision == 1 && !anim.GetCurrentAnimatorStateInfo (0).IsName ("Hit Reaction")) {
						anim.SetTrigger ("punch");
						decision = 0;
						attackTimer = 0;
					} else if (decision == 2 && !anim.GetCurrentAnimatorStateInfo (0).IsName ("Hit Reaction")) {
						anim.SetTrigger ("kick");
						decision = 0;
						attackTimer = 0;
					} else {
						attackTimer = 2.4f;
					}
				}
				anim.SetBool ("walking", false);
			} else {
				if (attackTimer > 2.5) {
					attackTimer = 2;
					decision = 0;
				}
			}
		}
		if (anim.GetCurrentAnimatorStateInfo (0).IsName ("walking")) {
			if (!footsteps.isPlaying) {
				footsteps.Play ();
			}
		} else {
			footsteps.Stop ();
		}
		if (anim.GetCurrentAnimatorStateInfo (0).IsName ("punch")) {
			left.danger = true;
		} else {
			left.danger = false;
		}
		if (anim.GetCurrentAnimatorStateInfo (0).IsName ("punch2")||anim.GetCurrentAnimatorStateInfo (0).IsName ("punch3")) {
			right.danger = true;
		} else {
			right.danger = false;
		}
		if (anim.GetCurrentAnimatorStateInfo (0).IsName ("kick")||anim.GetCurrentAnimatorStateInfo (0).IsName ("Hurricane")) {
			leg.danger = true;
		} else {
			leg.danger = false;
		}
    }
    public void hit()
    {

		if (iframes > 0|| dm || dead)
            return;
		hp = hp - 10;
		if (hp <= 0) {
			if (!dead) {
				dead = true;
				anim.SetTrigger ("Dead");
				agent.isStopped = false;
				deathSound.Play ();
			}
			return;
		}
        iframes += Time.deltaTime;
		revenge++;
        anim.SetTrigger("Hit");
		if (revenge > 2 && hp >= 100) {
			revenge = 0;
			int x = Random.Range (0, 5);
			if (x == 0) {
				transform.position = new Vector3 (-37.21f, 16.5701f, 160.768f);
			}
			if (x == 1) {
				transform.position = new Vector3 (-43.32f, 16.5701f, 151.82f);
			}
			if (x == 2) {
				transform.position = new Vector3 (-25.15f, 16.5701f, 151.82f);
			}
			if (x == 3) {
				transform.position = new Vector3 (-25.15f, 16.5701f, 168.94f);
			}
			if (x == 4) {
				transform.position = new Vector3 (-43.32f, 16.5701f, 168.94f);
			}
		} else if (revenge > 2 && hp < 100) {
			dm = true;
			transform.position = new Vector3 (-34.21f, 16.5701f, 160.768f);
			transform.LookAt (GameObject.Find ("The_Prince").transform);
			agent.destination = new Vector3 (transform.position.x + transform.forward.x * 15, transform.position.y, transform.position.z + transform.forward.z * 15);
			anim.SetBool ("Hurricane", true);
			agent.speed = 5;
			agent.isStopped = false;
			revenge = 0;
			dmSound.Play ();
			return;
		}
		hitSound.Play ();   
    }

    void OnTriggerStay(Collider c)
	{
		if (!dm&&!dead) {
			if (c.name.Equals ("RightHit") || c.name.Equals ("LeftHit")) {
				if (!anim.GetCurrentAnimatorStateInfo (0).IsName ("punch") && !anim.GetCurrentAnimatorStateInfo (0).IsName ("punch2") &&
				   !anim.GetCurrentAnimatorStateInfo (0).IsName ("punch3") && !anim.GetCurrentAnimatorStateInfo (0).IsName ("kick")
				   && !anim.GetCurrentAnimatorStateInfo (0).IsName ("Hit Reaction")) {
					anim.SetBool ("walking", true);
					agent.destination = c.gameObject.transform.position;
				}
				transform.LookAt (c.gameObject.transform.position);
			}
		}
	}
}
