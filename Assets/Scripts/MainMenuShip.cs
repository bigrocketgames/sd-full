using UnityEngine;
using System.Collections;
//using UnityEditor.AnimatedValues;

public class MainMenuShip : MonoBehaviour {
	
	public GameObject play;
	public GameObject options;
	public GameObject credits;
	public GameObject exit;
	public GameObject laser;
	public AudioClip playerLaserSound;
	
	private Animator animator;
	private float projectileSpeed = 5;
	private Vector3 offset = new Vector3(0,0.5f, 0);
	private SFXManager sfxManager;
	
	// Use this for initialization
	void Start () {
		animator = GetComponent<Animator>();
		sfxManager = GameObject.FindObjectOfType<SFXManager>();
	}
	
	public void MoveShip(string Button)
	{
		if(Button == "options")
		{	
			animator.SetTrigger("OptionsTrigger");
			StartCoroutine(FireLaserDelay());
		}
		else if (Button == "credits")
		{
			animator.SetTrigger("CreditsTrigger");
			StartCoroutine(FireLaserDelay());
		}
		else
		{
			FireLaser();
		}
		
	}
	
	void FireLaser()
	{
		GameObject beam = Instantiate(laser, transform.position+offset, Quaternion.identity) as GameObject;
		beam.GetComponent<Rigidbody2D>().velocity = new Vector3(0, projectileSpeed, 0);
		sfxManager.PlaySFX(playerLaserSound);
	}
	
	IEnumerator FireLaserDelay()
	{
		yield return new WaitForSeconds(0.62f);
		GameObject beam = Instantiate(laser, transform.position+offset, Quaternion.identity) as GameObject;
		beam.GetComponent<Rigidbody2D>().velocity = new Vector3(0, projectileSpeed, 0);
		sfxManager.PlaySFX(playerLaserSound);
	}
}
