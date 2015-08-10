using UnityEngine;
using System.Collections;

public class EnemySpawner : MonoBehaviour {
	
	public GameObject[] enemies;
	public GameObject[] bosses;
	
	private int enemyStartCount;
	private int enemiesThisWaveCount;
	private int enemyBuildCount;
	private int difficutlty;
	private int enemyShipArraySelect;
	private ScoreManager scoreManager;
	
	// Use this for initialization
	void Start () {
		difficutlty = PlayerPrefsManager.GetDifficulty();
		scoreManager = GameObject.FindObjectOfType<ScoreManager>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	public void EnemyNumber(int wave)
	{	
		if (wave % 10 == 0)
		{
			BossCreation(wave);
			scoreManager.SetEnemyWaveText(1);
		}
		else
		{
			enemyBuildCount = 0;
			enemyStartCount = (int)Mathf.Log(wave,2f)*2 + ((int)Mathf.Sqrt(wave) * 5);
			enemiesThisWaveCount = enemyStartCount;
			InvokeRepeating("EnemyCreation",0f,1.0f);
			scoreManager.SetEnemyWaveText(enemiesThisWaveCount);
		}
			
	}
	
	void BossCreation(int wave)
	{
		GameObject enemy = Instantiate(bosses[(wave/10)-1],new Vector3(0,7.5f,-2),Quaternion.identity) as GameObject;
	}
	
	public void EnemyCreation()
	{
		if(enemyBuildCount < enemyStartCount)
		{
			float randomX = Random.Range(-2.7f,2.71f);
			float randomY = Random.Range(5.5f, 8f);
			if(difficutlty == 1)
			{
				enemyShipArraySelect = Random.Range(0,enemies.Length - 4);
			}
			else
			{
				enemyShipArraySelect = Random.Range(0,enemies.Length);
			}
			
			GameObject enemy = Instantiate(enemies[enemyShipArraySelect],new Vector3(randomX,randomY,-2),Quaternion.identity) as GameObject;
			enemyBuildCount++;
		}
		else
		{
			CancelInvoke();
		}
			
	}
	
	public void EnemyDead()
	{
		enemiesThisWaveCount --;
		scoreManager.SetEnemyWaveText(enemiesThisWaveCount);
		
		if(enemiesThisWaveCount <= 0)
		{
			CallNextWave();
		}
	}
	
	void CallNextWave()
	{
		GameScene gameScene = GameObject.FindObjectOfType<GameScene>();
		gameScene.NextWave();
	}
}
