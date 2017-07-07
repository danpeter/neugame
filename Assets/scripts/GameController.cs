using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour {

	public GameObject player;
	public GameObject[] hazards;
	public Vector3 spawnValues;
	public int hazardCount;
	public float spawnWait;
	public float startWait;
	public float waveWait;
	public Text scoreText;
	private int score;

	public Text restartText;
	public Text gameOverText;

	private bool gameOver;
	private bool restart;

	void Start () {
		gameOver = false;
		restart = false;
		restartText.text = "";
		gameOverText.text = "";
		score = 0;
		UpdateScore ();
		StartCoroutine (SpawnWaves ());
	}

	void Update() 
	{
		if (restart) {
			if (Input.GetKeyDown (KeyCode.R)) {
				Application.LoadLevel (Application.loadedLevel);
			}
		}
	}

	IEnumerator SpawnWaves ()
	{
		yield return new WaitForSeconds (startWait);
		while (true) {
			for (int i = 0; i < hazardCount; i++) {
				//Vector3 spawnPosition = new Vector3 (Random.Range (-spawnValues.x, spawnValues.x), spawnValues.y, spawnValues.z);
				if (player == null) {
					break;
				}
				Vector3 spawnPosition = player.transform.position;
				Debug.Log (spawnPosition.ToString ());
				spawnPosition.x += (Random.Range (10, 20) * (Random.value < 0.5 ? 1 : -1));
				spawnPosition.z += (Random.Range (10, 20) * (Random.value < 0.5 ? 1 : -1));
				Debug.Log (spawnPosition.ToString ());
				Quaternion spawnRotation = Quaternion.identity;
				GameObject hazard = hazards[Random.Range(0, hazards.Length)];
				Instantiate (hazard, spawnPosition, spawnRotation);
				yield return new WaitForSeconds (spawnWait);
			}
			yield return new WaitForSeconds (waveWait);

			if (gameOver) {
				restartText.text = "Press 'R' for Restart";
				restart = true;
				break;
			}
		}
	}

	public void AddScore(int newScoreValue)
	{
		score += newScoreValue;
		UpdateScore ();
	}

	void UpdateScore()
	{
		scoreText.text = "Score: " + score;
	}

	public void GameOver ()
	{
		gameOverText.text = "Game Over";
		gameOver = true;
	}

}
