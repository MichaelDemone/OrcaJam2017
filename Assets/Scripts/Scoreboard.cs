using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
                pti.MaxNumberOfTowers = Mathf.FloorToInt((_score - 10f) / 30f) + 1;
            } 
            else{ pti.MaxNumberOfTowers = 5; }
		
	}
	
	// Update is called once per frame
	void Update () {
        //print(score);
	}
}
