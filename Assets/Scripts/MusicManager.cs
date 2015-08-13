using UnityEngine;
using System.Collections;

public class MusicManager : MonoBehaviour {

	public AudioClip[] levelMusicChangeArray;
	
	private AudioSource musicSource;
	private AudioClip previousClip;
	private AudioClip currentMusic;
	private float volume;
	
	void Awake()
	{
		DontDestroyOnLoad(gameObject);
	}
	
	void Start()
	{
		musicSource = GetComponent<AudioSource>();
		
		if(PlayerPrefs.HasKey("master_volume"))
		{
			volume = PlayerPrefsManager.GetMasterVolume();
			musicSource.volume = volume;
		}
		else
		{
			musicSource.volume = 0.3f;
			PlayerPrefsManager.SetMasterVolume(0.3f);
		}
	}
	
	void OnLevelWasLoaded(int level)
	{	
		currentMusic = musicSource.clip;
		AudioClip thisLevelMusic = levelMusicChangeArray[level];
				
		if(thisLevelMusic && (currentMusic != thisLevelMusic))
		{
			musicSource.clip = thisLevelMusic;
			musicSource.loop = true;
			musicSource.Play ();
			currentMusic = thisLevelMusic;
		}
		else
		{
			//I have commented out the stop line, so that the music doesn't stop upon moving to a scene without music, but rather continues to play the same song.
			//musicSource.Stop ();
		}
		
	}
	
	public void PlayGameMusic(AudioClip clip, bool loop)
	{
		musicSource.Stop();
		musicSource.clip = clip;
		musicSource.loop = loop;
		musicSource.Play();
	}
	
	public void ChangeVolume(float volume)
	{
		musicSource.volume = volume;
	}
	
	public void PauseMusic()
	{
		musicSource.Pause();
	}
	
	public void UnPauseMusic()
	{
		musicSource.UnPause();
	}
	
	public void StopMusic()
	{
		musicSource.Stop();
	}
}
