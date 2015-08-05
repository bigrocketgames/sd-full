using UnityEngine;
using System.Collections;

public class MeteorSpawner : MonoBehaviour {
	
	public GameObject[] meteors;
	
	private int meteorChoice;
	
	// Use this for initialization
	void Start () {
		InvokeRepeating("SpawnMeteor", 10f, 3f);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void SpawnMeteor()
	{
		meteorChoice = Random.Range(0,meteors.Length);
		float randomX = Random.Range(-2.7f,2.71f);
		float randomY = Random.Range(5.5f, 8f);
		
		GameObject meteor = Instantiate(meteors[meteorChoice],new Vector3(randomX,randomY,0),Quaternion.identity) as GameObject;
	}
}
