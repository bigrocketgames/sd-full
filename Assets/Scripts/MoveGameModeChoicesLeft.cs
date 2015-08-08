using UnityEngine;
using System.Collections;

public class MoveGameModeChoicesLeft : MonoBehaviour {

	public RectTransform myRect;
	
	private string moveToSpot;
	private GameObject rightArrow;
	
	void Start ()
	{
		rightArrow = GameObject.FindGameObjectWithTag("arrowRight");
	}
	
	// Update is called once per frame
	void Update () {
		
		if(myRect.position.x == -12f)
		{
			rightArrow.SetActive(false);
		}
		else
		{
			rightArrow.SetActive(true);
		}
		
		if (moveToSpot == "Left1")
		{
			myRect.position = new Vector3(Mathf.MoveTowards(myRect.position.x, -6f, 6*Time.deltaTime),0,0);
			if(myRect.position.x == -6f)
			{
				moveToSpot = "";
			}
		}
		else if (moveToSpot == "Left2")
		{
			myRect.position = new Vector3(Mathf.MoveTowards(myRect.position.x, 0f, 6*Time.deltaTime),0,0);
			if(myRect.position.x == 0f)
			{
				moveToSpot = "";
			}
		}
	}
	
	void OnMouseUp()
	{
		print (myRect.position.x);
		
		if (myRect.position.x == -12f)
		{
			moveToSpot = "Left1";
		}
		else if (myRect.position.x == -6f)
		{
			moveToSpot = "Left2";
		}
	}
}
