using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Heyzap;

public class Maze26 : MonoBehaviour {
	public static Maze26 instance;
	public GameObject Ball;
	public GameObject FinishParticleSystem;
	public GameObject Shard;
	public GameObject LevelCompletePanel;
	public GameObject MenuPanel;
	public GameObject MapPanel;
	public GameObject MoreShards;
	public Image Map;
	public Sprite ShardsCollected;
	public Image[] menuShards;
	public int[,] map;
	public int[,] rotations;
	Vector3 start = new Vector3(0.05f, 0.05f, 3.05f);
	Vector3 finish = new Vector3(7.05f, 0.05f, 4.05f);
	private int currentLevel;
	private int localLevel;
	private int currentSpawnsLeft;
	public Text shardsText;
	private float currentTime;
	public Text currentTimeText;
	public Text currentTimeScore;
	public Text bestTimeScore;
	private string timeString;
	private string bestTimeVariable;
	private string currentShardsForLevelVariable;
	private string currentMapVariable;
	private bool gameOver;
	private bool mapButtonClicked;
	private float firstX, secondX, firstY, secondY;

	void Awake() {
		if (instance == null)
			instance = this;
	}

	void Start() {
		mapButtonClicked = false;
		bestTimeVariable = "bestTimeLevel26";
		currentShardsForLevelVariable = "currentShardsForLevel26";
		currentMapVariable = "mapLevel26";
		currentLevel = 26;
		localLevel = 6;
		HeyzapAds.Start("3a045a04722e168baefdd3eeff198305", HeyzapAds.FLAG_NO_OPTIONS);
		HZVideoAd.Fetch ();
		HZIncentivizedAd.Fetch ();
		map = new int[8, 8] {{4, 1, 3, 1, 3, 3, 3, 2}, {2, 1, 4, 3, 2, 2, 2, 2}, {3, 2, 2, 2, 2, 3, 3, 4}, {1, 2, 2, 3, 4, 3, 3, 3},
			{2, 3, 2, 2, 3, 2, 1, 2}, {4, 3, 2, 2, 2, 2, 2, 4}, {2, 3, 3, 3, 2, 3, 4, 1}, {2, 3, 3, 3, 4, 2, 3, 2}};
		rotations = new int[8, 8] {{1, 1, 0, 1, 0, 0, 0, 1}, {0, 3, 3, 1, 0, 1, 0, 2}, {1, 0, 1, 3, 2, 1, 1, 2}, {0, 2, 3, 0, 3, 1, 1, 1},
			{3, 0, 1, 0, 0, 2, 0, 2}, {1, 0, 2, 3, 1, 0, 2, 2}, {0, 0, 0, 0, 2, 1, 1, 2}, {3, 0, 0, 0, 3, 3, 0, 2}};
		MazeGenerator.instance.mazeGenerator (map, rotations, 8);
		Ball.transform.position = start;
		Ball.transform.localScale = new Vector3 (0.2f, 0.2f, 0.2f);
		Ball.SetActive (true);
		Instantiate (FinishParticleSystem, finish, Quaternion.identity);
		if (!PlayerPrefs.HasKey (currentShardsForLevelVariable))
			PlayerPrefs.SetInt (currentShardsForLevelVariable, 0);

		currentSpawnsLeft = 3 - PlayerPrefs.GetInt(currentShardsForLevelVariable);
		firstX = secondX = firstY = secondY = 100f;
		while(currentSpawnsLeft > 0){
			int randX = Random.Range (0, 8);
			int randZ = Random.Range (0, 8);
			float Shardx = (float)randX + 0.05f;
			float Shardy = (float)randZ + 0.05f;
			if (Shardx != start.x && Shardx != finish.x && Shardy != start.z && Shardy != finish.z && Shardx != (firstX + 0.05f) && Shardx != (secondX + 0.05f) && Shardy != (firstY + 0.05f) && Shardy != (secondY + 0.05f)) { 
				Instantiate (Shard, new Vector3 ((float)randX, 0.15f, (float)randZ), Quaternion.identity);
				currentSpawnsLeft--;
				if (currentSpawnsLeft == 2) {
					firstX = Shardx;
					firstY = Shardy;
				}
				if (currentSpawnsLeft == 1) {
					secondX = Shardx;
					secondY = Shardy;
				}
			}
		}
		if (!PlayerPrefs.HasKey (bestTimeVariable)) 
			bestTimeScore.text = "00:00";
		else 
			bestTimeScore.text = ((int)(PlayerPrefs.GetFloat(bestTimeVariable)/ 60f)).ToString ("00") + ":" + ((int)(PlayerPrefs.GetFloat (bestTimeVariable) % 60f)).ToString ("00");
		currentTime = 0f;
		shardsText.text = PlayerPrefs.GetInt ("currentShards").ToString();
		currentTimeText.text = "";
		currentTimeScore.text = "";
		bestTimeScore.text = "";
		timeString = "";
		gameOver = false;
	}


