using UnityEngine;
using System.Collections;

public class StarbaseShieldController : MonoBehaviour {

	private int shieldHealth;
	private StarbaseController starbaseController;
	private bool shieldsUp = false;
	
	// Use this for initialization
	void Start () {
		
	}
	
	public void Activate()
	{
		if(!shieldsUp)
		{
			shieldHealth = 10;
			starbaseController = GameObject.FindObjectOfType<StarbaseController>();
			shieldsUp = true;
			Invoke("Deactivate", 15f);
			InvokeRepeating("ChangeColor",0f,0.1f);
		}
		else
		{
			print ("refreshing shield");
			CancelInvoke();
			shieldHealth = 10;
			starbaseController = GameObject.FindObjectOfType<StarbaseController>();
			shieldsUp = true;
			Invoke("Deactivate", 15f);
			InvokeRepeating("ChangeColor",0f,0.1f);
		}
		
	}
	
	void Deactivate()
	{	
		shieldsUp = false;
		CancelInvoke();
		starbaseController.DeactivateShields();
	}
	
	void OnTriggerEnter2D (Collider2D coll)
	{
		if(coll.gameObject.tag == "EnemyLaser")
		{
			Destroy(coll.gameObject);
			shieldHealth --;
			
			if(shieldHealth <= 0)
			{
				Deactivate();
			}
		}
		else if(coll.gameObject.tag == "BrownMeteor" || coll.gameObject.tag == "GreyMeteor")
		{
			shieldHealth -= 2;
			
			if(shieldHealth <= 0)
			{
				Deactivate();
			}
		}
		else if(coll.gameObject.tag == "BossLaser")
		{
			shieldHealth -= 2;
			Destroy(coll.gameObject);
			
			if(shieldHealth <= 0)
			{
				Deactivate();
			}
		}
	}
	
	void ChangeColor()
	{
		Color m_color = new Color(Random.Range(0.5f,1f),Random.Range(0.5f,1f),Random.Range(0.5f,1f),0.5f);
		foreach(var r in GetComponents<Renderer>())
		{
			r.material.color = m_color;
		}
	}
}
