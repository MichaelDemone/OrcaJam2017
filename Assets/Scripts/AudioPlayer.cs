using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public class AudioPlayer : MonoBehaviour
{

	private static AudioPlayer instance;
	private static AudioSource audioSource;
	
	public static void PlayFile(AudioClip clip, float volume)
	{
		audioSource.PlayOneShot(clip, volume);
	}
	
	// Use this for initialization
	void Start ()
	{
		instance = this;
		audioSource = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
