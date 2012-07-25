using UnityEngine;
using System.Collections;

public class MagicBridge : MonoBehaviour {

	void Win() {
		GameManager.WinScene();
	}

	void Point() {
		GameManager.score = GameManager.score + 1;
	}
}
