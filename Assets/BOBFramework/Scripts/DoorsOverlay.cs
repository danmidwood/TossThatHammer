using UnityEngine;
using System.Collections;

public class DoorsOverlay : MonoBehaviour {
	
	public Texture2D topDoorTex;
	public Texture2D bottomDoorTex;
	
	public GUISkin guiSkin;
	
	public GUIStyle gameOverTitleStyle;
	public GUIStyle scorePanelStyle;
	public GUIStyle retryButtonStyle;
	public GUIStyle quitButtonStyle;
	public GUIStyle scoreTextStyle;
	
	public GUIStyle thumbUpStyle;
	public GUIStyle thumbDownStyle;
	
	public Texture2D largeLogoAGAITCHESON;
	public Texture2D largeLogoSPILTMILK;
	public Texture2D largeLogoMOBILEPIE;
	public Texture2D largeLogoBIGPIXEL;
	public Texture2D largeLogoFGOL;
	public Texture2D largeLogoINDIESKIES;
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void OnGUI(){
		
		GUI.skin = guiSkin;
		
		GUIManager.SetUpGUI();
		
		float doorsCloseAmount = GameManager.doorsCloseTimer;

		
		GUI.DrawTexture(new Rect(0, ((1 - doorsCloseAmount) * -320), 960, 320), topDoorTex);
		GUI.DrawTexture(new Rect(0, 320 + ((1 - doorsCloseAmount) * 320), 960, 320), bottomDoorTex);
		
		ManageThumbs();
		if (GameManager.showGameOverGraphics)
		{
			ManageGameOver();
		}
		else
		{
			ManageStudioLogo();
		}
		
		GUIManager.ResetGUI();
		
		GUI.skin = null;
	}
	
	void ManageThumbs()
	{
		float thumbsPos = GameManager.thumbIconTimer;
		
		if(GUI.Button(new Rect(- 120 + (140 * thumbsPos), 260, 120, 120), "", thumbUpStyle)) //183, 174
		{
			GameManager.HideThumbs();
		}
		if(GUI.Button(new Rect(960 - (140 * thumbsPos), 260, 120, 120), "", thumbDownStyle))
		{
			GameManager.HideThumbs();
		}
	}
	
	void ManageStudioLogo()
	{
		float logoTimer = (1 - GameManager.thumbIconTimer);
		if (GameManager.doorsClose)
		{
			logoTimer = -logoTimer;
		}
		
		GUI.DrawTexture(new Rect(180 , 70 + (640 * logoTimer), 600, 500), LargeLogoForStudio(GameManager.StudioForScene(GameManager.nextSceneNumber)), ScaleMode.ScaleToFit, true, 0);
	}
	
	void ManageGameOver()
	{
		float tempTime = GameManager.gameOverTimer;

		GUI.Box(new Rect(144 + ((1 - tempTime) * 960), 20, 672, 118), "", gameOverTitleStyle);
		
		GUI.Box(new Rect(147 + ((1 - tempTime) * 960), 170, 667, 243), "", scorePanelStyle);
		GUI.Box(new Rect(147 + ((1 - tempTime) * 960), 300, 667, 64), GameManager.score.ToString(), scoreTextStyle);
		
		if(GUI.Button(new Rect(100 + (960 * (1 - tempTime)), 425, 361, 155), "", retryButtonStyle))
		{
			GameManager.StartNewGame();
		}
		if(GUI.Button(new Rect(500 + (960 * (1 - tempTime)), 425, 361, 155), "", quitButtonStyle))
		{
			GameManager.ReturnToMenu();
		}
	}
	
	private Texture2D LargeLogoForStudio(MoreGamesGUI.StudioHandle myHandle)
	{
		switch (myHandle)
		{
		case MoreGamesGUI.StudioHandle.SPILTMILK:
			return largeLogoSPILTMILK;
			
		default:
			return null;
		}
	}
}
