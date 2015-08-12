using UnityEngine;
using System.Collections;

public class BossController : MonoBehaviour {
	
	public GameObject blueEnemyLaser;
	public GameObject greenEnemyLaser;
	public GameObject redEnemyLaser;
	public AudioClip shipExplode;
	
	private GameScene gameScene;
	private PlayerLaser playerLaser;
	
	private int baseHealth = 100;
	private int health;
	private int difficulty;
	private int wave;
	
	private float randomX;
	private float randomY;
	private float speed = 1f;
	private bool isDead = false;
	private int explosionCount;
	
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
		
		repeatFireRate = 3.0f - (0.01f * wave);
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
				KillBoss();
			}
		}
	}
	
	private void DetermineHealth()
	{
		
		health = (baseHealth * difficulty) + (wave * 20);
		
		if (difficulty == 2)
		{
			health *= (int)1.5f;
		}
		else if(difficulty == 3)
		{
			health *= 2;
		}
	}
	
	private void NextDestination()
	{
		randomX = Random.Range(-1.63f, 1.63f);
		randomY = Random.Range(2.5f, 4.25f);
	}
	
	public void KillBoss()
	{
		isDead = true;
		CancelInvoke();
		sfxManager.PlaySFX(shipExplode);
		transform.position = transform.position;
		scoreManager.SetScoreText(this.tag);
		explosionCount = 0;
		shipExplodeAnim.SetTrigger("beginBossExplode");
	}
	
	public void nextExplosion()
	{
		explosionCount++;
		bossBeginExplode bossBeginExplosion = GetComponentInChildren<bossBeginExplode>();
		bossBeginExplosion.ExplosionOffset(explosionCount);
		
		if(explosionCount < 5)
		{
			sfxManager.PlaySFX(shipExplode);
			shipExplodeAnim.SetTrigger("beginBossExplode");
		}
		else
		{
			EnemySpawner enemySpawner = GameObject.FindObjectOfType<EnemySpawner>();
			enemySpawner.EnemyDead();
			sfxManager.PlaySFX(shipExplode);
			shipExplodeAnim.SetTrigger("bossExploding");
		}
	}
	
	public void DestroyBossObject()
	{
		Destroy(this.gameObject);
	}
	
	public bool IsDead()
	{
		return isDead;
	}
}
