using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class firetrap : MonoBehaviour {

    void OnTriggerEnter(Collider c)
    {
		if (!enabled)
			return;
        if (c.name.Equals("RightHit") || c.name.Equals("LeftHit"))
        {
            GameObject.Find("FireTrap0").transform.GetChild(0).gameObject.SetActive(true);
            GameObject.Find("FireTrap1").transform.GetChild(0).gameObject.SetActive(true);
            GameObject.Find("FireTrap2").transform.GetChild(0).gameObject.SetActive(true);
        }
    }
}
