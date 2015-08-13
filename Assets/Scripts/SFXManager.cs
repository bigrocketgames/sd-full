using UnityEngine;
using System.Collections;

public class SFXManager : MonoBehaviour {
	
	public AudioClip buttonPress;
	
	private AudioSource sfxSource;
	private float volume;
	
	void Awake()
	{
		DontDestroyOnLoad(gameObject);
	}
	
	// Use this for initialization
	void Start () {
		sfxSource = GetComponent<AudioSource>();
		
		if(PlayerPrefs.HasKey("sfx_volume"))
		{
			volume = PlayerPrefsManager.GetSFXVolume();
			sfxSource.volume = volume;
		}
		else
		{
			sfxSource.volume = 1.0f;
			PlayerPrefsManager.SetSFXVolume(1.0f);
		}
	}
	
	public void ChangeVolume(float volume)
	{
		sfxSource.volume = volume;
	}
			
	public void PlaySFX(AudioClip clip)
	{
		sfxSource.clip = clip;
		sfxSource.loop = false;
		sfxSource.Play ();
	}
	
	public void PlayButtonPress()
	{
		sfxSource.clip = buttonPress;
		sfxSource.loop = false;
		sfxSource.Play ();
	}
	
	public void Pause()
	{
		sfxSource.Pause();
	}
	
	public void UnPause()
	{
		sfxSource.UnPause();
	}
	
	public void StopSounds()
	{
		sfxSource.Stop();
	}
}
