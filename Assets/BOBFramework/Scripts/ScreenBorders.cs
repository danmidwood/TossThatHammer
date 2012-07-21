using UnityEngine;
using System.Collections;

public class ScreenBorders : MonoBehaviour {
	
	public GUIStyle blackBorderStyle;
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void OnGUI()
	{
		float borderThickness = 0;
		
		//draw the borders on the side!!
		if (GUIManager.activeRect.y == 0)
		{
			borderThickness = GUIManager.offsetVector.x;//GUIManager.activeRect.x;
			
			GUI.Box(new Rect(0, 0, borderThickness, Screen.height), "", blackBorderStyle);
			GUI.Box(new Rect(Screen.width - borderThickness, 
				0, borderThickness, Screen.height), "", blackBorderStyle);
		}
		else
		{
			//draw borders on the top!
			borderThickness = GUIManager.offsetVector.y;//GUIManager.activeRect.y;
			
			GUI.Box(new Rect(0, 0, Screen.width, borderThickness), "", blackBorderStyle);
			GUI.Box(new Rect(0, Screen.height - borderThickness, 
				Screen.width, borderThickness), "", blackBorderStyle);

		}
	}
}
