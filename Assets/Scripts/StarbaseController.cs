using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class StarbaseController : MonoBehaviour {
	
	public Slider healthSlider;
	public Image starbaseHealthfill;
	public GameObject starbaseShield;
	
	private int startHealth = 1000;
	private int health;
	private int level;
	private float weaponFireRate = 5.0f;
	private int difficulty;
	
	private ScoreManager scoreManager;
	
	// Use this for initialization
	void Start () {
		starbaseShield.SetActive(false);
		scoreManager = GameObject.FindObjectOfType<ScoreManager>();
		difficulty = PlayerPrefsManager.GetDifficulty();
		healthSlider.maxValue = startHealth;
		health = startHealth;
		healthSlider.value = health;
		level = 1;
		
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
		if(level == 1)
		{
			//TODO - implement first gun
			level++;
		}
		else if(level == 2)
		{
			//TODO - add second gun
			level++;
		}
		else if(level == 3)
		{
			//TODO - add third gun
			level++;
		}
		else if(level == 4)
		{
			//TODO - upgarde first gun to two shots
			level++;
		}
		else if(level == 5)
		{
			//TODO - upgarde second gun to two shots
			level++;
		}
		else if(level == 6)
		{
			//TODO - upgarde third gun to two shots
			level++;
		}
		else
		{
			weaponFireRate -= 0.1f;
			//TODO - cancel and reinvoke the fire call with new weaponFireRate
		}
	}

}
