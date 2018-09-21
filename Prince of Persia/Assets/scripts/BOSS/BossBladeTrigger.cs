using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBladeTrigger : MonoBehaviour {
    public GameObject cutSceneObj;
    //public Camera CMBrain;
    void OnTriggerEnter(Collider c)
    {
        if (c.name.Equals("RightHit") || c.name.Equals("LeftHit"))
        {
            cutSceneObj.GetComponent<bossCutscene>().enabled = true;
            //CMBrain.enabled = true;
            GameObject.Find("blade1").transform.GetChild(1).gameObject.SetActive(true);
            GameObject.Find("blade2").transform.GetChild(1).gameObject.SetActive(true);
            GameObject.Find("blade3").transform.GetChild(1).gameObject.SetActive(true);
            GameObject.Find("blade4").transform.GetChild(1).gameObject.SetActive(true);
            GameObject.Find("blade5").transform.GetChild(1).gameObject.SetActive(true);
            GameObject.Find("blade6").transform.GetChild(1).gameObject.SetActive(true);
            GameObject.Find("blade7").transform.GetChild(1).gameObject.SetActive(true);

            //GameObject.Find("blade1").GetComponent<Animation>().enabled = true;
            //GameObject.Find("blade2").GetComponent<Animation>().enabled = true;
            //GameObject.Find("blade3").GetComponent<Animation>().enabled = true;
            //GameObject.Find("blade4").GetComponent<Animation>().enabled = true;
            //GameObject.Find("blade5").GetComponent<Animation>().enabled = true;
            //GameObject.Find("blade6").GetComponent<Animation>().enabled = true;
            //GameObject.Find("blade7").GetComponent<Animation>().enabled = true;
        }
    }
}
