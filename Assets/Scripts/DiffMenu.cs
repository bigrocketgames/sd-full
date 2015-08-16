using UnityEngine;
using System.Collections;

public class DiffMenu : MonoBehaviour {
	
	private SFXManager sfxManager;
	private LevelManager levelManager;
	
	// Use this for initialization
	void Start () {
		levelManager = GameObject.FindObjectOfType<LevelManager>();
		sfxManager = FindObjectOfType<SFXManager>();
	}
	
	void Update ()
	{
		if(Input.GetKeyUp(KeyCode.Escape))
		{
			levelManager.LoadLevel("ModeChoice");
		}
	}
	
	public void DifficultySelect (string Button)
	{
		if (Button == "Easy")
		{
			PlayerPrefsManager.SetDifficulty(1);
		}
		else if (Button == "Normal")
		{
			PlayerPrefsManager.SetDifficulty(2);
		}
		else if (Button == "Hard")
		{
			PlayerPrefsManager.SetDifficulty(3);
		}
		
		playSFX();
		levelManager.LoadLevel("Game");
	}
	
	public void playSFX()
	{
		sfxManager.PlayButtonPress();
	}
}
