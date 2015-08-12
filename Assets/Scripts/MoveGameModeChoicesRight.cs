using UnityEngine;
using System.Collections;

public class MoveGameModeChoicesRight : MonoBehaviour {

	public RectTransform myRect;
	
	private GameObject leftArrow;
	private string moveToSpot;
	
	void Start ()
	{
		leftArrow = GameObject.FindGameObjectWithTag("arrowLeft");
	}
	
	// Update is called once per frame
	void Update () {
	
		if(myRect.position.x == 0)
		{
			leftArrow.SetActive(false);
		}
		else
		{
			leftArrow.SetActive(true);
		}
			
		if (moveToSpot == "Right1")
		{
			myRect.position = new Vector3(Mathf.MoveTowards(myRect.position.x, -6f, 6*Time.deltaTime),0,0);
			if(myRect.position.x == -6f)
			{
				moveToSpot = "";
			}
		}
		else if (moveToSpot == "Right2")
		{
			myRect.position = new Vector3(Mathf.MoveTowards(myRect.position.x, -12f, 6*Time.deltaTime),0,0);
			if(myRect.position.x == -12f)
			{
				moveToSpot = "";
			}
		}
	}
	
	void OnMouseUp()
	{
		if (myRect.position.x == 0)
		{
			moveToSpot = "Right1";
		}
		else if (myRect.position.x == -6f)
		{
			moveToSpot = "Right2";
		}
	}
}
