using UnityEngine;
using System.Collections;

public class CreditMenu : MonoBehaviour {
	
	private SFXManager sfxManager;
	
	// Use this for initialization
	void Start () {
		sfxManager = FindObjectOfType<SFXManager>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	public void playSFX()
	{
		sfxManager.PlayButtonPress();
	}
}
