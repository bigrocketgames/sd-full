using UnityEngine;
using System.Collections;

public class EnemyController : MonoBehaviour {
	
	public GameObject[] enemyLasers;
	public AudioClip shipExplode;
	
	private GameScene gameScene;
	private PlayerLaser playerLaser;
	
	private int baseHealth = 1;
	private int health;
	private int difficulty;
	private int wave;
	
	private float xmin;
	private float xmax;
	private float ymin = -2.25f;
	//private float ymax = 8;
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
		
		repeatFireRate = 2.5f - (0.01f * wave);
		laserSpeed = 3.0f + (0.03f * wave);
		InvokeRepeating("NextDestination",0f,1.0f);
		InvokeRepeating("EnemyFire",0f,repeatFireRate);
		
		scoreManager = GameObject.FindObjectOfType<ScoreManager>();
	}
	
	// Update is called once per frame
	void FixedUpdate () {

		if(!isDead)
		{
			transform.position = new Vector3(Mathf.MoveTowards(transform.position.x,randomX,Time.deltaTime*speed),Mathf.MoveTowards(transform.position.y,randomY,Time.deltaTime*speed),transform.position.z);
		}
		else
		{
			return;
		}
	}
	
	void EnemyFire()
	{
		int enemyColorChoice = Random.Range(0,3);
		
		beam = Instantiate(enemyLasers[enemyColorChoice], transform.position - offset, Quaternion.identity) as GameObject;
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
		
		health = baseHealth;
		
		if (this.tag == "Medium")
		{
			health *= 2;
		}
		else if(this.tag == "Hard")
		{
			health *= 3;
		}
	}
	
	private void NextDestination()
	{
		randomX = Random.Range(xmin, xmax);
		randomY = Random.Range(ymin, camera.ViewportToWorldPoint(new Vector3(1,1,distance)).y - padding);
	}
	
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
