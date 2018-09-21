using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hayCol : MonoBehaviour {

	void OnCollisionEnter(Collision c)
    {
        if (c.gameObject.tag.Equals("Player"))
        {
            GameObject.Destroy(this.gameObject);
        }
    }
}
