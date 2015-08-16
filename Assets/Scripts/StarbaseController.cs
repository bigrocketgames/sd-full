using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class StarbaseController : MonoBehaviour {
	
	public Slider healthSlider;
	public Image starbaseHealthfill;
	public GameObject starbaseShield;
	public GameObject starbaseGun11;
	public GameObject starbaseGun12;
	public GameObject starbaseGun13;
	public GameObject starbaseGun21;
	public GameObject starbaseGun22;
	public GameObject starbaseGun23;
	
	public bool gun11Missing = true;
	public bool gun12Missing = true;
	public bool gun13Missing = true;
	public bool gun21Missing = true;
	public bool gun22Missing = true;
	public bool gun23Missing = true;
	
	private GameObject gun11;
	private GameObject gun12;
	private GameObject gun13;
	private GameObject gun21;
	private GameObject gun22;
	private GameObject gun23;
	private int startHealth;
	private int health;
	private int difficulty;
	
	private ScoreManager scoreManager;
	
	// Use this for initialization
	void Start () {
		starbaseShield.SetActive(false);
		scoreManager = GameObject.FindObjectOfType<ScoreManager>();
		difficulty = PlayerPrefsManager.GetDifficulty();
		startHealth = 1000 * difficulty;
		healthSlider.maxValue = startHealth;
		health = startHealth;
		healthSlider.value = health;
		
		Vector3 HealthBarOffset = new Vector3 (0f, 1.85f, -2);
		healthSlider.transform.position = transform.position + HealthBarOffset;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	public void StarbaseDamage(int damage)
	{
		health -= (damage * difficulty);
		
		if(health > startHealth)
		{
			healthSlider.value = startHealth;
		}
		else
		{
			healthSlider.value = health;
		}
		
		if(health <= healthSlider.maxValue/2 && health >= healthSlider.maxValue/4)
		{
			starbaseHealthfill.color = Color.yellow;
		}
		else if(health < healthSlider.maxValue/4)
		{
			starbaseHealthfill.color = Color.red;
		}
		else
		{
			starbaseHealthfill.color = Color.green;
		}
		
		if(health <= 0)
		{
			scoreManager.SetCurrentScore();
			GameScene gameScene = GameObject.FindObjectOfType<GameScene>();
			gameScene.GoToResults("You Lose!");
		}
	}
	
	public void StarbaseHealthIncrease()
	{
		health += 10;
		if(health > startHealth)
		{
			healthSlider.value = startHealth;
		}
		else
		{
			healthSlider.value = health;
		}
		
		if(health <= healthSlider.maxValue/2 && health >= healthSlider.maxValue/4)
		{
			starbaseHealthfill.color = Color.yellow;
		}
		else if(health < healthSlider.maxValue/4)
		{
			starbaseHealthfill.color = Color.red;
		}
		else
		{
			starbaseHealthfill.color = Color.green;
		}
	}
	
	public void ActivateShields()
	{
		starbaseShield.SetActive(true);
		StarbaseShieldController starbaseShieldController = GameObject.FindObjectOfType<StarbaseShieldController>();
		
		starbaseShieldController.Activate();
	}
	
	public void DeactivateShields()
	{
		starbaseShield.SetActive(false);
	}
	
	public void StarbaseWeaponUpgrade()
	{
	
		if(gun11Missing)
		{
			gun11 = Instantiate(starbaseGun11, starbaseGun11.transform.position,Quaternion.identity) as GameObject;
			gun11Missing = false;
		}
		else
		{
			if(gun12Missing)
			{
				gun12 = Instantiate(starbaseGun12, starbaseGun12.transform.position,Quaternion.identity) as GameObject;
				gun12Missing = false;
			}
			else
			{
				if(gun13Missing)
				{
					gun13 = Instantiate(starbaseGun13, starbaseGun13.transform.position,Quaternion.identity) as GameObject;
					gun13Missing = false;
				}
				else
				{
					if(gun21Missing)
					{
						gun21 = Instantiate(starbaseGun21, starbaseGun21.transform.position,Quaternion.identity) as GameObject;
						gun21Missing = false;
					}
					else
					{
						if(gun22Missing)
						{
							gun22 = Instantiate(starbaseGun22, starbaseGun22.transform.position,Quaternion.identity) as GameObject;
							gun22Missing = false;
						}
						else
						{
							if(gun23Missing)
							{
								gun23 = Instantiate(starbaseGun23, starbaseGun23.transform.position,Quaternion.identity) as GameObject;
								gun23Missing = false;
							}
						}
					}
				}
			}
		}
	}

}
