using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AudioManager : MonoBehaviour 
{
	AudioClip TransitionMusic, GameMusic;
	
	public AudioSource MusicSource, SfxSource1, SfxSource2;
	
	// Use this for initialization
	void Start()
	{
		TransitionMusic = (AudioClip)Resources.Load("transition_draft_1", typeof(AudioClip));
	}
	
	public void PlayMusic(bool isGameMusic, float speed)
	{
		if(isGameMusic)
		{
			MusicSource.loop = true;
			MusicSource.clip = GameMusic;
		}
		else
		{
			MusicSource.loop = false;
			MusicSource.clip = TransitionMusic;
		}
		MusicSource.pitch = speed;
		MusicSource.Play();
	}
	
	public void PlaySFX(SFX sfx)
	{
		if(!SfxSource1.isPlaying)
		{
			SfxSource1.clip = sfx.Clip;
		
			if(sfx.Loop)
				SfxSource1.loop = true;
			else
				SfxSource1.loop = false;
			
			if(sfx.RandomisePitch)
				SfxSource1.pitch = 1.0f + Random.Range(-sfx.PitchRange, sfx.PitchRange);
			else
				SfxSource1.pitch = 1.0f;

			SfxSource1.Play();
		}
		else
		{
			SfxSource2.clip = sfx.Clip;
			
			if(sfx.Loop)
				SfxSource2.loop = true;
			else
				SfxSource2.loop = false;
			
			if(sfx.RandomisePitch)
				SfxSource2.pitch = 1.0f + Random.Range(-sfx.PitchRange, sfx.PitchRange);
			else
				SfxSource2.pitch = 1.0f;
			
			SfxSource2.Play();
		}
	}
	
	public void PauseAll()
	{
		MusicSource.pitch = 0;
		SfxSource1.pitch = 0;
		SfxSource2.pitch = 0;
	}
	
	public void ResumeAll()
	{
		MusicSource.pitch = 1;
		SfxSource1.pitch = 1;
		SfxSource2.pitch = 1;
	}
}
