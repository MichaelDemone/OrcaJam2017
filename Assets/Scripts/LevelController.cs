using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelController : MonoBehaviour
{

	public int TotalNumberOfEnemies;
	public int CurrentNumberOfEnemies;
	public float CurrentEnemyHealth;
	
	public int NumberOfTowersAvailable;
	public int NumberOfTowersLeft;
	public int NumberOfTowersPlaced;
	
	
	// Use this for initialization
	void Start () 
	{
		
	}
	
	// Update is called once per frame
	void Update () 
	{
		
	}

	public void TowerPlaced()
	{
		NumberOfTowersAvailable--;
		NumberOfTowersPlaced++;
	}
}
