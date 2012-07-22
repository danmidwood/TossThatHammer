using UnityEngine;
using System.Collections;

public class GUIManager : MonoBehaviour {
	
	public static float intendedWidth = 960;
	public static float intendedHeight = 640;
	
	public static Matrix4x4 scaleMatrix;
	public static Vector2 offsetVector;
	public static Rect activeRect;
	public static Matrix4x4 originalScaleMatrix;
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	public static void CalculateScaleProperties()
	{
		originalScaleMatrix = GUI.matrix;
		
		//Debug.Log("Screen: " + Screen.width.ToString() + ", " + Screen.height.ToString());
		
		//Debug.Log("X: " + (Screen.width / intendedWidth).ToString());
		//Debug.Log("Y: " + (Screen.height / intendedHeight).ToString());
		
		float xScale = Screen.width / intendedWidth;
		float yScale = Screen.height / intendedHeight;
		
		float scale = Mathf.Min(xScale, yScale);
		
		//Debug.Log("SCALE: " + scale.ToString());
		
		Vector3 scaleVec = new Vector3(scale,
			scale,
			1);
		scaleMatrix = Matrix4x4.TRS(Vector3.zero, Quaternion.identity, scaleVec);
		
		
		
		offsetVector = new Vector2();
		if (xScale != yScale)
		{
			if (scale == xScale)
			{
				offsetVector.y = ((float)Screen.height) - (scale * intendedHeight);
				offsetVector.y *= 0.5f;
			}
			else
			{
				offsetVector.x = ((float)Screen.width) - (scale * intendedWidth);
				offsetVector.x *= 0.5f;
			}
		}
		
		//if (offsetVector.x < 1) {offsetVector.x = 0;}
		//if (offsetVector.y < 1) {offsetVector.y = 0;}
		
		activeRect = new Rect(offsetVector.x / scale, offsetVector.y / scale, intendedWidth, intendedHeight);
		
		//Debug.Log("Offset: " + offsetVector.x.ToString() + ", " + offsetVector.y.ToString());
	}
	
	public static void SetUpGUI()
	{

		GUI.matrix = GUIManager.scaleMatrix;
		
		GUI.BeginGroup(activeRect);	
		//Debug.Log("Active rect = " + activeRect.ToString());
		
		GUI.matrix = GUIManager.scaleMatrix;
		
	}
	
	public static void ResetGUI()
	{
		GUI.matrix = originalScaleMatrix;
		
		GUI.EndGroup();
	}
}
