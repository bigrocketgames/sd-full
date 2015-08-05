using UnityEngine;
using System.Collections;

public class EnemySpawner : MonoBehaviour {
	
	public GameObject[] enemies;
	
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
		enemyBuildCount = 0;
		enemyStartCount = (int)Mathf.Log(wave,2f)*4 + ((int)Mathf.Sqrt(wave) * 5);
		enemiesThisWaveCount = enemyStartCount;
		InvokeRepeating("EnemyCreation",0f,1.0f);
		scoreManager.SetEnemyWaveText(enemiesThisWaveCount);
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
