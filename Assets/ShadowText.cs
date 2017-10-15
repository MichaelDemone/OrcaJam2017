using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShadowText : MonoBehaviour
{

	public Text TextBeingSet;
	public Text OtherText;
	
	
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (TextBeingSet.text != OtherText.text)
		{
			OtherText.text = TextBeingSet.text;
		}
	}
}
