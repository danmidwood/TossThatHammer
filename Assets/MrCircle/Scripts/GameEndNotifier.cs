using UnityEngine;
using System.Collections;

public class GameEndNotifier : MonoBehaviour {

	GameObject player;
	bool gameOverMessageSent = false;


	void Start() {
		player = GameObject.Find("base");
	}

	void Update() {
		if (IsGameOver() && !gameOverMessageSent) {
			gameOverMessageSent = true;
			player.SendMessage("GameOver");
		}

	}


	bool IsGameOver() {
		return GameManager.completedScene;
	}
}
