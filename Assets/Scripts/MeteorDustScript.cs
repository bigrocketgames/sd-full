using UnityEngine;
using System.Collections;

public class MeteorDustScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
		Invoke ("KillDust",0.9f);
	}
	
	void KillDust()
	{
		Destroy(gameObject);
	}
}
