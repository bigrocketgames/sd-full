using UnityEngine;
using System.Collections;

public class EnemyLaser : MonoBehaviour {

	private PlayerController playerController;
	private StarbaseController starbaseController;
	private float laserDamage;
	
	// Use this for initialization
	void Start () {
		
	}
	
	void OnTriggerEnter2D (Collider2D coll)
	{
		if(coll.gameObject.tag == "Player")
		{
			playerController = coll.gameObject.GetComponent<PlayerController>();
			playerController.DoDamage(1);
			Destroy(gameObject);
		}
		else if(coll.gameObject.tag == "Starbase")
		{
			starbaseController = coll.gameObject.GetComponentInParent<StarbaseController>();
			starbaseController.StarbaseDamage(1);
			Destroy(gameObject);
		}
	}
}
