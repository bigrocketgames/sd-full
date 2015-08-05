using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour {
	
	public Sprite blue1;
	public Sprite green1;
	public Sprite orange1;
	public Sprite red1;
	public Sprite blue2;
	public Sprite green2;
	public Sprite orange2;
	public Sprite red2;
	public Sprite blue3;
	public Sprite green3;
	public Sprite orange3;
	public Sprite red3;
	
	public GameObject laserBlue;
	public GameObject laserGreen;
	public GameObject laserOrange;
	public GameObject laserRed;
	public GameObject missle;
	
	public GameObject playerShield;
	public GameObject playerHealthBar;
	public Slider playerHealthSlider;
	public Image playerHealthFill;
	public AudioClip playerLaserSound;	
	
	private float speed = 5.0f;
	private float padding = 0.5f;
	private float xmax;
	private float xmin;
	private float ymax;
	private float ymin;
	
	private float weaponRepeatRate = 0.5f;
	private string playerLaser;
	private string playerMissile;
	private GameObject playerLaserColor;
	private float laserSpeed = 10;
	private Vector3 offset = new Vector3(0,0.5f, 0);
	private Vector3 playerHealthBarOffset;
	
	private SFXManager sfxManager;
	private int health;
	private int startHealth = 10;
	private int lives = 5;
	private SpriteRenderer shipSprite;
	private ScoreManager scoreManager;
	private ShipShieldController shipShieldController;
	private EnemyController enemyController;
	private bool shieldsUp = false;
	
	// Use this for initialization
	void Start () {
		playerShield.SetActive(false);
		Camera camera = Camera.main;
		float distance = transform.position.z - camera.transform.position.z;
		xmin = camera.ViewportToWorldPoint(new Vector3(0,0,distance)).x + padding;
		xmax = camera.ViewportToWorldPoint(new Vector3(1,1,distance)).x - padding;
		ymin = camera.ViewportToWorldPoint(new Vector3(0,0,distance)).y + 1.0f;
		ymax = camera.ViewportToWorldPoint(new Vector3(1,1,distance)).y - 1.0f;
		
		shipSprite = GetComponent<SpriteRenderer>();
		sfxManager = GameObject.FindObjectOfType<SFXManager>();
		setPlayerLaser("1laser");
		InvokeRepeating("FireLaser", 0.5f, weaponRepeatRate);
		
		scoreManager = GameObject.FindObjectOfType<ScoreManager>();
		scoreManager.SetLivesLeftText(lives);
		
		playerHealthBarOffset = new Vector3 (0f, -0.5f, 0);
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		
		playerHealthBar.transform.position = transform.position + playerHealthBarOffset;
		
		Vector3 shipPos = transform.position;
		#if UNITY_EDITOR || UNITY_STANDALONE || UNITY_WEBPLAYER
		Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

		if(Input.GetMouseButton(0))
		{
			float diffx = mousePos.x - shipPos.x;
			float diffy = mousePos.y - shipPos.y;
			
			if(diffx > .5)
			{
				transform.position = new Vector3(Mathf.Clamp(transform.position.x + speed * Time.deltaTime, xmin, xmax), transform.position.y, transform.position.z);
			}
			else if(diffx < -.5)
			{
				transform.position = new Vector3(Mathf.Clamp(transform.position.x - speed * Time.deltaTime, xmin, xmax), transform.position.y, transform.position.z);
			}
			else
			{
				transform.position = transform.position;
			}
			
			if(diffy > .5)
			{
				transform.position = new Vector3(transform.position.x, Mathf.Clamp(transform.position.y + speed * Time.deltaTime, ymin, ymax), transform.position.z);
			}
			else if(diffy < -.5)
			{
				transform.position = new Vector3(transform.position.x, Mathf.Clamp(transform.position.y - speed * Time.deltaTime, ymin, ymax), transform.position.z);
			}
			else
			{
				transform.position = transform.position;
			}
		}
		
		if(Input.GetKey(KeyCode.RightArrow))
		{
			transform.position = new Vector3(Mathf.Clamp(transform.position.x + speed * Time.deltaTime, xmin, xmax), transform.position.y, transform.position.z);
		}
		else if(Input.GetKey(KeyCode.LeftArrow))
		{
			transform.position = new Vector3(Mathf.Clamp(transform.position.x - speed * Time.deltaTime, xmin, xmax), transform.position.y, transform.position.z);
		}
		
		if(Input.GetKey(KeyCode.UpArrow))
		{
			transform.position = new Vector3(transform.position.x, Mathf.Clamp(transform.position.y + speed * Time.deltaTime, ymin, ymax), transform.position.z);
		}
		else if(Input.GetKey(KeyCode.DownArrow))
		{
			transform.position = new Vector3(transform.position.x, Mathf.Clamp(transform.position.y - speed * Time.deltaTime, ymin, ymax), transform.position.z);
		}
		#endif
		
		#if UNITY_IOS || UNITY_ANDROID ||UNITY_WP_8_1
		Vector3 touchPos = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);
		{
			if(Input.GetTouch(0).phase == TouchPhase.Began || Input.GetTouch(0).phase == TouchPhase.Moved || Input.GetTouch(0).phase == TouchPhase.Stationary)
			{
				float diffx = touchPos.x - shipPos.x;
				float diffy = touchPos.y - shipPos.y;
				
				if(diffx > .5)
				{
					transform.position = new Vector3(Mathf.Clamp(transform.position.x + speed * Time.deltaTime, xmin, xmax), transform.position.y, transform.position.z);
				}
				else if(diffx < -.5)
				{
					transform.position = new Vector3(Mathf.Clamp(transform.position.x - speed * Time.deltaTime, xmin, xmax), transform.position.y, transform.position.z);
				}
				
				if(diffy > .5)
				{
					transform.position = new Vector3(transform.position.x, Mathf.Clamp(transform.position.y + speed * Time.deltaTime, ymin, ymax), transform.position.z);
				}
				else if(diffy < -.5)
				{
					transform.position = new Vector3(transform.position.x, Mathf.Clamp(transform.position.y - speed * Time.deltaTime, ymin, ymax), transform.position.z);
				}
			}
		}
		#endif
	}
	
	void FireLaser()
	{
		switch (playerLaser)
		{
			case "1laser":
				GameObject beam = Instantiate(playerLaserColor, transform.position + offset, Quaternion.identity) as GameObject;
				beam.GetComponent<Rigidbody2D>().velocity = new Vector3 (0, laserSpeed, 0);
				break;
			case "2laser":
				GameObject beam21 = Instantiate(playerLaserColor, transform.position + new Vector3(-.1f, 0.5f, 0f), Quaternion.identity) as GameObject;
				GameObject beam22 = Instantiate(playerLaserColor, transform.position + new Vector3(.1f, 0.5f, 0f), Quaternion.identity) as GameObject;
				beam21.GetComponent<Rigidbody2D>().velocity = new Vector3 (0, laserSpeed, 0);
				beam22.GetComponent<Rigidbody2D>().velocity = new Vector3 (0, laserSpeed, 0);
				break;
			case "3laser":
				GameObject beam31 = Instantiate(playerLaserColor, transform.position + offset, Quaternion.identity) as GameObject;
				GameObject beam32 = Instantiate(playerLaserColor, transform.position + new Vector3(-.15f, 0.5f, 0f), Quaternion.Euler(0, 0, 10)) as GameObject;
				GameObject beam33 = Instantiate(playerLaserColor, transform.position + new Vector3(.15f, 0.5f, 0f), Quaternion.Euler(0, 0, -10)) as GameObject;
				beam31.GetComponent<Rigidbody2D>().velocity = new Vector3 (0, laserSpeed, 0);
				beam32.GetComponent<Rigidbody2D>().velocity = new Vector3 (-1, laserSpeed, 0);
				beam33.GetComponent<Rigidbody2D>().velocity = new Vector3 (1, laserSpeed, 0);
				break;
			case "5laser":
				GameObject beam51 = Instantiate(playerLaserColor, transform.position + offset, Quaternion.identity) as GameObject;
				GameObject beam52 = Instantiate(playerLaserColor, transform.position + new Vector3(-.15f, 0.5f, 0f), Quaternion.Euler(0, 0, 10)) as GameObject;
				GameObject beam53 = Instantiate(playerLaserColor, transform.position + new Vector3(.15f, 0.5f, 0f), Quaternion.Euler(0, 0, -10)) as GameObject;
				GameObject beam54 = Instantiate(playerLaserColor, transform.position + new Vector3(-.30f, 0.5f, 0f), Quaternion.Euler(0, 0, 20)) as GameObject;
				GameObject beam55 = Instantiate(playerLaserColor, transform.position + new Vector3(.30f, 0.5f, 0f), Quaternion.Euler(0, 0, -20)) as GameObject;
				beam51.GetComponent<Rigidbody2D>().velocity = new Vector3 (0, laserSpeed, 0);
				beam52.GetComponent<Rigidbody2D>().velocity = new Vector3 (-1, laserSpeed, 0);
				beam53.GetComponent<Rigidbody2D>().velocity = new Vector3 (1, laserSpeed, 0);
				beam54.GetComponent<Rigidbody2D>().velocity = new Vector3 (-2, laserSpeed, 0);
				beam55.GetComponent<Rigidbody2D>().velocity = new Vector3 (2, laserSpeed, 0);
				break;
		}
	}
	
	public void setPlayerShip(string shipName)
	{
		health = startHealth;
		SetHealthBar();
		
		switch (shipName)
		{
			case "1blue":
				shipSprite.sprite = blue1;
				playerLaserColor = laserBlue;
				break;
			case "1green":
				shipSprite.sprite = green1;
				playerLaserColor = laserGreen;
				break;
			case "1orange":
				shipSprite.sprite = orange1;
				playerLaserColor = laserOrange;
				break;
			case "1red":
				shipSprite.sprite = red1;
				playerLaserColor = laserRed;
				break;
			case "2blue":
				shipSprite.sprite = blue2;
				playerLaserColor = laserBlue;
				break;
			case "2green":
				shipSprite.sprite = green2;
				playerLaserColor = laserGreen;
				break;
			case "2orange":
				shipSprite.sprite = orange2;
				playerLaserColor = laserOrange;
				break;
			case "2red":
				shipSprite.sprite = red2;
				playerLaserColor = laserRed;
				break;
			case "3blue":
				shipSprite.sprite = blue3;
				playerLaserColor = laserBlue;
				break;
			case "3green":
				shipSprite.sprite = green3;
				playerLaserColor = laserGreen;
				break;
			case "3orange":
				shipSprite.sprite = orange3;
				playerLaserColor = laserOrange;
				break;
			case "3red":
				shipSprite.sprite = red3;
				playerLaserColor = laserRed;
				break;
		}
		
		PolygonCollider2D body = this.gameObject.AddComponent<PolygonCollider2D>();
		body.isTrigger = true;
		
	}
	
	void setPlayerLaser(string weapon)
	{
		playerLaser = weapon;
	}
	
	public void UpgradeLaser()
	{
		if(playerLaser == "1laser")
		{
			playerLaser = "2laser";
		}
		else if(playerLaser == "2laser")
		{
			playerLaser = "3laser";
		}
		else if(playerLaser == "3laser")
		{
			playerLaser = "5laser";
		}
	}
	
	public void HealthIncrease()
	{
		health += 2;
		SetHealthBar();
	}
	
	void OnTriggerEnter2D (Collider2D coll)
	{
		if(coll.gameObject.tag == "Easy" || coll.gameObject.tag == "Medium" || coll.gameObject.tag == "Hard")
		{
			enemyController = coll.gameObject.GetComponent<EnemyController>();
			bool isDead = enemyController.IsDead();
			
			if (!isDead)
			{
				enemyController.KillEnemy();
				KillPlayer();
			}
			else
			{
				return;
			}
		}
		else if (coll.gameObject.tag == "BrownMeteor"  || coll.gameObject.tag == "GreyMeteor")
		{
			KillPlayer();
		}
	}
	
	public void ActivateShields()
	{
		
		if(!shieldsUp)
		{
			playerShield.SetActive(true);
			shipShieldController = GameObject.FindObjectOfType<ShipShieldController>();
			shieldsUp = true;
			shipShieldController.Activate();
		}
		else
		{
			shipShieldController.Refresh();
		}
	}
	
	public void DeactivateShields()
	{
		shieldsUp = false;
		playerShield.SetActive(false);
	}
	
	public void DoDamage(int damage)
	{
		health -= damage;
		SetHealthBar();
		scoreManager.ResetKillStreak();
		Handheld.Vibrate();
		
		if (health <= 0)
		{
			KillPlayer();
		}
		
	}
	
	void KillPlayer()
	{
		if(lives <= 0)
		{
			Handheld.Vibrate();
			CancelInvoke();
			Destroy(gameObject);
			
			scoreManager.SetCurrentScore();
			GameScene gameScene = GameObject.FindObjectOfType<GameScene>();
			gameScene.GoToResults("You LoSE!");
		}
		else
		{
			if(shieldsUp)
			{
				DeactivateShields();
			}
			lives--;
			scoreManager.SetLivesLeftText(lives);
			scoreManager.SetScoreText(-10000);
			Handheld.Vibrate();
			CancelInvoke();
			this.gameObject.SetActive(false);
			ResetPlayer();
		}
	}
	
	void ResetPlayer()
	{
		transform.position = new Vector3(0, -4, -2);
		health = startHealth;
		SetHealthBar();
		this.gameObject.SetActive(true);
		setPlayerLaser("1laser");
		InvokeRepeating("FireLaser", 0.5f, weaponRepeatRate);
	}
	
	public void Player1Up()
	{
		lives++;
		scoreManager.SetLivesLeftText(lives);
	}
	
	void SetHealthBar()
	{
		playerHealthSlider.value = health;
		
		if(health <= playerHealthSlider.maxValue/2 && health >= playerHealthSlider.maxValue/4)
		{
			playerHealthFill.color = Color.yellow;
		}
		else if(health < playerHealthSlider.maxValue/4)
		{
			playerHealthFill.color = Color.red;
		}
		else
		{
			playerHealthFill.color = Color.green;
		}
	}
}
