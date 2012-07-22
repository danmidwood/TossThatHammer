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
	public Texture2D largeLogoONIMOBI;
	
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

		
		GUI.DrawTexture(new Rect(0, ((1 - doorsCloseAmount) * -327), 960, 327), topDoorTex);
		GUI.DrawTexture(new Rect(0, 327 + ((1 - doorsCloseAmount) * 327), 960, 327), bottomDoorTex);
				
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
		
			if (GameManager.justLostGame)
			{
				GUI.Box(new Rect(0, -90 + (135 * thumbsPos), 960, 40), "STAGE FAILED");
				
				string lostString = GameManager.remainingLives.ToString() + " LIVES REMAINING";
				if (GameManager.remainingLives == 1)
				{
					lostString = "ONE LIFE LEFT...";
				}
				GUI.Box(new Rect(0, 670 - (120 * thumbsPos), 960, 40), lostString);
			}
			else
			{
				int winStringNumber = (GameManager.score / 100) % 7;
				string winString = "Well done!";
				
				switch (winStringNumber)
				{
					case 0:
						winString = "GO GET 'EM!";
						break;
					
					case 1:
						winString = "GREAT JOB, TIGER!";
						break;
					
					case 2:
						winString = "NICE WORK!";
						break;
					
					case 3:
						winString = "KEEP IT UP!";
						break;
					
					case 4:
						winString = "SUPER!";
						break;
					
					case 5:
						winString = "JUST LIKE A PRO";
						break;
					
					case 6:
						winString = "FANTASTIC!";
						break;
				}
				
				//GUI.Box(new Rect(0, -670 + (175 * thumbsPos), 960, 40), winString);
				
			}
	}
	
	void ManageStudioLogo()
	{
		float logoTimer = (1 - GameManager.thumbIconTimer);
		if (GameManager.doorsClose)
		{
			logoTimer = -logoTimer;
		}
		if (LargeLogoForStudio(GameManager.StudioForScene(GameManager.nextSceneNumber)) != null)
		{
			//was 600 x 500
			GUI.DrawTexture(new Rect(180 , 120 + (640 * logoTimer), 600, 400), LargeLogoForStudio(GameManager.StudioForScene(GameManager.nextSceneNumber)), ScaleMode.ScaleToFit, true, 0);
		}
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
		case MoreGamesGUI.StudioHandle.BIGPIXEL:
			return largeLogoBIGPIXEL;
		case MoreGamesGUI.StudioHandle.AGAITCHESON:
			return largeLogoAGAITCHESON;
			
		default:
			return null;
		}
	}
}
