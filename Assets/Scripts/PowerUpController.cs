using UnityEngine;
using System.Collections;

public class PowerUpController : MonoBehaviour {
	
	public AudioClip powerUpSound;
	
	private PlayerController playerController;
	private StarbaseController starbaseController;
	private SFXManager sfxManager;
	
	// Use this for initialization
	void Start () {
		playerController = FindObjectOfType<PlayerController>();
		starbaseController = FindObjectOfType<StarbaseController>();
		sfxManager = FindObjectOfType<SFXManager>();
		
		Rigidbody2D body = GetComponent<Rigidbody2D>();
		Invoke("KillPowerUp",4f);
		body.velocity = new Vector2(0f, -1f);
		
		if(this.tag == "1Up")
		{
			SpriteRenderer playerRenderer = playerController.GetComponent<SpriteRenderer>();
			SpriteRenderer thisRenderer = GetComponent<SpriteRenderer>();
			PolygonCollider2D shipCollider = GetComponent<PolygonCollider2D>();
			
			shipCollider.isTrigger = true;
			thisRenderer.sprite = playerRenderer.sprite;
			this.transform.localScale = new Vector3(0.4f, 0.4f, 0.4f);
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void KillPowerUp()
	{
		Destroy(gameObject);
	}
	
	void OnTriggerEnter2D(Collider2D coll)
	{
		if(coll.gameObject.tag == "Player")
		{
			sfxManager.PlaySFX(powerUpSound);
			
			if(this.tag == "ShipLaserUp")
			{	
				playerController.UpgradeLaser();
				Destroy(gameObject);
			}
			else if(this.tag == "ShipHealthUp")
			{
				playerController.HealthIncrease();
				Destroy(gameObject);
			}
			else if(this.tag == "ShipShieldsUp")
			{
				playerController.ActivateShields();
				Destroy(gameObject);
			}
			else if(this.tag == "1Up")
			{
				playerController.Player1Up();
				Destroy(gameObject);
			}
			else if(this.tag == "StarbaseLaserUp")
			{
				print ("need to make starbase, and starbase guns");
				starbaseController.StarbaseWeaponUpgrade();
				Destroy(gameObject);
			}	
			else if(this.tag == "StarbaseHealthUp")
			{
				starbaseController.StarbaseHealthIncrease();
				Destroy(gameObject);
			}
			else if(this.tag == "StarbaseShieldsUp")
			{
				starbaseController.ActivateShields();
				Destroy(gameObject);
			}
		}
	}
}
