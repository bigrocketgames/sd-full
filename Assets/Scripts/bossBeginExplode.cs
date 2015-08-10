using UnityEngine;
using System.Collections;

public class bossBeginExplode : MonoBehaviour {

	private Transform tran;
	
	// Use this for initialization
	void Start () {
		tran = GetComponentInParent<Transform>();
	}
	
	// Update is called once per frame
	void Update () {
		
		
	}
	
	public void ExplosionOffset(int count)
	{
		
		
		switch (count)
		{
			case 1:
				transform.position = new Vector3(tran.position.x + 0.75f, tran.position.y + 0.75f, 0);
				break;
			case 2:
				transform.position = new Vector3(tran.position.x - 1.5f, tran.position.y - 1.25f, 0);
				break;
			case 3:
				transform.position = new Vector3(tran.position.x + 1.5f, tran.position.y, 0);
				break;
			case 4:
				transform.position = new Vector3(tran.position.x - 1.5f, tran.position.y + 1.25f, 0);
				break;
		}
		
	}
}
