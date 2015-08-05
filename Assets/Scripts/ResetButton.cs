using UnityEngine;
using System.Collections;

public class ResetButton : MonoBehaviour {
	
	private LevelManager levelManager;
	private GameScene gameScene;
	
	// Use this for initialization
	void Start () {
		levelManager = FindObjectOfType<LevelManager>();
		gameScene = FindObjectOfType<GameScene>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void OnMouseUp()
	{
		gameScene.SetMusicVolume();
		Time.timeScale = 1.0f;
		levelManager.LoadLevel("Game");
	}
}
