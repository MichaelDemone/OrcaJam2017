using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Scoreboard : MonoBehaviour {

	public static int score
	{
		get { return _score; }
		set
		{
			_score = value;
			Instance.SetNumTowers();
		}
	}
	
	private static int _score = 0;

	public PlayerTowerInteractions pti;

	private static Scoreboard Instance;

	public InfoText InfoText;
	
	// Use this for initialization
	void Start () 
	{
		Instance = this;
		_score = 0;
		SetNumTowers();
	}

	void SetNumTowers()
    {
	    if (pti.MaxNumberOfTowers < 5)
	    {
		    int towersBefore = pti.MaxNumberOfTowers;
		    pti.MaxNumberOfTowers = Mathf.FloorToInt((_score - 10f) / 30f) + 1;
		    if (towersBefore != pti.MaxNumberOfTowers)
		    {
			    InfoText.SetText("You gained a new tower! Don't forget to place it.");
		    }
	    }
	    else
	    {
		    pti.MaxNumberOfTowers = 5;
	    }
		
	}
	
	// Update is called once per frame
	void Update () {
        //print(score);
	}
}
