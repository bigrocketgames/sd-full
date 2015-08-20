using UnityEngine;
using System.Collections;

public class SplashScreen : MonoBehaviour {

	public Animator logoAnimator;
	
	private Animator rocketAnimator;
	private float startCount;
	private bool isLaunching = false;
	
	// Use this for initialization
	void Start () {
		rocketAnimator = GetComponent<Animator>();
		startCount = Time.time;
	}
	
	// Update is called once per frame
	void Update () {
		if(!isLaunching)
		{
			if(startCount + Time.time >= 0.5f)
			{
				rocketAnimator.SetTrigger("isLaunching");
				isLaunching = true;
			}
		}
	}
	
	public void LogoAnimate()
	{
		logoAnimator.SetTrigger("LogoMove");
	}
}
