using UnityEngine;
using System.Collections;


public class DiffMenu : MonoBehaviour {
	
	private SFXManager sfxManager;
	private LevelManager levelManager;
	
	void Awake ()
	{
		HZIncentivizedAd.AdDisplayListener listener = delegate(string adState, string adTag)
		{
			if(adState.Equals ("incentivized_result_complete"))
			{
				levelManager.LoadLevel("Game");
			}
//			if(adState.Equals ("incentivized_result_incomplete"))
//			{
//				levelManager.LoadLevel("Game");
//			}
//			if(adState.Equals ("hide"))
//			{
//				levelManager.LoadLevel("Game");
//			}
		};
		
		HZIncentivizedAd.setDisplayListener(listener);
	}
	void Start () {
		levelManager = GameObject.FindObjectOfType<LevelManager>();
		sfxManager = FindObjectOfType<SFXManager>();
	}
	
	void Update ()
	{
		if(Input.GetKeyUp(KeyCode.Escape))
		{
			levelManager.LoadLevel("ModeChoice");
		}
	}
	
	public void DifficultySelect (string Button)
	{
		if (Button == "Easy")
		{
			PlayerPrefsManager.SetDifficulty(1);
		}
		else if (Button == "Normal")
		{
			PlayerPrefsManager.SetDifficulty(2);
		}
		else if (Button == "Hard")
		{
			PlayerPrefsManager.SetDifficulty(3);
		}
		
		playSFX();
		PlayAd();
	}
	
	public void playSFX()
	{
		sfxManager.PlayButtonPress();
	}
	
	void PlayAd()
	{
		if (HZIncentivizedAd.isAvailable("reward"))
		{
			HZIncentivizedAd.show("reward");
		}
		else
		{
			if (HZVideoAd.isAvailable("reward"))
			{
				HZVideoAd.show("reward");
			}
			else
			{
				levelManager.LoadLevel("Game");
			}
		}
	}
	
	
}
