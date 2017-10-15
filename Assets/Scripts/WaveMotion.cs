using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveMotion : MonoBehaviour
{
	private Vector2 initialPos;

	public float TimeOffset = 0;
	public float XMagnitude = 0.1f;
	public float YMagnitude = 0.1f;
	
	// Use this for initialization
	void Start ()
	{
		initialPos = transform.position;
		timeOnCurve = TimeOffset;
	}

	private float timeOnCurve = 0;
	
	// Update is called once per frame
	void Update ()
	{
		timeOnCurve += Time.deltaTime;
		
		float currentYOffset = Mathf.Sin(Mathf.PI * timeOnCurve) * YMagnitude;
		float currentXOffset = Mathf.Sin(Mathf.PI * (timeOnCurve + 0.5f)) * XMagnitude;

		gameObject.transform.position = initialPos + new Vector2(currentXOffset, currentYOffset);

	}
}
