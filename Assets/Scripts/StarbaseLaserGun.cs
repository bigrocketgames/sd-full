using UnityEngine;
using System.Collections;

public class StarbaseLaserGun : MonoBehaviour {
	
	public GameObject[] starbaseLasers;
	
	private int health;
	private int difficulty;
	private float fireRate = 2.5f;
	private float laserVelocity = 14f;
	private Vector3 laserOffset1 = new Vector3(0.012f, 0.175f, 0f);
	private Vector3 laserOffset2 = new Vector3(0.163f, 0.175f, 0f);
	private Vector3 laserOffset3 = new Vector3(-0.134f, 0.175f, 0f);
	private StarbaseController starbaseController;
	
	// Use this for initialization
	void Start () {
		starbaseController = FindObjectOfType<StarbaseController>();
		health = 50;
		difficulty = PlayerPrefsManager.GetDifficulty();
		InvokeRepeating("FireLasers",0.1f,fireRate);
	}
	
	void FireLasers()
	{
		GameObject laser = starbaseLasers[Random.Range(0,starbaseLasers.Length-1)];
		
		if(this.tag == "StarbaseGun1")
		{
			GameObject beam1 = Instantiate(laser,this.transform.position + laserOffset1,Quaternion.identity) as GameObject;
			beam1.GetComponent<Rigidbody2D>().velocity = new Vector3 (0f, laserVelocity, 0f);
		}
		
		if(this.tag == "StarbaseGun2")
		{
			GameObject beam2 = Instantiate(laser,this.transform.position + laserOffset2,Quaternion.identity) as GameObject;
			GameObject beam3 = Instantiate(laser,this.transform.position + laserOffset3,Quaternion.identity) as GameObject;
			beam2.GetComponent<Rigidbody2D>().velocity = new Vector3 (0f, laserVelocity, 0f);
			beam3.GetComponent<Rigidbody2D>().velocity = new Vector3 (0f, laserVelocity, 0f);
		}
	}
	
	void OnTriggerEnter2D(Collider2D coll)
	{
		if(coll.tag != "Player" && coll.tag != "StarbaseShield")
		{
			health -= 1*difficulty;
			
			if(health <= 0)
			{
				GunDestroy(this.gameObject.name);
			}
		}
	}
	
	void GunDestroy(string name)
	{
		switch (name)
		{
			case "SBGun11(Clone)":
				Destroy(this.gameObject);
				starbaseController.gun11Missing = true;
				break;
			case "SBGun12(Clone)":
				Destroy(this.gameObject);
				starbaseController.gun12Missing = true;
				break;
			case "SBGun13(Clone)":
				Destroy(this.gameObject);
				starbaseController.gun13Missing = true;
				break;
			case "SBGun21(Clone)":
				Destroy(this.gameObject);
				starbaseController.gun21Missing = true;
				break;
			case "SBGun22(Clone)":
				Destroy(this.gameObject);
				starbaseController.gun22Missing = true;
				break;
			case "SBGun23(Clone)":
				Destroy(this.gameObject);
				starbaseController.gun23Missing = true;
				break;
		}
	}
}
