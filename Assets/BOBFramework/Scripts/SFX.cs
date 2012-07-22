using UnityEngine;
using System.Collections;

public class SFX : MonoBehaviour 
{
	public AudioClip Clip;
	
	public bool Loop;
	
	public bool RandomisePitch = true;
	public float PitchRange = 0.1f;
	
	public float Volume = 1;
}
