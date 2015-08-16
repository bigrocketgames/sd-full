using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ShipShieldController : MonoBehaviour {
	
	private PlayerController playerController;
	private int shieldHealth;
	
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	public void Activate()
	{
		shieldHealth = 5;
		playerController = GameObject.FindObjectOfType<PlayerController>();
		InvokeRepeating("ChangeColor",0f,0.1f);
		Invoke("Deactivate", 7.0f);
	}
	
	void ChangeColor()
	{
		Color m_color = new Color(Random.Range(0.5f,1f),Random.Range(0.5f,1f),Random.Range(0.5f,1f),1f);
		foreach(var r in GetComponents<Renderer>())
		{
			r.material.color = m_color;
		}
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
	
	public void Refresh()
	{
		CancelInvoke();
		InvokeRepeating("ChangeColor",0f,0.1f);
		Invoke("Deactivate", 7.0f);
		shieldHealth = 5;
	}
	public void Deactivate()
	{
		CancelInvoke();
		playerController.DeactivateShields();
	}
}
