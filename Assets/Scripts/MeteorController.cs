using UnityEngine;
using System.Collections;

public class MeteorController : MonoBehaviour {
	
	public GameObject brownDust;
	public GameObject greyDust;
	public AudioClip explosion;
	
	private float speed = 3f;
	private float health = 3f;
	private StarbaseController starbaseController;
	private EnemyController enemyController;
	private SFXManager sfxManager;
	
	// Use this for initialization
	void Start () {
		sfxManager = FindObjectOfType<SFXManager>();
	}
	
	// Update is called once per frame
	void FixedUpdate () 
	{
		transform.position = new Vector3(transform.position.x, transform.position.y - speed * Time.deltaTime, transform.position.z);
	}
	
	void OnTriggerEnter2D (Collider2D coll)
	{
		if(this.transform.position.y <= 5f)
		{	
			if(coll.gameObject.tag == "Easy" || coll.gameObject.tag == "Medium" || coll.gameObject.tag == "Hard")
			{
				enemyController = coll.gameObject.GetComponent<EnemyController>();
				bool isDead = enemyController.IsDead();
				
				if (!isDead && coll.transform.position.y <= 5f)
				{
					enemyController.KillEnemyNoBonus();
					KillMeteor();
				}
				else
				{
					return;
				}
			}
			else if(coll.gameObject.tag == "Player")
			{
				KillMeteorNoPoints();
			}
			
			if(coll.gameObject.tag == "Laser")
			{
				DamageMeteor();
				Destroy(coll.gameObject);
			}
			else if(coll.gameObject.tag == "Starbase")
			{
				starbaseController = coll.gameObject.GetComponentInParent<StarbaseController>();
				starbaseController.StarbaseDamage(2);
				KillMeteorNoPoints();
			}
		}	
	}
	
	void DamageMeteor()
	{
		health--;
		
		if(health <= 0)
		{
			KillMeteor();
		}
	}
	
	void KillMeteor()
	{
		if(this.tag == "BrownMeteor")
		{
			GameObject meteorDust = Instantiate(brownDust, gameObject.transform.position, Quaternion.identity) as GameObject;
		}
		else if (this.tag == "GreyMeteor")
		{
			GameObject meteorDust = Instantiate(greyDust, gameObject.transform.position, Quaternion.identity) as GameObject;
		}
		
		sfxManager.PlaySFX(explosion);
		ScoreManager scoreManager = GameObject.FindObjectOfType<ScoreManager>();
		scoreManager.SetScoreText(1000);
		PowerUpSpawner powerUpSpawner = GameObject.FindObjectOfType<PowerUpSpawner>();
		powerUpSpawner.StarbasePowerUp(transform.position);
		Destroy(gameObject);
	}
	
	void KillMeteorNoPoints()
	{
		if(this.tag == "BrownMeteor")
		{
			GameObject meteorDust = Instantiate(brownDust, gameObject.transform.position, Quaternion.identity) as GameObject;
		}
		else if (this.tag == "GreyMeteor")
		{
			GameObject meteorDust = Instantiate(greyDust, gameObject.transform.position, Quaternion.identity) as GameObject;
		}
		
		sfxManager.PlaySFX(explosion);
		Destroy(gameObject);
	}
}
