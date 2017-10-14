using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestSideScrollingEnemy : MonoBehaviour
{

	public Vector3 Speed;
	
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
	{
		transform.position += Speed;
	}

	public void Highlight()
	{
		GetComponent<SpriteRenderer>().color = Color.black;
	}

	public void UnHighlight()
	{
		GetComponent<SpriteRenderer>().color = Color.white;
	}

	public void Hit()
	{
		GetComponent<SpriteRenderer>().color = Color.red;
	}
}
