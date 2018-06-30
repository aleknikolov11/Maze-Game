using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Heyzap;

public class AdManager : MonoBehaviour {

	public static AdManager instance;

	void Awake() {
		if (instance == null)
			instance = this;
	}

	void Start () {
		HeyzapAds.Start("3a045a04722e168baefdd3eeff198305", HeyzapAds.FLAG_NO_OPTIONS);
		HZVideoAd.Fetch ();
		HZIncentivizedAd.Fetch ();
		HZIncentivizedAd.AdDisplayListener listener = delegate(string adState, string adTag) {
			if (adState.Equals ("incentivized_result_complete")) {
				PlayerPrefs.SetInt("currentShards", PlayerPrefs.GetInt("currentShards") + 10);
			}
			if (adState.Equals ("incentivized_result_incomplete")) {
				//Don give him/her shards
			}
		};
		HZIncentivizedAd.SetDisplayListener (listener);

	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void showRewardedAd() {
		if (HZIncentivizedAd.IsAvailable())
			HZIncentivizedAd.Show ();
	}

	public void showVideoAd () {
		if (HZVideoAd.IsAvailable ())
			HZVideoAd.Show ();
	}
}
