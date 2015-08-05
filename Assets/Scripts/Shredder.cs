using UnityEngine;
using System.Collections;

public class Shredder : MonoBehaviour {

	void OnTriggerEnter2D (Collider2D coll)
	{
		if(coll.gameObject.tag == "Laser" || coll.gameObject.tag == "EnemyLaser")
		{
			Destroy(coll.gameObject);
		}
	}
}
