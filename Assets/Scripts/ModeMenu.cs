using UnityEngine;
using System.Collections;

public class ModeMenu : MonoBehaviour {

	private SFXManager sfxManager;
	private LevelManager levelManager;
	
	void Start ()
	{
		sfxManager = FindObjectOfType<SFXManager>();
		levelManager = FindObjectOfType<LevelManager>();
	}

	public void GameModeChoice(string Mode)
	{
		sfxManager.PlayButtonPress();
		PlayerPrefsManager.SetGameMode(Mode);
		levelManager.LoadLevel("DiffChoice");
	}
}
