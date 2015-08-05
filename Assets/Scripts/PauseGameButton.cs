using UnityEngine;
using System.Collections;

public class PauseGameButton : MonoBehaviour {

	private GameScene gameScene;
	
	// Use this for initialization
	void Start () {
		gameScene = FindObjectOfType<GameScene>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void OnMouseUp()
	{
		Time.timeScale = 0.0f;
		gameScene.OnPause();
	}
}
