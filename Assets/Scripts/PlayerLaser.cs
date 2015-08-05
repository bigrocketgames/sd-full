using UnityEngine;
using System.Collections;

public class PlayerLaser : MonoBehaviour {
	
	private EnemyController enemyController;
	private int laserDamage;
	private int difficulty;
	private int wave;
	
	// Use this for initialization
	void Start () {
		difficulty = PlayerPrefsManager.GetDifficulty();
		GameScene gameScene = GameObject.FindObjectOfType<GameScene>();
		wave = gameScene.GetWave();
		
		FigureDamage(difficulty,wave);
	}
	
	void Update ()
	{
		
	}
	
	void OnTriggerEnter2D (Collider2D coll)
	{
		if(coll.gameObject.tag == "Easy" || coll.gameObject.tag == "Medium" || coll.gameObject.tag == "Hard")
		{
			enemyController = coll.gameObject.GetComponent<EnemyController>();
			enemyController.DoDamage(laserDamage);
			bool isDead = enemyController.IsDead();
			
			if (!isDead)
			{
				Destroy(gameObject);
			}
			else
			{
				return;
			}
		}
	}
	
	//TODO - create method to figure out damage to inflict on the enemies with my lasers
	public void FigureDamage(int diff, int wave)
	{
		laserDamage = (5 * difficulty) + wave;
	}

}
