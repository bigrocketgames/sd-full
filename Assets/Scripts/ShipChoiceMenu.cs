using UnityEngine;
using System.Collections;

public class ShipChoiceMenu : MonoBehaviour {
	
	private LevelManager levelManager;
	
	// Use this for initialization
	void Start () {
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
}
