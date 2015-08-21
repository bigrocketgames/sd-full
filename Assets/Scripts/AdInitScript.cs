using UnityEngine;
using System.Collections;

public class AdInitScript : MonoBehaviour {

	void Start () {
		// Your Publisher ID is: 782c3494fb4c83a6b382abbbb6c3cdc4
		HeyzapAds.start("782c3494fb4c83a6b382abbbb6c3cdc4", HeyzapAds.FLAG_NO_OPTIONS);
		HZIncentivizedAd.fetch("default");
//		HeyzapAds.showMediationTestSuite();
	}
}
