using UnityEngine;
using System.Collections;

public class PlayerLaser : MonoBehaviour {
	
	private EnemyController enemyController;
	private BossController bossController;
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
			bool isDead = enemyController.IsDead();
			
			if (!isDead)
			{
				Destroy(gameObject);
				enemyController.DoDamage(laserDamage);
			}
			else
			{
				return;
			}
		}
		else if (coll.gameObject.tag == "Boss1" || coll.gameObject.tag == "Boss2" || coll.gameObject.tag == "Boss3" || coll.gameObject.tag == "Boss4" || coll.gameObject.tag == "Boss5")
		{
			bossController = coll.gameObject.GetComponent<BossController>();
			bool isDead = bossController.IsDead();
			
			if(!isDead)
			{
				Destroy(gameObject);
				bossController.DoDamage(laserDamage/2);
			}
			else
			{
				return;
			}
		}
	}
	
	//Determines damage to inflict on the enemies with my lasers
	public void FigureDamage(int diff, int wave)
	{
		laserDamage = (5 * difficulty) + wave;
	}

}
