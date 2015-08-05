using UnityEngine;
using System.Collections;

public class QuitButton : MonoBehaviour {
	
	private LevelManager levelManager;
	
	// Use this for initialization
	void Start () {
		levelManager = GameObject.FindObjectOfType<LevelManager>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void OnMouseUp()
	{
		Time.timeScale = 1.0f;
		levelManager.LoadLevel("Main Menu");
	}
}
