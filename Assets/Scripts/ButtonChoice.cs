using UnityEngine;
using System.Collections;

public class ButtonChoice : MonoBehaviour {
	
	public GameObject playHit;
	public GameObject play;
	public GameObject optionsHit;
	public GameObject options;
	public GameObject creditsHit;
	public GameObject credits;
	public GameObject exitHit;
	public GameObject exit;
	
	private MainMenuShip mainMenuShip;
	
	void Start ()
	{
		
		mainMenuShip = GameObject.FindObjectOfType<MainMenuShip>();
		play.SetActive(true);
		options.SetActive(true);
		credits.SetActive(true);
		exit.SetActive(true);
		playHit.SetActive(true);
		optionsHit.SetActive(true);
		creditsHit.SetActive(true);
		exitHit.SetActive(true);
	}
	
	void Update ()
	{
		if(Input.GetKeyUp(KeyCode.Escape))
		{
			Application.Quit();
		}
	}
	
	public void ButtonPress(string Button)
	{
		if(Button == "play")
		{
			play.SetActive(true);
			options.SetActive(false);
			credits.SetActive(false);
			exit.SetActive(false);
			playHit.SetActive(true);
			optionsHit.SetActive(false);
			creditsHit.SetActive(false);
			exitHit.SetActive(false);
			
			mainMenuShip.MoveShip(Button);
		}
		else if(Button == "options")
		{
			play.SetActive(false);
			options.SetActive(true);
			credits.SetActive(false);
			exit.SetActive(false);
			playHit.SetActive(false);
			optionsHit.SetActive(true);
			creditsHit.SetActive(false);
			exitHit.SetActive(false);
			
			mainMenuShip.MoveShip(Button);
		}
		else if(Button == "credits")
		{
			play.SetActive(false);
			options.SetActive(false);
			credits.SetActive(true);
			exit.SetActive(false);
			playHit.SetActive(false);
			optionsHit.SetActive(false);
			creditsHit.SetActive(true);
			exitHit.SetActive(false);
			
			mainMenuShip.MoveShip(Button);
		}
		else
		{
			play.SetActive(false);
			options.SetActive(false);
			credits.SetActive(false);
			exit.SetActive(true);
			playHit.SetActive(false);
			optionsHit.SetActive(false);
			creditsHit.SetActive(false);
			exitHit.SetActive(true);
			
			mainMenuShip.MoveShip(Button);
		}
	}
}
