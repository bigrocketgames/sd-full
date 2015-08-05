using UnityEngine;
using System.Collections;

public class EnemyController : MonoBehaviour {
	
	public GameObject blueEnemyLaser;
	public GameObject greenEnemyLaser;
	public GameObject redEnemyLaser;
	public AudioClip shipExplode;
	
	private GameScene gameScene;
	private PlayerLaser playerLaser;
	
	private int baseHealth = 10;
	private int health;
	private int difficulty;
	private int wave;
	
	private float xmin;
	private float xmax;
	private float ymin = -2.25f;
	private float ymax = 8;
	private float randomX;
	private float randomY;
	private float speed = 1f;
	private float padding = 0.5f;
	private Camera camera;
	private float distance;
	private bool isDead = false;
	
	private Vector3 offset = new Vector3(0f, 0.5f, 0f);
	private GameObject beam;
	private float repeatFireRate;
	private float laserSpeed = 3.0f;
	private Animator shipExplodeAnim;
	private PolygonCollider2D shipCollider;
	
	private ScoreManager scoreManager;
	private StarbaseController starbaseController;
	private SFXManager sfxManager;
	
	// Use this for initialization
	void Start () {
		shipExplodeAnim = GetComponent<Animator>();
		shipCollider = GetComponent<PolygonCollider2D>();
		gameScene = FindObjectOfType<GameScene>();
		sfxManager = FindObjectOfType<SFXManager>();
		wave = gameScene.GetWave();
		difficulty = PlayerPrefsManager.GetDifficulty();
		DetermineHealth();
		
		camera = Camera.main;
		distance = transform.position.z - camera.transform.position.z;
		xmin = camera.ViewportToWorldPoint(new Vector3(0,0,distance)).x + padding;
		xmax = camera.ViewportToWorldPoint(new Vector3(1,1,distance)).x - padding;
		
		repeatFireRate = 3.0f - (0.01f * wave);
		laserSpeed = 3.0f + (0.03f * wave);
		InvokeRepeating("NextDestination",0f,1.5f);
		InvokeRepeating("EnemyFire",0f,repeatFireRate);
		
		scoreManager = GameObject.FindObjectOfType<ScoreManager>();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		Vector3 shipPos = transform.position;
		float diffx = randomX - shipPos.x;
		float diffy = randomY - shipPos.y;
		
		if(!isDead)
		{
			if((diffx > -0.2 && diffx < 0.2) && (diffy > -0.2 && diffy < 0.2))
			{
				CancelInvoke();
				InvokeRepeating("NextDestination",0f,1.5f);
				InvokeRepeating("EnemyFire",0f,repeatFireRate);
			}
			
			if(diffx > 0.2)
			{
				transform.position = new Vector3(Mathf.Clamp(transform.position.x + speed * Time.deltaTime, xmin, xmax), transform.position.y, transform.position.z);
			}
			else if(diffx < -0.2)
			{
				transform.position = new Vector3(Mathf.Clamp(transform.position.x - speed * Time.deltaTime, xmin, xmax), transform.position.y, transform.position.z);
			}
			
			if(diffy > 0.2)
			{
				transform.position = new Vector3(transform.position.x, Mathf.Clamp(transform.position.y + speed * Time.deltaTime, ymin, ymax), transform.position.z);
			}
			else if(diffy < -0.2)
			{
				transform.position = new Vector3(transform.position.x, Mathf.Clamp(transform.position.y - speed * Time.deltaTime, ymin, ymax), transform.position.z);
			}
		else
			return;
		}
	}
	
	void EnemyFire()
	{
		int enemyColorChoice = Random.Range(1,4);
		
		if(enemyColorChoice == 1)
		{
			beam = Instantiate(blueEnemyLaser, transform.position - offset, Quaternion.identity) as GameObject;
		}
		else if(enemyColorChoice == 2)
		{
			beam = Instantiate(greenEnemyLaser, transform.position - offset, Quaternion.identity) as GameObject;
		}
		else if(enemyColorChoice == 3)
		{
			beam = Instantiate(redEnemyLaser, transform.position - offset, Quaternion.identity) as GameObject;
		}
		beam.GetComponent<Rigidbody2D>().velocity = new Vector3 (0, -laserSpeed, 0);
	}
	
	public void DoDamage(int damage)
	{
		if(!isDead)
		{
			health -= damage;
			
			if(health <= 0)
			{
				KillEnemy();
			}
		}
	}
	
	private void DetermineHealth()
	{
		
		health = (baseHealth * difficulty) + (wave * 5);
		if (this.tag == "Medium")
		{
			health *= (int)1.5f;
		}
		else if(this.tag == "Hard")
		{
			health *= 2;
		}
	}
	
	private void NextDestination()
	{
		randomX = Random.Range(xmin, xmax);
		randomY = Random.Range(ymin, camera.ViewportToWorldPoint(new Vector3(1,1,distance)).y - padding);
	}
	
//	void OnTriggerEnter2D (Collider2D coll)
//	{		
//		if(this.transform.position.y <= 5f)
//		{	
//			if(coll.gameObject.tag == "Player")
//			{
//				if(!isDead)
//				{
//					KillEnemy();
//				}
//			}
//			else if(coll.gameObject.tag == "Meteor")
//			{
//				if(!isDead)
//				{
//					KillEnemyNoBonus();
//				}
//			}
//		}
//	}
	
	public void KillEnemy()
	{
		isDead = true;
		CancelInvoke();
		sfxManager.PlaySFX(shipExplode);
		transform.position = transform.position;
		scoreManager.SetScoreText(this.tag);
		PowerUpSpawner powerUpSpawner = GameObject.FindObjectOfType<PowerUpSpawner>();
		powerUpSpawner.ShipPowerUp(transform.position);
		EnemySpawner enemySpawner = GameObject.FindObjectOfType<EnemySpawner>();
		enemySpawner.EnemyDead();
		shipExplodeAnim.SetTrigger("isExploding");
	}
	
	public void KillEnemyNoBonus()
	{
		isDead = true;
		CancelInvoke();
		sfxManager.PlaySFX(shipExplode);
		transform.position = transform.position;
		EnemySpawner enemySpawner = GameObject.FindObjectOfType<EnemySpawner>();
		enemySpawner.EnemyDead();
		shipExplodeAnim.SetTrigger("isExploding");
		
	}
	
	public void DestroyEnemyObject()
	{
		Destroy(this.gameObject);
	}
	
	public bool IsDead()
	{
		return isDead;
	}
}
