using UnityEngine;
using System.Collections;

public class PlayGUI : MonoBehaviour {
	
	public GUISkin guiSkin;
	
	public Texture2D heartTexture;
	public Texture2D heartTextureEmpty;
	
	public GUIStyle topBarStyle;
	public GUIStyle bottomBarStyle;
	public GUIStyle timerStyle;
	
	public GUIStyle scoreStyle;
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void OnGUI(){
				
		GUIManager.SetUpGUI();
		
		GUI.skin = guiSkin;
		
		//GUI.Box(new Rect(0, 0, 960, 100), "Time Left: " + GameManager.remainingStageTime.ToString());
		
		GUI.Box(new Rect(0, 0, 960, 100), "", topBarStyle);
		GUI.Box(new Rect(0, 540, 960, 100), "", bottomBarStyle);
		
		float timeFractionRemaining = (GameManager.remainingStageTime / 7);
		timeFractionRemaining = Mathf.Max(0, timeFractionRemaining);
		
		//GUI.Box(new Rect(39, 27, 880 * timeFractionRemaining, 39), "", timerStyle);
		GUI.Box(new Rect(41, 29, 876 * timeFractionRemaining, 35), "", timerStyle);
		
		GUI.Box(new Rect(160, 570, 445, 50), GameManager.score.ToString(), scoreStyle);
		
		float heartSpacing = 300.0f / (Mathf.Max(3, GameManager.remainingLives));
		
		for(int i = 0; (i < GameManager.remainingLives) || (i < 3); i++)
		{
			GUI.Box(new Rect(960 - ((i + 1) * heartSpacing), 555, 84, 75), 
				(i < GameManager.remainingLives)? heartTexture : heartTextureEmpty);
		}
		
		GUI.skin = null;
		
		GUIManager.ResetGUI();
	}
}
