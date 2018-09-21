using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class trapactivate : MonoBehaviour {

	 void OnTriggerEnter(Collider other)
    {
		if (!enabled)
			return;
        print("-----------------------");
        print(other.gameObject.name);
        print(other.gameObject.tag);
        print("-----------------------");
        if(other.name.Equals("RightHit") || other.name.Equals("LeftHit"))
        {
            GameObject.Find("SPT1").GetComponent<Animation>().enabled = true;
            GameObject.Find("SPT2").GetComponent<Animation>().enabled = true;
            GameObject.Find("SPT3").GetComponent<Animation>().enabled = true;

        }
    }
}
