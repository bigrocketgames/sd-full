using UnityEngine;
using System.Collections;

public class MainMenuLaser : MonoBehaviour {

	private ParticleSystem particles;
	private Rigidbody2D rigidBody;
	private LevelManager levelManager;

	// Use this for initialization
	void Start () {
		particles = GameObject.FindObjectOfType<ParticleSystem>();
		levelManager = GameObject.FindObjectOfType<LevelManager>();
		rigidBody = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void OnCollisionEnter2D (Collision2D coll)
	{
		Destroy(gameObject);
		Vector3 particleLocale = coll.transform.position;
		particles.transform.position = particleLocale;
		particles.Play();
		Destroy(coll.gameObject);
		
		switch(coll.gameObject.name)
		{
		case "OptionsHitBox":
			GameObject buttonClicked = GameObject.FindGameObjectWithTag("OptionsTag");
			Destroy(buttonClicked.gameObject);
			levelManager.LoadLevel("Options");
			break;
		case "PlayHitBox":
			levelManager.LoadLevel("ShipChoice");
			break;
		case "ExitHitBox":
			levelManager.QuitRequest();
			break;
		case "CreditsHitBox":
			levelManager.LoadLevel("Credits");
			break;
		}	
	}
}
