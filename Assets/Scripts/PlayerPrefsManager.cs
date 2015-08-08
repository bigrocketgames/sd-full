using UnityEngine;
using System.Collections;

public class PlayerPrefsManager : MonoBehaviour {

	const string MASTER_VOLUME_KEY = "master_volume";
	const string SFX_VOLUME_KEY = "sfx_volume";
	const string DIFFICULTY_KEY = "difficulty";
	const string LEVEL_KEY = "level_unlocked_";
	const string HIGHSCORE_KEY = "highscore";
	const string CURRENT_SCORE_KEY = "current_score";
	const string PLAYER_SHIP_KEY = "ship_choice";
	const string MODE_CHOICE_KEY = "mode_choice";
	const string RESULT_KEY = "result";
	
	public static void SetResult(string result)
	{
		PlayerPrefs.SetString(RESULT_KEY,result);
	}
	
	public static string GetResult()
	{
		return PlayerPrefs.GetString(RESULT_KEY);
	}
	
	public static void SetMasterVolume(float volume)
	{
		if(volume >= 0f && volume <= 1f)
		{
			PlayerPrefs.SetFloat(MASTER_VOLUME_KEY, volume);
		}
		else
		{
			Debug.LogError("Master Volume out of range");
		}
	}
	
	public static float GetMasterVolume()
	{
		return PlayerPrefs.GetFloat(MASTER_VOLUME_KEY);
	}
	
	public static void SetSFXVolume(float volume)
	{
		if(volume >= 0f && volume <= 1f)
		{
			PlayerPrefs.SetFloat(SFX_VOLUME_KEY, volume);
		}
		else
		{
			Debug.LogError("Master Volume out of range");
		}
	}
	
	public static float GetSFXVolume()
	{
		return PlayerPrefs.GetFloat(SFX_VOLUME_KEY);
	}
	
	//TODO May use this as an unlock features for purchases of currency to buy boosts - or to unlock difficulty modes
//	public static void UnlockLevel (int level)
//	{
//		if(level <= Application.levelCount - 1)
//		{
//			PlayerPrefs.SetInt(LEVEL_KEY + level.ToString(), 1); //use 1 for true
//		}
//		else
//		{
//			Debug.LogError("Level does not exist to be able to unlock");
//		}
//	}
//	
//	public static bool IsLevelUnlocked(int level)
//	{
//		int levelValue = PlayerPrefs.GetInt (LEVEL_KEY + level.ToString());
//		bool isLevelUnlocked = (levelValue == 1);
//		
//		if(level <= Application.levelCount - 1)
//		{
//			return isLevelUnlocked;
//		}
//		else
//		{
//			Debug.LogError ("Level does not exist in Build Order to be able to unlock");
//			return false;
//		}
//	}
	
	
	public static void SetDifficulty (int difficulty)
	{
		PlayerPrefs.SetInt(DIFFICULTY_KEY, difficulty);
	}
	
	public static int GetDifficulty()
	{
		return PlayerPrefs.GetInt(DIFFICULTY_KEY);
	}
	
	public static void SetCurrentScore (int score)
	{
		PlayerPrefs.SetInt(CURRENT_SCORE_KEY, score);
	}
	
	public static int GetCurrentScore()
	{
		return PlayerPrefs.GetInt(CURRENT_SCORE_KEY);
	}
	
	public static int GetHighScore()
	{
		return PlayerPrefs.GetInt(HIGHSCORE_KEY);	
	}
	
	public static void SetHighScore(int score)
	{
		PlayerPrefs.SetInt(HIGHSCORE_KEY, score);
	}
	
	public static void SetPlayerShip(string shipName)
	{
		PlayerPrefs.SetString(PLAYER_SHIP_KEY, shipName);
	}
	
	public static string GetPlayerShip()
	{
		return PlayerPrefs.GetString(PLAYER_SHIP_KEY);
	}
	
	public static void SetGameMode(string modeChoice)
	{
		PlayerPrefs.SetString(MODE_CHOICE_KEY, modeChoice);
	}
	
	public static string GetGameMode()
	{
		return PlayerPrefs.GetString(MODE_CHOICE_KEY);
	}
}
