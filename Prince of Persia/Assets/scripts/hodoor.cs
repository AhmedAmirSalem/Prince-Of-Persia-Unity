using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hodoor : MonoBehaviour {

    bool door = false;

    void OnTriggerEnter(Collider c)
    {
		if (!GameMaster.keys || GameMaster.paused)
			return;
        if (c.name.Equals("RightHit") || c.name.Equals("LeftHit"))
        {
            door = true;
            GameObject.Find("AxeTrap").GetComponent<Animation>().enabled = true;
        }
    }

    void Update()
    {
        if (door)
        {
            if (GameObject.Find("HODOOR").transform.rotation.eulerAngles.y > 197) door = false;
            GameObject.Find("HODOOR").transform.RotateAround(new Vector3(-20.29f, 46.63f, 187.6849f), Vector3.up, 20 * Time.deltaTime);
        }
    }
}
