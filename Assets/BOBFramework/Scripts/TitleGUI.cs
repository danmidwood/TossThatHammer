using UnityEngine;
using System.Collections;

public class TitleGUI : MonoBehaviour {
	
	public Texture2D playTexture;
	public Texture2D moreGamesTexture;
	public Texture2D titleTexture;
	public GUISkin guiSkin;
	
	public GUIStyle backgroundImageStyle;
	public GUIStyle playButtonStyle;
	public GUIStyle moreGamesButtonStyle;
	public GUIStyle scoreMeterStyle;
	
	private string creditsString;
	private float creditsTimer;
	private int currentCredit;
	
	public bool selectedNextPage;
	public int nextPageNum;
	public float pageTransTime;
	
	public SFX buttonPress;	
	AudioManager _audioManager;
	
	// Use this for initialization
	void Start () {
		_audioManager = GetComponentInChildren<AudioManager>();
	}
	
	// Update is called once per frame
	void Update () {
		switch (currentCredit)
		{
			case 0:
				creditsString = "COPYRIGHT 2012 BEST OF BRITISH TEAM";
				break;

			case 1:
				creditsString = "SPECIAL THANKS TO UNITY, MIND CANDY...";
				break;

			case 2:
				creditsString = "... AND OSBORNE CLARKE FOR THEIR SUPPORT";
				break;
			
			default:
				currentCredit = 0;
				break;
		}
			
		creditsTimer += Time.deltaTime;
		if(creditsTimer > 5)
		{
			currentCredit ++;
			creditsTimer = 0;
		}
	}
	
	void OnGUI()
	{
		GUIManager.SetUpGUI();
		
		GUI.skin = guiSkin;
		
		GUI.Box(new Rect(0,0, 960, 640), "", backgroundImageStyle);
		
		GUI.Box(new Rect(70,70,820,200), titleTexture);
		
		if(GUI.Button(new Rect(100, 270, 370, 180), "", playButtonStyle))
		{
			//StartGameTransition();
			selectedNextPage = true;
			nextPageNum = 0;
			PlaySelectSound();
		}
		
		if(GUI.Button(new Rect(490, 270, 370, 180), "", moreGamesButtonStyle))
		{
			//GoToMoreGamesScene();
			selectedNextPage = true;
			nextPageNum = 1;
			PlaySelectSound();
		}
		
		GUI.Box(new Rect(58,455,844,85), GameManager.highScore.ToString(), scoreMeterStyle);		
		GUI.Box(new Rect(0,545,960,37), creditsString);
		
		GUI.skin = null;
		
		GUIManager.ResetGUI();
		
		if (selectedNextPage)
		{
			pageTransTime += Time.deltaTime;
			
			if (pageTransTime > 0.2f)
			{
				if (nextPageNum == 1)
				{
					GoToMoreGamesScene();
				}
				else
				{
					StartGameTransition();
				}
			}

		}
	}
	
	
	void GoToMoreGamesScene()
	{
		Application.LoadLevel(1);
	}
	
	void StartGameTransition()
	{
		GameManager.justLeftTitleScreen = true;
		GameManager.nextSceneNumber = GameManager.RandomSceneNumber();
		GameManager.EndCurrentScene();
	}
	
	void PlaySelectSound()
	{
		_audioManager.PlaySFX(buttonPress);
	}
}