using UnityEngine;
using System.Collections;

public class CreditMenu : MonoBehaviour {
	
	private SFXManager sfxManager;
	private LevelManager levelManager;
	
	// Use this for initialization
	void Start () {
		sfxManager = FindObjectOfType<SFXManager>();
		levelManager = FindObjectOfType<LevelManager>();
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(Input.GetKeyUp(KeyCode.Escape))
		{
			levelManager.LoadLevel("Main Menu");
		}
	}
	
	public void playSFX()
	{
		sfxManager.PlayButtonPress();
	}
}
