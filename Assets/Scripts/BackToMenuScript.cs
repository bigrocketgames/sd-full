using UnityEngine;
using System.Collections;

public class BackToMenuScript : MonoBehaviour {
	
	private LevelManager levelManager;
	private SFXManager sfxManager;
	
	// Use this for initialization
	void Start () {
		levelManager = FindObjectOfType<LevelManager>();
		sfxManager = FindObjectOfType<SFXManager>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnMouseUp()
	{
		//AutoInitScript.I.indexToLoad = Application.loadedLevel-1;
		sfxManager.PlayButtonPress();
		
		if(Application.loadedLevel == 4)
		{	
			levelManager.LoadLevel("Main Menu");
		}
		else
		{
			Application.LoadLevel (Application.loadedLevel-1);
		}
	}

}
