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
        int towersBefore = pti.MaxNumberOfTowers;
        if (score < 10) { pti.MaxNumberOfTowers = 0; }
        else if (score < 30) { pti.MaxNumberOfTowers = 1; }
        else if (score < 60) { pti.MaxNumberOfTowers = 2; }
        else if (score < 120) { pti.MaxNumberOfTowers = 3; }
        else if (score > 240) { pti.MaxNumberOfTowers = 4; }
        else { pti.MaxNumberOfTowers = 5; }


        if (towersBefore != pti.MaxNumberOfTowers)
        {
            InfoText.SetText("You gained a new tower! Don't forget to place it.");
        }
    {
	
	// Update is called once per frame
	void Update () {
        //print(score);
	}
}
