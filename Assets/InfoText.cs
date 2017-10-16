using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InfoText : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}


	private IEnumerator textThing = null;
	
	public void SetText(string s)
	{
		StartCoroutine(SetText2(s));
	}

	
	
	IEnumerator SetText2(string s)
	{
		GetComponent<Text>().text = s;
		yield return new WaitForSeconds(6);
		if(GetComponent<Text>().text.Equals(s)) GetComponent<Text>().text = "";
	}
}
