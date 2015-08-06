using UnityEngine;
using System.Collections;

public class MoveGameModeChoices : MonoBehaviour {

	private RectTransform myRect;
	private float speed = 4;
	
	// Use this for initialization
	void Start () {
		myRect = GetComponent<RectTransform>();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
	
		if (myRect.position.x == 0)
		{
			myRect.position = new Vector3 (transform.position.x - speed * Time.deltaTime,0,0);
		}
	
	}
}
