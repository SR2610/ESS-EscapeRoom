﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour {

	public GameObject SFXPrefab; //Prefab for the SFX
	public float DefaultRange = 5F;
	public List<string> SFXNames;
	public List<AudioClip> SFXClips;
	public Dictionary<string, AudioClip> SFXDict = new Dictionary<string, AudioClip>(); //Dictionary associating the name of the SFX with the relevant file

	public AudioClip FirstSong;
	public AudioClip LastSong;


	void Start()
	{
		for (int i = 0; i < SFXNames.Count; i++) //Adds to dictionary
		{
			SFXDict.Add(SFXNames[i], SFXClips[i]);
		}
	}

	public void PlaySFX(string Name, Transform Location) //Plays sound at location
	{
		PlaySFX(Name, Location, DefaultRange);

	}

	public void PlaySFX(string Name, Transform Location,float Range) //Plays sound at location
	{
		if (SFXDict.ContainsKey(Name))
		{
			GameObject SFX = Instantiate(SFXPrefab, Location);
			SFX.GetComponent<AudioSource>().clip = SFXDict[Name];
			SFX.GetComponent<AudioSource>().maxDistance = Range;
			SFX.GetComponent<AudioSource>().Play();
			Destroy(SFX, SFX.GetComponent<AudioSource>().clip.length); //Cleans up when clip is finished
		}
		else
			Debug.LogWarning("Tried to Play Sound that Didn't Exist");
	}

	public void PlaySFX(string Name) //Plays SFX at the manager
	{
		PlaySFX(Name, gameObject.transform,100);
	}

	public void PlaySecondTrack()
	{
		AudioSource AS = GetComponent<AudioSource>();
		AS.Stop();
		AS.clip = LastSong;
		AS.Play();

	}
}
