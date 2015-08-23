using UnityEngine;
using System.Collections;

public class BossController : MonoBehaviour {
	
	public GameObject bossLaserOrange;
	public GameObject bossLaserYellow;
	public GameObject bossLaserRed;
	public GameObject bossLaserBlue;
	public GameObject bossLaserGreen;
	public AudioClip shipExplode;
	
	private GameScene gameScene;
	
	private int baseHealth = 30;
	private int health;
	private int difficulty;
	private int wave;
	
	private float randomX;
	private float randomY;
	private float speed = 1f;
	private bool isDead = false;
	private int explosionCount;
	
	private float repeatFireRate;
	private float laserSpeed = 4.0f;
	private Animator shipExplodeAnim;
	
	private ScoreManager scoreManager;
	private SFXManager sfxManager;
	
	// Use this for initialization
	void Start () {
		shipExplodeAnim = GetComponent<Animator>();
		gameScene = FindObjectOfType<GameScene>();
		sfxManager = FindObjectOfType<SFXManager>();
		wave = gameScene.GetWave();
		difficulty = PlayerPrefsManager.GetDifficulty();
		DetermineHealth();
		
		string gameMode = PlayerPrefsManager.GetGameMode();
		{
			if(gameMode == "Boss")
			{
				repeatFireRate = 2.0f - (0.1f * wave);
			}
			else
			{
				repeatFireRate = 2.0f - (0.01f * wave);
			}
		}
		
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
		switch (this.tag)
		{
		case "Boss1":
			Vector3 boss1BeamOffset1 = new Vector3(-0.52f, -0.79f, 0);
			Vector3 boss1BeamOffset2 = new Vector3(0.52f, -0.79f, 0);
			GameObject boss1Beam1 = Instantiate(bossLaserOrange, transform.position + boss1BeamOffset1, Quaternion.identity) as GameObject;
			GameObject boss1Beam2 = Instantiate(bossLaserOrange, transform.position + boss1BeamOffset2, Quaternion.identity) as GameObject;
			boss1Beam1.GetComponent<Rigidbody2D>().velocity = new Vector3 (0, -laserSpeed, 0);
			boss1Beam2.GetComponent<Rigidbody2D>().velocity = new Vector3 (0, -laserSpeed, 0);
			break;
		case "Boss2":
			Vector3 boss2BeamOffset1 = new Vector3(-0.478f, -0.69f, 0);
			Vector3 boss2BeamOffset2 = new Vector3(0.515f, -0.69f, 0);
			GameObject boss2Beam1 = Instantiate(bossLaserYellow, transform.position + boss2BeamOffset1, Quaternion.identity) as GameObject;
			GameObject boss2Beam2 = Instantiate(bossLaserYellow, transform.position + boss2BeamOffset2, Quaternion.identity) as GameObject;
			boss2Beam1.GetComponent<Rigidbody2D>().velocity = new Vector3 (0, -laserSpeed, 0);
			boss2Beam2.GetComponent<Rigidbody2D>().velocity = new Vector3 (0, -laserSpeed, 0);
			break;
		case "Boss3":
			Vector3 boss3BeamOffset1 = new Vector3(-0.519f, -0.676f, 0);
			Vector3 boss3BeamOffset2 = new Vector3(0.499f, -0.676f, 0);
			GameObject boss3Beam1 = Instantiate(bossLaserRed, transform.position + boss3BeamOffset1, Quaternion.identity) as GameObject;
			GameObject boss3Beam2 = Instantiate(bossLaserRed, transform.position + boss3BeamOffset2, Quaternion.identity) as GameObject;
			boss3Beam1.GetComponent<Rigidbody2D>().velocity = new Vector3 (0, -laserSpeed, 0);
			boss3Beam2.GetComponent<Rigidbody2D>().velocity = new Vector3 (0, -laserSpeed, 0);
			break;
		case "Boss4":
			Vector3 boss4BeamOffset1 = new Vector3(-0.007f, -1.154f, 0);
			Vector3 boss4BeamOffset2 = new Vector3(-0.288f, -0.687f, 0);
			Vector3 boss4BeamOffset3 = new Vector3(0.276f, -0.687f, 0);
			GameObject boss4Beam1 = Instantiate(bossLaserBlue, transform.position + boss4BeamOffset1, Quaternion.identity) as GameObject;
			GameObject boss4Beam2 = Instantiate(bossLaserBlue, transform.position + boss4BeamOffset2, Quaternion.identity) as GameObject;
			GameObject boss4Beam3 = Instantiate(bossLaserBlue, transform.position + boss4BeamOffset3, Quaternion.identity) as GameObject;
			boss4Beam1.GetComponent<Rigidbody2D>().velocity = new Vector3 (0, -laserSpeed, 0);
			boss4Beam2.GetComponent<Rigidbody2D>().velocity = new Vector3 (0, -laserSpeed, 0);
			boss4Beam3.GetComponent<Rigidbody2D>().velocity = new Vector3 (0, -laserSpeed, 0);
			break;
		case "Boss5":
			Vector3 boss5BeamOffset1 = new Vector3(0.294f, -0.508f, 0);
			Vector3 boss5BeamOffset2 = new Vector3(-0.32f, -0.508f, 0);
			Vector3 boss5BeamOffset3 = new Vector3(-0.007f, -0.696f, 0);
			Vector3 boss5BeamOffset4 = new Vector3(1.057f, 0.062f, 0);
			Vector3 boss5BeamOffset5 = new Vector3(-1.069f, 0.062f, 0);
			GameObject boss5Beam1 = Instantiate(bossLaserGreen, transform.position + boss5BeamOffset1, Quaternion.identity) as GameObject;
			GameObject boss5Beam2 = Instantiate(bossLaserGreen, transform.position + boss5BeamOffset2, Quaternion.identity) as GameObject;
			GameObject boss5Beam3 = Instantiate(bossLaserGreen, transform.position + boss5BeamOffset3, Quaternion.identity) as GameObject;
			GameObject boss5Beam4 = Instantiate(bossLaserGreen, transform.position + boss5BeamOffset4, Quaternion.identity) as GameObject;
			GameObject boss5Beam5 = Instantiate(bossLaserGreen, transform.position + boss5BeamOffset5, Quaternion.identity) as GameObject;
			boss5Beam1.GetComponent<Rigidbody2D>().velocity = new Vector3 (0, -laserSpeed, 0);
			boss5Beam2.GetComponent<Rigidbody2D>().velocity = new Vector3 (0, -laserSpeed, 0);
			boss5Beam3.GetComponent<Rigidbody2D>().velocity = new Vector3 (0, -laserSpeed, 0);
			boss5Beam4.GetComponent<Rigidbody2D>().velocity = new Vector3 (0, -laserSpeed, 0);
			boss5Beam5.GetComponent<Rigidbody2D>().velocity = new Vector3 (0, -laserSpeed, 0);
			break;
		}
			
		
		
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
		
		health = baseHealth;
		
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
