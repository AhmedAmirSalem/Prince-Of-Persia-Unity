using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class NPC_patrol : MonoBehaviour
{

	NavMeshAgent agent;
	Animator anim;
	public Transform[] points;
	public float speed_increase = 3f;
	public int hp = 30;
	public GameObject drop;
	AudioSource footStep;
	AudioSource Death;
	AudioSource Hit;
	AudioSource stab;
	AudioSource caught;
	private int destPoint = 0;
	private float run_speed;
	private float walk_speed;
	private bool target_prince = false;
	private float iframes;
	private float dropTimer;
	private float stabDelay;
	private bool dropped = false;
    public Canvas enemyHUDCanvas;
    private float hudTimer = -1;
	private bool seen;
    // Use this for initialization
    void Start ()
	{
		agent = GetComponent<NavMeshAgent> ();
		anim = GetComponent<Animator> ();

		// Disabling auto-braking allows for continuous movement
		// between points (ie, the agent doesn't slow down as it
		// approaches a destination point).
		//agent.autoBraking = false;

		run_speed = agent.speed + speed_increase;
		walk_speed = agent.speed;
		iframes = 0;
		dropped = false;
		dropTimer = 0;
		footStep = GetComponents<AudioSource> () [0];
		Death = GetComponents<AudioSource> () [1];
		Hit = GetComponents<AudioSource> () [2];
		stab = GetComponents<AudioSource> () [3];
		caught = GetComponents<AudioSource> () [4];
		seen = false;
		GotoNextPoint ();
	
	}
    public void updateHPBar()
    {
        try //since some enemies might not have the healthbar merged in yet
        {
            if (!enemyHUDCanvas.enabled) {
                enemyHUDCanvas.enabled = true;
                hudTimer = -1;
                    }
            enemyHUDCanvas.GetComponentInChildren<Slider>().value = hp;
        }
        catch (System.Exception)
        {

        }
    }
    public void clearHud()
    {
        if (hudTimer >= 0)
        {
            hudTimer -= Time.deltaTime;
            if (hudTimer <= 0)
                enemyHUDCanvas.enabled = false;
        }
    }
    public void hit ()
	{
	
		if (iframes > 0 || anim.GetBool("dead"))
			return;

		iframes += Time.deltaTime;
		hp = hp - 10;
        updateHPBar();
        anim.SetTrigger ("hit");

		if (hp <= 0) {
			if (!GameMaster.paused) {
				agent.isStopped = true;
			}
            enemyHUDCanvas.enabled = false;
            anim.SetBool ("dead", true);
			anim.SetBool ("run", false);
			getChildSwordGameObject ().GetComponent<EnemyWeaponScript> ().danger = false;
			dropTimer += Time.deltaTime;
			footStep.Stop ();
			Death.Play ();

		} else {
			Hit.Play ();
		}
	}


	void OnTriggerStay (Collider c)
	{
		if (!GameMaster.paused) {
			if (anim.GetBool ("dead"))
				return;
	
			RaycastHit hit;

			Vector3 fwd = transform.position - c.transform.position;
			if (Physics.Raycast (transform.position, -fwd , out hit)) {

				if (!hit.transform.gameObject.CompareTag ("Player") && !hit.transform.gameObject.CompareTag ("PrinceWeapon"))
					return;
			
			}

			if (!seen) {

				caught.Play ();
				seen = true;
			}

			transform.LookAt (c.gameObject.transform.position);
			anim.SetBool ("run", true);
			anim.SetBool ("walk", false);
			target_prince = true;
			agent.destination = c.gameObject.transform.position;
			agent.speed = run_speed;
        }
	}

	void OnTriggerExit (Collider c)
	{
		if (!GameMaster.paused) {
			if (c.gameObject.CompareTag ("Player") || c.gameObject.CompareTag ("PrinceWeapon")) {
				target_prince = false;
				anim.SetBool ("run", false);
				anim.SetBool ("walk", true);
				agent.speed = walk_speed;
                if (enemyHUDCanvas.enabled)
                    hudTimer = 6;
            }
        }
	}


	void GotoNextPoint ()
	{
		// Returns if no points have been set up
		if (points.Length == 0)
			return;

		// Set the agent to go to the currently selected destination.
		agent.destination = points [destPoint].position;

		// Choose the next point in the array as the destination,
		// cycling to the start if necessary.
		destPoint = (destPoint + 1) % points.Length;
	}

	public GameObject getChildSwordGameObject ()
	{
		
		Transform[] ts = transform.GetComponentsInChildren<Transform> (true);
		foreach (Transform t in ts)
			if (t.gameObject.name == "Sword_joint")
				return t.gameObject;
		return null;
	}


	// Update is called once per frame
	void Update ()
	{
        clearHud();
        if (iframes > 0) {
			iframes += Time.deltaTime;
		}

		if (iframes > 0.5f) {
		
			iframes = 0;
		}
		if (!GameMaster.paused) {
			if (anim.GetBool ("dead")) {
		
				if (dropTimer > 0) {
			
					dropTimer += Time.deltaTime;
				}

				if (dropTimer > 2.3f) {
			
					if (!dropped) {

						Instantiate (drop, transform.position, drop.transform.rotation);
						dropped = true;
				
					}
			
				}
		
				return;
			}
			
		
			GameObject sword = getChildSwordGameObject ();
			if (anim.GetCurrentAnimatorStateInfo (0).IsName ("stab") && anim.GetCurrentAnimatorStateInfo (0).normalizedTime < 0.3) {
				
				if(!stab.isPlaying)
					stab.Play ();
				sword.GetComponent<EnemyWeaponScript> ().danger = true;
			} else {

				sword.GetComponent<EnemyWeaponScript> ().danger = false;
			}
			if (!anim.GetCurrentAnimatorStateInfo (0).IsName ("death") && hp<0) {
				anim.SetBool ("dead", true);
				agent.isStopped = true;
			}

			if (anim.GetCurrentAnimatorStateInfo (0).IsName ("stab")) {
				agent.isStopped = true;
			} else {

				agent.isStopped = false;
			}

			if (target_prince) {
		
				if (agent.remainingDistance < 1.5f) {
					anim.SetTrigger ("stab");

					anim.SetBool ("run", false);
					anim.SetBool ("walk", false);
					agent.isStopped = true;
					target_prince = false;
					return;
				} else {
					
					if (!anim.GetCurrentAnimatorStateInfo (0).IsName ("stab")) {
						agent.isStopped = false;
						anim.SetBool ("run", true);
						anim.SetBool ("walk", false);
					}
					return;
				}
		
			}


			// Choose the next destination point when the agent gets
			// close to the current one.
			if (!agent.pathPending && agent.remainingDistance < 0.5f)
				GotoNextPoint ();

		}
	}
}
