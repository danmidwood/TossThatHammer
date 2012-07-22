using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {
	
	public static bool doorsClose;
	public static float doorsCloseTimer;
	public static int remainingLives = 3;
	public static int startingLives = 3;
	public static int score = 0;
	public static int highScore;
	
	public static bool completedScene;
	public static bool delayForCompletion;
	public static float completionDelay = 0;
	
	public static float remainingStageTime = 7;
	public static bool stageIsPlay;
	
	public static float playSpeed = 1;

	public static bool justLoadedScene;
	public static int sceneFramecount = 0;
	
	public static float thumbIconTimer = 0;
	public static bool showThumbs = false;
	
	public static float waitForNextStageTimer = 0;
	
	public static bool gameOver = false;
	public static float gameOverTimer = 0;
	
	public static int nextSceneNumber = 2;
	public static int previousSceneNumber = 0;
	public static bool justLeftGameOver = false;
	
	public static bool showGameOverGraphics;
	public static bool justLostGame;
	public static bool returnToMainMenu;
	public static bool justLeftTitleScreen;
	
	static AudioManager _audioManager;
	
	// Use this for initialization
	void Start () {
		GUIManager.CalculateScaleProperties();
		GetHighScore();
		
		_audioManager = GetComponentInChildren<AudioManager>();
	}
	
	// Update is called once per frame
	void Update () 
	{
	
		if (sceneFramecount > 1)
			//(!justLoadedScene)//to stop the loading time being counted by the deltatime and taken from the clock
		{
			//Debug.Log(stageIsPlay.ToString() + " next: " + nextSceneNumber);
			
			if(stageIsPlay)
			{
				//Debug.Log("COMPLETED: " + completedScene.ToString() + " DELAYED: " + completionDelay.ToString());
				
				if (!completedScene)
				{
					if (doorsCloseTimer <= 0) // the timer doesn't start until the doors are fully open
					{
						remainingStageTime -= Time.deltaTime;
					}
				}
				else
				{
					if (delayForCompletion)
					{
						completionDelay += Time.deltaTime;
						if (completionDelay >= 1)
						{
							delayForCompletion = false;
							EndCurrentScene(); // this thing gives you 1 second after you win the game before it closes the doors
						}
					}
				}
				
				
				if(remainingStageTime <= 0 && !completedScene)
				{
					LoseScene();
				}
			}
			
			if(doorsClose)
			{
				doorsCloseTimer += Time.deltaTime;
				doorsCloseTimer = Mathf.Min(doorsCloseTimer, 1);
				
				
				if(completedScene && doorsCloseTimer >= 1
					&& !gameOver)
				{
					AwaitTimerForNextStage();
				}
			}
			else
			{
				doorsCloseTimer -= Time.deltaTime;
				doorsCloseTimer = Mathf.Max(doorsCloseTimer, 0);
				if (doorsCloseTimer <= 0 && justLostGame)
				{
					if (!completedScene)
					{
						justLostGame = false;
					}
				}
				if (doorsCloseTimer <= 0 && showGameOverGraphics)
				{
					showGameOverGraphics = false;
				}
			}
			
			UpdateThumbs();	
			UpdateGameOver();
		}
		
		if (!Application.isLoadingLevel)
		{
			justLoadedScene = false;
			sceneFramecount ++;
		}
	}
	
	void LoadNextScene()
	{				
		playSpeed += 0.1f;
		Time.timeScale = playSpeed;
		remainingStageTime = 7;		

		sceneFramecount = 0;
		
		//doorsCloseTimer = 0;
		justLoadedScene = true;
		previousSceneNumber = nextSceneNumber;
		Application.LoadLevel(nextSceneNumber);
		
		StartScene();
		HideThumbs();
	}
	
	public static void EndCurrentScene()
	{
		nextSceneNumber = RandomSceneNumber();
		if (returnToMainMenu)
		{
			nextSceneNumber = 0;
			returnToMainMenu = false;
		}
		completionDelay = 0;
		
		doorsClose = true;
		completedScene = true;
		if (remainingLives <= 0)
		{
			StartGameOver();
		}
		else
		{
			if (previousSceneNumber != 0 && !justLeftGameOver && !justLeftTitleScreen)
			{
				ShowThumbs();
			}
		}
		
		justLeftGameOver = false;
		justLeftTitleScreen = false;
	}
	
	public static void LoseScene()
	{
		if (!completedScene)//note - this stops Win/Lose being called multiple times during the door transition
		{
			remainingLives -= 1;
			//EndCurrentScene();
			justLostGame = true;
			
			completedScene = true;
			delayForCompletion = true;
		}
	}
	
	public static void WinScene()
	{
		if (!completedScene)//note - this stops Win/Lose being called multiple times during the door transition
		{
			score += 100;
			SaveScore();
			//EndCurrentScene();
			//now you wait for the completion timer
			completedScene = true;
			delayForCompletion = true;
		}
	}
	
	public static void StartScene()
	{
		//justLostGame = false;
		completedScene = false;
		doorsClose = false;
		stageIsPlay = true; //set to false if it's title screen etc. Tells the timer to count down!
		if (nextSceneNumber == 0)
		{
			stageIsPlay = false;
		}
	}
	
	void AwaitTimerForNextStage()
	{
		waitForNextStageTimer += Time.fixedDeltaTime;
		
		if (waitForNextStageTimer >= 1 && !_audioManager.MusicSource.isPlaying)
		{
			LoadNextScene();
			waitForNextStageTimer = 0;
		}
	}
	
	public static bool SceneIsActive()
	{
		return (sceneFramecount > 1);
	}
	
	void UpdateThumbs()
	{
		//Debug.Log("thumbs time " + thumbIconTimer.ToString() + " " + showThumbs.ToString());
		
		if (showThumbs)
		{
			thumbIconTimer += Time.deltaTime * 2;
			thumbIconTimer = Mathf.Min(thumbIconTimer, 1);
		}
		else
		{
			thumbIconTimer -= Time.deltaTime * 2;
			thumbIconTimer = Mathf.Max(thumbIconTimer, 0);
		}
	}
	
	public static void ShowThumbs()
	{
		showThumbs = true;
		_audioManager.PlayMusic(false, Time.timeScale);
	}
	
	public static void HideThumbs()
	{
		showThumbs = false;
	}
	
	public static void ResetPlayData()
	{
		score = 0;
		remainingLives = startingLives;
		playSpeed = 1;
		
		gameOver = false;
	}
	
	public static void StartGameOver()
	{
		gameOver = true;
		Time.timeScale = 1;
		showGameOverGraphics = true;
	}
	
	public static void ReturnToMenu()
	{
		justLeftGameOver = true;
		returnToMainMenu = true;
		
		ResetPlayData();
		nextSceneNumber = 0;
		EndCurrentScene();
		
		stageIsPlay = false;
	}
	
	public static void StartNewGame()
	{
		justLeftGameOver = true;
		
		ResetPlayData();
		//nextSceneNumber = RandomSceneNumber();
		EndCurrentScene();
	}
	
	void UpdateGameOver()
	{
		if (gameOver)
		{
			gameOverTimer += Time.deltaTime;
			gameOverTimer = Mathf.Min(gameOverTimer, 1);
		}
		else
		{
			gameOverTimer -= Time.deltaTime;
			gameOverTimer = Mathf.Max(gameOverTimer, 0);
		}
	}
	
	public static int RandomSceneNumber()
	{
		//will change once I have more scenes to choose from!!
		int myNextScene = 2;//Random.Range(2,4 + 1);
		//Debug.Log("Next Scene: #" + myNextScene.ToString());
		return myNextScene;
	}
	
	public static void SaveScore()
	{
		if (score > highScore)
		{
			PlayerPrefs.SetInt("highScore", score);
			highScore = score;
		}
	}
	
	public static int GetHighScore()
	{
		highScore = PlayerPrefs.GetInt("highScore", 0);
		return highScore;
	}
	
	public static MoreGamesGUI.StudioHandle StudioForScene(int mySceneNum)
	{
		if (mySceneNum == 2)
		{
			return MoreGamesGUI.StudioHandle.SPILTMILK;
		}
		if (mySceneNum == 3)
		{
			return MoreGamesGUI.StudioHandle.AGAITCHESON;
		}
		if (mySceneNum == 4)
		{
			return MoreGamesGUI.StudioHandle.BIGPIXEL;
		}
		
		return MoreGamesGUI.StudioHandle.endOfList;
	}
}
