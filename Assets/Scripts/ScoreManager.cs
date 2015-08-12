using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour {
	
	public Text waveText;
	public Text livesText;
	public Text scoreText;
	
	private int score;
	private int points;
	private int killStreak;
	private GameScene gameScene;
	
	// Use this for initialization
	void Start () {
		killStreak = 0;
		SetScoreText();
		gameScene = FindObjectOfType<GameScene>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	public void SetEnemyWaveText(int enemies)
	{
		int wave = gameScene.GetWave();
		waveText.text = "Wave " + wave + " enemies: " + enemies;
	}
	
	public void SetLivesLeftText(int lives)
	{
		livesText.text = "Lives Left: " + (lives);
	}
	
	public void SetScoreText(string shipDiff)
	{
		killStreak++;
		int wave = gameScene.GetWave();
		points = 10 + (wave * 10) + (killStreak * 10);
		
		if(shipDiff == "Medium")
		{
			points *= 2;
		}
		else if(shipDiff == "Hard")
		{
			points *= 3;
		}
		else if(shipDiff == "Boss1" || shipDiff == "Boss2" || shipDiff == "Boss3" || shipDiff == "Boss4" || shipDiff == "Boss5")
		{
			points *= 100;
		}
		
		score += points;
		scoreText.text = "Score: " + score;
	}
	
	public void SetScoreText(int point)
	{
		score += points;
		scoreText.text = "Score: " + score;
	}
	
	public void SetScoreText()
	{
		scoreText.text = "Score: " + score;
	}
	
	public void SetResult(string result)
	{
		PlayerPrefsManager.SetResult(result);
	}
	
	public void SetCurrentScore()
	{
		PlayerPrefsManager.SetCurrentScore(score);
	}
	
	public void ResetKillStreak()
	{
		killStreak = 0;
	}
}
