using UnityEngine;
using System.Collections;

public class SplashScreen : MonoBehaviour {

	public Animator logoanimator;
	
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	public void LogoAnimate()
	{
		logoanimator.SetTrigger("LogoMove");
	}
}
