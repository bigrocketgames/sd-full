using UnityEngine;
using System.Collections;

public class ResumeButton : MonoBehaviour {

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
		gameScene.SetMusicVolume();
		gameScene.OnUnPause();
	}
}
