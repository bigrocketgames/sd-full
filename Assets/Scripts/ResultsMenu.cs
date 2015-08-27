using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ResultsMenu : MonoBehaviour {

	public Text resultText;
	public Text currentScoreText;
	public Text highScoreText;
	public Text highScoreNumber;
	
	public GameObject newHighScore;
	public Text newHighScoreText;
	private ScoreManager scoreManager;
	private SFXManager sfxManager;
	private LevelManager levelManager;

	private int highScore;
	private int currentScore;
	private string mode;
	
	// Use this for initialization
	void Start () {
		HZBannerAd.show(HZBannerAd.POSITION_BOTTOM);
		
		mode = PlayerPrefsManager.GetGameMode();
		newHighScore.SetActive(false);
		scoreManager = FindObjectOfType<ScoreManager>();
		sfxManager = FindObjectOfType<SFXManager>();
		levelManager = FindObjectOfType<LevelManager>();
		resultText.text = PlayerPrefsManager.GetResult();
		CheckHighScore();
		SetScoreText();
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(Input.GetKeyUp(KeyCode.Escape))
		{
			levelManager.LoadLevel("Main Menu");
		}
	}
	
	void CheckHighScore()
	{
		if(mode == "Wave")
		{
			highScore = PlayerPrefsManager.GetWaveHighScore();
		}
		else if(mode == "Boss")
		{
			highScore = PlayerPrefsManager.GetBossHighScore();
		}
		else if(mode == "Endless")
		{
			highScore = PlayerPrefsManager.GetEndlessHighScore();
		}
		
		currentScore = PlayerPrefsManager.GetCurrentScore();
		
		if(currentScore > highScore)
		{
			if(mode == "Wave")
			{
				PlayerPrefsManager.SetWaveHighScore(currentScore);
			}
			else if(mode == "Boss")
			{
				PlayerPrefsManager.SetBossHighScore(currentScore);
			}
			else if(mode == "Endless")
			{
				PlayerPrefsManager.SetEndlessHighScore(currentScore);
			}
			
			highScore = currentScore;
			NewHighScore();
		}
	}
	
	void SetScoreText()
	{
		currentScoreText.text = "ScorE " + currentScore.ToString();
		if(mode == "Wave")
		{
			highScoreText.text = " WAvE HIGH ScorE";
		}
		else if(mode == "Boss")
		{
			highScoreText.text = "BOSS HIGH ScorE";
		}
		else if(mode == "Endless")
		{
			highScoreText.text = "ENdLESS HIGH ScorE";
		}
		highScoreNumber.text = highScore.ToString();
		
	}
	
	void NewHighScore()
	{
		newHighScore.SetActive(true);
		Invoke("ChangeGreen",0.1f);
	}
	
	void ChangeGreen()
	{
		newHighScoreText.color = Color.green;
		Invoke("ChangeRed",0.1f);
	}
	
	void ChangeRed()
	{
		newHighScoreText.color = Color.red;
		Invoke("ChangeBlue",0.1f);
	}
	
	void ChangeBlue()
	{
		newHighScoreText.color = Color.blue;
		Invoke ("ChangeWhite",0.1f);
	}
	
	void ChangeBlack()
	{
		newHighScoreText.color = Color.black;
		Invoke("ChangeWhite",0.1f);
	}
	
	void ChangeWhite()
	{
		newHighScoreText.color = Color.white;
		Invoke("ChangeGreen",0.1f);
	}
	
	public void PlayButtonPressSFX()
	{
		sfxManager.PlayButtonPress();
	}
}
