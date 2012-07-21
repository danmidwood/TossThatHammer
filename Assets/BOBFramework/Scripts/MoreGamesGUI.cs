using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MoreGamesGUI : MonoBehaviour {
	
	public enum StudioHandle
	{
		AGAITCHESON,
		MOBILEPIE,
		SPILTMILK,
		BIGPIXEL,
		INDIESKIES,
		FGOL,
		endOfList
	}
	
	public GUISkin guiSkin;
	
	public GUIStyle backButtonStyle;
	public GUIStyle cellBackgroundStyle;
	
	public GUIStyle backgroundImageStyle;
	public GUIStyle titleBannerStyle;
	public GUIStyle smallFontStyle;
	
	public GUIStyle logoBoxStyle;
	public Texture2D logoAGAITCHESON;
	public Texture2D logoMOBILEPIE;
	public Texture2D logoSPILTMILK;
	public Texture2D logoBIGPIXEL;
	public Texture2D logoINDIESKIES;
	public Texture2D logoFGOL;
	
	private Vector2 scrollPosition = new Vector2();
	
	private float scrollSpeed = 2;
	
	private List<int> orderedStudios;
	
	// Use this for initialization
	void Start () {
		PopulateStudioList();
	}
	
	// Update is called once per frame
	void Update () {
		var tempTouch = Input.touches[0];
		
		if(tempTouch.phase == TouchPhase.Moved)
    		scrollPosition.y += tempTouch.deltaPosition.y * scrollSpeed;
	}
	
	void OnGUI(){
		
				
		GUIManager.SetUpGUI();
		
		GUI.skin = guiSkin;
		
		GUI.Box(new Rect(0,0, 960, 640), "", backgroundImageStyle);
		GUI.Box(new Rect(0,0, 960, 96), "", titleBannerStyle);
		
	    scrollPosition = GUI.BeginScrollView (new Rect (0, 96, 960, 640 - 96),
	        scrollPosition, new Rect(0,0, 900, 30 + (180 * ((int)StudioHandle.endOfList))), GUIStyle.none, GUIStyle.none);
	    
		StudioHandle currentStudio = StudioHandle.endOfList;
	    for (int i = 0 ; i < (int)StudioHandle.endOfList ; i++)
		{
			currentStudio = (StudioHandle)orderedStudios[i];
			
			if(GUI.Button(new Rect(120, (i * 180) + 15, 715, 169), "", cellBackgroundStyle))
			{
				OpenProductPageForStudio(currentStudio);
			}			
			
			GUI.Box(new Rect(120 + 180, (i * 180) + 15 + 30, 500, 45), StudioProductFromHandle(currentStudio));
			if(currentStudio == StudioHandle.FGOL)
			{
				GUI.Box(new Rect(120 + 190, (i * 180) + 15 + 95, 480, 45), StudioNameFromHandle(currentStudio), smallFontStyle);
			}
			else
			{
				GUI.Box(new Rect(120 + 190, (i * 180) + 15 + 95, 480, 45), StudioNameFromHandle(currentStudio));
			}
			
			GUI.DrawTexture(new Rect(120 + 55, (i * 180) + 15 + 20, 120, 120), LogoTextureForStudio(currentStudio), ScaleMode.ScaleToFit); //logoBoxStyle);
			
			/*if(GUI.Button(new Rect(0 + 35, (i * 120) + 15, 100, 100), "LOGO " + currentStudio.ToString()))
			{
				OpenHomepageForStudio(currentStudio);
			}
			
			if(GUI.Button(new Rect(120 + 35, (i * 120) + 10 + 15, 340, 80), StudioNameFromHandle(currentStudio)))
			{
				OpenHomepageForStudio(currentStudio);
			}
			
			if(GUI.Button(new Rect(480 + 35, (i * 120) + 10 + 15, 340, 80), StudioProductFromHandle(currentStudio)))
			{
				OpenProductPageForStudio(currentStudio);
			}*/
			
		}
		
	    // End the scroll view that we began above.
	    GUI.EndScrollView ();
		
		if(GUI.Button(new Rect(5, 5, 96, 76), "", backButtonStyle))
		{
			ReturnToTitleScreen();
		}
		
		GUI.skin = null;
		
		GUIManager.ResetGUI();
	}
	
	private string StudioNameFromHandle(StudioHandle myHandle)
	{
		string tempString = "Unknown Name";
		
		switch (myHandle)
		{
			case StudioHandle.AGAITCHESON:
				tempString = "Alistair Aitcheson";
				break;

			case StudioHandle.MOBILEPIE:
				tempString = "Mobile Pie";
				break;
			
			case StudioHandle.SPILTMILK:
				tempString = "Spilt Milk Studios";
				break;		
			
			case StudioHandle.BIGPIXEL:
				tempString = "Big Pixel";
				break;	
		
			case StudioHandle.INDIESKIES:
				tempString = "Indie Skies";
				break;	
		
			case StudioHandle.FGOL:
				tempString = "Future Games of London";
				break;	
		}
		
		return tempString;
	}
	
	private string StudioProductFromHandle(StudioHandle myHandle)
	{
		string tempString = "Unknown Product";
		
		switch (myHandle)
		{
			case StudioHandle.AGAITCHESON:
				tempString = "Greedy Bankers";
				break;

			case StudioHandle.MOBILEPIE:
				tempString = "My Star";
				break;
			
			case StudioHandle.SPILTMILK:
				tempString = "Hard Lines";
				break;		
			
			case StudioHandle.BIGPIXEL:
				tempString = "Off the Leash";
				break;	
		
			case StudioHandle.INDIESKIES:
				tempString = "Paradise Golf";
				break;	
		
			case StudioHandle.FGOL:
				tempString = "Hungry Shark";
				break;	
		}
		
		return tempString;
	}
	
	private void OpenHomepageForStudio(StudioHandle myHandle)
	{
		string tempURL = "";
		
		switch (myHandle)
		{
			case StudioHandle.AGAITCHESON:
				tempURL = "http://www.alistairaitcheson.com";
				break;

			case StudioHandle.MOBILEPIE:
				tempURL = "http://www.mobilepie.com";
				break;
			
			case StudioHandle.SPILTMILK:
				tempURL = "http://www.spiltmilkstudios.com";
				break;		
			
			case StudioHandle.BIGPIXEL:
				tempURL = "http://bigpixelstudios.co.uk";
				break;	
		
			case StudioHandle.INDIESKIES:
				tempURL = "http://www.indieskies.com/";
				break;	
		
			case StudioHandle.FGOL:
				tempURL = "http://www.futuregamesoflondon.com/";
				break;	
		}
		
		Application.OpenURL(tempURL);
	}
		
	private void OpenProductPageForStudio(StudioHandle myHandle)
	{
		//note: currently only directs to iOS page. Change this to detect Android and direct appropriately
		
		string tempURL = "";
		
		switch (myHandle)
		{
			case StudioHandle.AGAITCHESON:
				tempURL = "http://itunes.apple.com/gb/app/greedy-bankers-bailout!/id500676882?mt=8";
				break;

			case StudioHandle.MOBILEPIE:
				tempURL = "http://itunes.apple.com/app/my-star/id422562697";
				break;
			
			case StudioHandle.SPILTMILK:
				tempURL = "http://itunes.apple.com/gb/app/hard-lines/id440571567?mt=8";
				break;		
			
			case StudioHandle.BIGPIXEL:
				tempURL = "http://itunes.apple.com/us/app/off-the-leash/id480125356?mt=8";
				break;	
		
			case StudioHandle.INDIESKIES:
				tempURL = "http://itunes.apple.com/gb/app/paradise-golf-hd/id479199207?affId=2082346";
				break;	
		
			case StudioHandle.FGOL:
				tempURL = "http://itunes.apple.com/us/app/hungry-shark-part-3/id408369543?mt=8";
				break;	
		}
		
		Application.OpenURL(tempURL);
	}
	
	private Texture2D LogoTextureForStudio(StudioHandle myHandle)
	{
		switch (myHandle)
		{
			case StudioHandle.AGAITCHESON:
				return logoAGAITCHESON;

			case StudioHandle.MOBILEPIE:
				return logoMOBILEPIE;

			case StudioHandle.SPILTMILK:
				return logoSPILTMILK;

			case StudioHandle.BIGPIXEL:
				return logoBIGPIXEL;

			case StudioHandle.INDIESKIES:
				return logoINDIESKIES;	
		
			case StudioHandle.FGOL:
				return logoFGOL;

			default:
				return null;
		}
	}
	
	private void ReturnToTitleScreen()
	{
		Application.LoadLevel(0);
	}
	
	private void PopulateStudioList()
	{
		orderedStudios = new List<int>();
		int nextNumber = 0;
		
		while (orderedStudios.Count < (int)StudioHandle.endOfList)
		{
			nextNumber = Random.Range(0, (int)StudioHandle.endOfList);
			if (!orderedStudios.Contains(nextNumber))
			{
				orderedStudios.Add(nextNumber);
			}
		}
	}
}
