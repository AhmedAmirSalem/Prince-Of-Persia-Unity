using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class GameMaster : MonoBehaviour {

	public static int hp = 100;
	public static bool keys;
	public static int sands = 0;
	public AudioMixer mainMixer;
	public static bool paused;
	public static AudioSource collect;
   // public static bool isCamIntroEnd=false;
	// Use this for initialization
	void Start () {

		hp = 100;
		keys = false;
		sands = 0;
		collect = GetComponent<AudioSource> ();
		mainMixer.SetFloat("musicVol", PlayerPrefs.GetFloat("musicVol",-29f));
		mainMixer.SetFloat("speechVol", PlayerPrefs.GetFloat("speechVol",0.85f));
		mainMixer.SetFloat("sfxVol", PlayerPrefs.GetFloat("effectVol",0.7f));

		DontDestroyOnLoad (gameObject);
		
	}

	public static void Pause(){
		GameObject[] g = GameObject.FindGameObjectsWithTag ("Pausable");
		for (int i = 0; i < g.Length; i++) {
			GameObject c = g [i];
			Animator anim = c.GetComponent<Animator> ();
			if (anim != null) {
				anim.speed = 0;
			}
			NavMeshAgent a = c.GetComponent<NavMeshAgent> ();
			if (a != null) {
				a.enabled = false;
			}
			Animation ani = c.GetComponent<Animation> ();
			if (ani != null) {
				ani.enabled = false;
			}
			trapactivate trap = c.GetComponent<trapactivate> ();
			if (trap != null) {
				trap.enabled = false;
			}
			hodoor door = c.GetComponent<hodoor> ();
			if (door != null) {
				door.enabled = false;
			}
			firetrap fire = c.GetComponent<firetrap> ();
			if (fire != null) {
				fire.enabled = false;
			}
		}
		paused = true;
	}
	public static void Unpause(){
		GameObject[] g = GameObject.FindGameObjectsWithTag ("Pausable");
		for (int i = 0; i < g.Length; i++) {
			GameObject c = g [i];
			Animator anim = c.GetComponent<Animator> ();
			if (anim != null) {
				anim.speed = 1;
			}
			NavMeshAgent a = c.GetComponent<NavMeshAgent> ();
			if (a != null) {
				a.enabled = true;
			}
			Animation ani = c.GetComponent<Animation> ();
			if (ani != null) {
				ani.enabled= true;
			}
			trapactivate trap = c.GetComponent<trapactivate> ();
			if (trap != null) {
				trap.enabled = true;
			}
			hodoor door = c.GetComponent<hodoor> ();
			if (door != null) {
				door.enabled = true;
			}
		}
		paused = false;
	}
	// Update is called once per frame
	void Update () {


	}
}
