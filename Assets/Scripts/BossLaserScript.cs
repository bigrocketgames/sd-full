using UnityEngine;
using System.Collections;

public class BossLaserScript : MonoBehaviour {

	private PlayerController playerController;
	private StarbaseController starbaseController;
	
	void OnTriggerEnter2D (Collider2D coll)
	{
		if(coll.gameObject.tag == "Player")
		{
			playerController = coll.gameObject.GetComponent<PlayerController>();
			playerController.DoDamage(2);
			Destroy(gameObject);
		}
		else if(coll.gameObject.tag == "Starbase")
		{
			starbaseController = coll.gameObject.GetComponentInParent<StarbaseController>();
			starbaseController.StarbaseDamage(2);
			Destroy(gameObject);
		}
	}
}
