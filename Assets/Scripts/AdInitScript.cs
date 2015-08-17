using UnityEngine;
using System.Collections;

public class AdInitScript : MonoBehaviour {

	void Awake () {
		// Your Publisher ID is: 782c3494fb4c83a6b382abbbb6c3cdc4
		HeyzapAds.start("782c3494fb4c83a6b382abbbb6c3cdc4", HeyzapAds.FLAG_NO_OPTIONS);
		if(HZIncentivizedAd.isAvailable("default"))
		{
			return;
		}
		else
		{
			HZIncentivizedAd.fetch("default");
		}
		
	}
}
