using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class OptionsMenu : MonoBehaviour {

	public Slider musicVolumeSlider;
	public LevelManager levelManager;
	public Slider sfxVolumeSlider;
	
	private MusicManager musicManager;
	private SFXManager sfxManager;
	
	// Use this for initialization
	void Start () {
		musicManager = GameObject.FindObjectOfType<MusicManager>();
		sfxManager = GameObject.FindObjectOfType<SFXManager>();
		
		musicVolumeSlider.value = PlayerPrefsManager.GetMasterVolume();
		sfxVolumeSlider.value = PlayerPrefsManager.GetSFXVolume();
	}
	
	// Update is called once per frame
	void Update () {
		musicManager.ChangeVolume (musicVolumeSlider.value);
		sfxManager.ChangeVolume (sfxVolumeSlider.value);
		
		if(Input.GetKeyUp(KeyCode.Escape))
		{
			levelManager.LoadLevel("Main Menu");
		}
	}
	
	public void SaveAndExit()
	{
		PlayerPrefsManager.SetMasterVolume(musicVolumeSlider.value);
		PlayerPrefsManager.SetSFXVolume(sfxVolumeSlider.value);
		sfxManager.PlayButtonPress();
		levelManager.LoadLevel("Main Menu");
	}
	
	public void SetDefaults()
	{
		sfxManager.PlayButtonPress();
		musicVolumeSlider.value = 0.30f;
		sfxVolumeSlider.value = 1.0f;
	}
}
