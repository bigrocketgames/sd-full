using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class DiffMenu : MonoBehaviour {
	
	public GameObject adQuestionPanel;
	public GameObject yesButton;
	public GameObject noButton;
	public GameObject okButton;
	public GameObject ok1Button;
	public GameObject fightButton;
	public GameObject BackButton;
	public Text adQuestionText;
	
	private SFXManager sfxManager;
	private LevelManager levelManager;
	private int rewardNumber;
	private string weaponUpgradeText;
	
	void Start () {
		adQuestionPanel.SetActive(false);
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
		AskForAd();
	}
	
	public void playSFX()
	{
		sfxManager.PlayButtonPress();
	}
	
	void AskForAd()
	{
		adQuestionPanel.SetActive(true);
		ok1Button.SetActive(false);
		okButton.SetActive(false);
		fightButton.SetActive(false);
	}
	
	public void AnswerNo()
	{
		levelManager.LoadLevel("Game");
	}
	
	public void PlayAd()
	{
		if (HZIncentivizedAd.isAvailable("default"))
		{
			HZIncentivizedAd.show("default");
		}
		else
		{
			HeyzapAdsAndroid.showDebugLogs();
			adQuestionText.text = "We encountered an error, but no worries, you still get an upgrade.  Click OK to receive your free upgrade.";
			okButton.SetActive(true);
			yesButton.SetActive(false);
			noButton.SetActive(false);
		}
		
		HZIncentivizedAd.AdDisplayListener listener = delegate(string adState, string adTag)
		{
			if(adState.Equals ("incentivized_result_complete"))
			{
				GetReward();
				ShowReward();
			}
			if(adState.Equals ("failed"))
			{
//				levelManager.LoadLevel("Game");
			}
			if(adState.Equals ("fetch_failed"))
			{
//				levelManager.LoadLevel("Game");
			}
		};
		
		HZIncentivizedAd.setDisplayListener(listener);
	}
	
	
	public void PrepareBattle()
	{
		ok1Button.SetActive(false);
		yesButton.SetActive(false);
		noButton.SetActive(false);
		fightButton.SetActive(true);
		adQuestionText.text = "Press Fight! when you are ready to enter battle.";
	}
	
	public void ShowReward()
	{	
		BackButton.SetActive(false);
		GetReward();
		adQuestionText.text = "Your reward is the upgrade to: " + weaponUpgradeText;
		ok1Button.SetActive(true);
		okButton.SetActive(false);
	}
	
	void GetReward()
	{
		rewardNumber = Random.Range(1,101);
		
		if(rewardNumber >=75)
		{
			weaponUpgradeText = "DoubleLasers";
		}
		else if(rewardNumber <=95)
		{
			weaponUpgradeText = "TripleLasers";
		}
		else
		{
			weaponUpgradeText = "FiveShooter";
		}
	}
}
