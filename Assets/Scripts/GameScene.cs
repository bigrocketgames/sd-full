using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.Analytics;

public class GameScene : MonoBehaviour {
	
	private int wave = 1;
	private PlayerController playerController;
	private EnemySpawner enemySpawner;
	private MusicManager musicManager;
	private ScoreManager scoreManager;
	private SFXManager sfxManager;
	private int difficulty;
	private AudioClip currentMusic;
	private int musicChoiceArrayNumber;
	private float startTime;
	
	public AudioClip[] gameMusicArray;
	public AudioClip bossMusic;
	public GameObject pauseButton;
	public GameObject pauseMenu;
	public GameObject musicSlider;
	public GameObject sfxSlider;
	public Slider musicVolumeSlider;
	public Slider sfxVolumeSlider;
	
	// Use this for initialization
	void Start () {
		pauseMenu.SetActive(false);
		musicSlider.SetActive(false);
		sfxSlider.SetActive(false);
		difficulty = PlayerPrefsManager.GetDifficulty();
		scoreManager = GameObject.FindObjectOfType<ScoreManager>();
		playerController = GameObject.FindObjectOfType<PlayerController>();
		enemySpawner = GameObject.FindObjectOfType<EnemySpawner>();
		musicManager = GameObject.FindObjectOfType<MusicManager>();
		sfxManager = GameObject.FindObjectOfType<SFXManager>();
		
		playerController.setPlayerShip(PlayerPrefsManager.GetPlayerShip());
		enemySpawner.EnemyNumber(wave);
		StartMusic();
	}
	
	// Update is called once per frame
	void Update () {
		if(sfxVolumeSlider.IsActive())
		{
			musicManager.ChangeVolume (musicVolumeSlider.value);
			sfxManager.ChangeVolume (sfxVolumeSlider.value);
		}
		
		if (Time.time >= (startTime + currentMusic.length))
		{
			NextSong();
		}
	}
	
	void StartMusic()
	{
		musicChoiceArrayNumber = Random.Range(0,5);
		musicManager.PlayGameMusic(gameMusicArray[musicChoiceArrayNumber],false);
		currentMusic = gameMusicArray[musicChoiceArrayNumber];
		startTime = Time.time;
	}
	
	void NextSong()
	{
		if(musicChoiceArrayNumber + 1 == gameMusicArray.Length)
		{
			musicManager.PlayGameMusic(gameMusicArray[0], false);
			musicChoiceArrayNumber = 0;
			currentMusic = gameMusicArray[musicChoiceArrayNumber];
			startTime = Time.time;
		}
		else
		{
			musicChoiceArrayNumber++;
			musicManager.PlayGameMusic(gameMusicArray[musicChoiceArrayNumber], false);
			currentMusic = gameMusicArray[musicChoiceArrayNumber];
			startTime = Time.time;
		}
	}
	
	public int GetWave()
	{
		return wave;
	}
	
	public void NextWave()
	{
		if(difficulty == 1)
		{
			if(wave < 10)
			{
				wave ++;
				enemySpawner.EnemyNumber(wave);
			}
			else if (wave >= 10)
			{
				scoreManager.SetCurrentScore();
				Invoke("YouWin", 1.0f);
			}
		}
		else if(difficulty == 2)
		{
			if(wave < 30)
			{
				wave ++;
				enemySpawner.EnemyNumber(wave);
			}
			else if (wave >= 30)
			{
				scoreManager.SetCurrentScore();
				Invoke("YouWin", 1.0f);
			}
		}
		else if (difficulty == 3)
		{
			if(wave < 50)
			{
				wave ++;
				enemySpawner.EnemyNumber(wave);
			}
			else if (wave >= 50)
			{
				scoreManager.SetCurrentScore();
				Invoke("YouWin", 1.0f);
			}
		}
		
		if( (wave) % 5 == 0)
			{
				AnalyticTest();
			}
		
		if(wave == 10 || wave == 20 || wave == 30 || wave == 40 || wave == 50)
		{
			musicManager.PlayGameMusic(bossMusic, true);
		}
		else if(wave == 11 || wave == 21 || wave == 31 || wave == 41 || wave == 51)
		{
			NextSong();
		}
	}
	
	void YouWin()
	{
		GoToResults("You WIN!");
	}
	
	public void OnPause()
	{
		pauseButton.SetActive(false);
		pauseMenu.SetActive(true);
		musicSlider.SetActive(true);
		sfxSlider.SetActive(true);
		musicVolumeSlider.value = PlayerPrefsManager.GetMasterVolume();
		sfxVolumeSlider.value = PlayerPrefsManager.GetSFXVolume();
	}
	
	public void OnUnPause()
	{
		pauseMenu.SetActive(false);
		musicSlider.SetActive(false);
		sfxSlider.SetActive(false);
		pauseButton.SetActive(true);
		Time.timeScale = 1.0f;
	}
	
	public void SetMusicVolume()
	{
		PlayerPrefsManager.SetMasterVolume(musicVolumeSlider.value);
		PlayerPrefsManager.SetSFXVolume(sfxVolumeSlider.value);
	}
	
	public void GoToResults(string result)
	{
		PlayerPrefsManager.SetResult(result);
		LevelManager levelManager = GameObject.FindObjectOfType<LevelManager>();
		levelManager.LoadLevel("Results");
	}
	
	void AnalyticTest()
	{
		Analytics.CustomEvent("waveCompletion", new Dictionary<string, object>
		{
			{"wavePassed", "Wave: " + (wave-1).ToString()}
		});
	}
}