	public void MainMenu() {
		if (PlayerPrefs.GetInt ("currentAdIteration") == 2) {
			PlayerPrefs.SetInt ("currentAdIteration", 0);
			if (HZVideoAd.IsAvailable ())
				HZVideoAd.Show ();
		} else
			PlayerPrefs.SetInt ("currentAdIteration", PlayerPrefs.GetInt ("currentAdIteration") + 1);
		SceneManager.LoadScene (0);
	}

	public void ReplayLevel() {
		if (PlayerPrefs.GetInt ("currentAdIteration") == 2) {
			PlayerPrefs.SetInt ("currentAdIteration", 0);
			if (HZVideoAd.IsAvailable ())
				HZVideoAd.Show ();
		} else
			PlayerPrefs.SetInt ("currentAdIteration", PlayerPrefs.GetInt ("currentAdIteration") + 1);
		SceneManager.LoadScene (currentLevel);
	}

	public void NextLevel() {
		if (PlayerPrefs.GetInt ("currentAdIteration") == 2) {
			PlayerPrefs.SetInt ("currentAdIteration", 0);
			if (HZVideoAd.IsAvailable ())
				HZVideoAd.Show ();
		} else
			PlayerPrefs.SetInt ("currentAdIteration", PlayerPrefs.GetInt ("currentAdIteration") + 1);
		SceneManager.LoadScene (currentLevel + 1);
	}

	public void GameOver() {
		for (int i = 0; i < PlayerPrefs.GetInt (currentShardsForLevelVariable); i++) {
			menuShards [i].sprite = ShardsCollected;
		}
		currentTimeScore.text = ((int)(currentTime / 60f)).ToString ("00") + ":" + ((int)(currentTime % 60f)).ToString ("00");
		LevelCompletePanel.SetActive (true);
		Ball.GetComponent<Rigidbody> ().velocity = Vector3.zero;
		Ball.GetComponent<Rigidbody> ().position = Vector3.Slerp (Ball.GetComponent<Rigidbody> ().position, finish, 2.5f);
		PlayerPrefs.SetInt ("8x8rectangle" + (localLevel + 1).ToString () + "isUnlocked", 1);
		currentTime = Time.time;
		if (PlayerPrefs.HasKey (bestTimeVariable)) {
			if (currentTime < PlayerPrefs.GetFloat (bestTimeVariable)) {
				PlayerPrefs.SetFloat (bestTimeVariable, currentTime);
				bestTimeScore.text = timeString;
			}
		} else {
			PlayerPrefs.SetFloat (bestTimeVariable, currentTime);
			bestTimeScore.text = timeString;
		}
		gameOver = true;
	}

	public void Collect () {
		PlayerPrefs.SetInt ("currentShards", PlayerPrefs.GetInt ("currentShards") + 1);
		PlayerPrefs.SetInt (currentShardsForLevelVariable, PlayerPrefs.GetInt (currentShardsForLevelVariable) + 1);
		Ball.GetComponent<BallController> ().collect = false;
	}

	public void Update() {
		shardsText.text = PlayerPrefs.GetInt ("currentShards").ToString();
		if (!gameOver) {
			currentTime += 1 * Time.deltaTime;
			timeString = currentTimeText.text = ((int)(currentTime / 60f)).ToString ("00") + ":" + ((int)(currentTime % 60f)).ToString ("00");
		}
		if (Ball.GetComponent<BallController> ().gameOver && !gameOver)
			GameOver ();
		if (Ball.GetComponent<BallController> ().collect)
			Collect ();
	}


	public void ShowMenuPanel() {
		MenuPanel.SetActive (true);
	}

	public void HideMenuPanel() {
		MenuPanel.SetActive (false);
	}

	public void ShowMap () {
		if (!mapButtonClicked) {
			if (PlayerPrefs.HasKey (currentMapVariable))
				Map.gameObject.SetActive (true);
			else
				MapPanel.SetActive (true);
			mapButtonClicked = true;
		} else {
			Map.gameObject.SetActive (false);
			MapPanel.SetActive (false);
			mapButtonClicked = false;
		}
	}

	public void BuyMap() {
		if (!PlayerPrefs.HasKey(currentMapVariable)) {
			if (PlayerPrefs.GetInt ("currentShards") >= 10) {
				PlayerPrefs.SetInt ("currentShards", PlayerPrefs.GetInt ("currentShards") - 10);
				MapPanel.SetActive (false);
				Map.gameObject.SetActive (true);
				PlayerPrefs.SetInt (currentMapVariable, 1);
			} else {
				if (HZIncentivizedAd.IsAvailable ()) {
					MapPanel.SetActive (false);
					MoreShards.SetActive (true);
				}
			}
		}
	}

	public void GetShards() {
		if (HZIncentivizedAd.IsAvailable ()) {
			HZIncentivizedAd.Show ();
			PlayerPrefs.SetInt ("currentShards", PlayerPrefs.GetInt ("currentShards") + 5);
			MoreShards.SetActive (false);
			MapPanel.SetActive (true);
		}
	}
}