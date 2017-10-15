using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTowerInteractions : MonoBehaviour
{

	public int MaxNumberOfTowers = 10;
	public int NumberOfTowersPlaced = 0;
	
	public GameObject Tower;
	public Transform TowerPlacementLocation;
	
	private bool towerClicked = false;
	
	void Update ()
	{
		if (Time.timeScale == 0) return;
		
		if (Input.GetAxis("PlaceTower") != 0 && !towerClicked)
		{
			towerClicked = true;
			PlaceTower(transform.localScale.x < 0);
		}
		else if (Input.GetAxis("PlaceTower") == 0)
		{
			towerClicked = false;
		}

		if (Input.GetAxis("RemoveTower") != 0)
		{
			towerClicked = true;
			RetreiveTower(transform.localScale.x < 0);
		}
	}
	
	
	public void PlaceTower(bool facingLeft)
	{
		if (NumberOfTowersPlaced == MaxNumberOfTowers) return;

		NumberOfTowersPlaced++;
		Vector3 playerPosition = TowerPlacementLocation.position;
		playerPosition.z = -3;

		Instantiate(Tower, playerPosition, Quaternion.identity);
	}

	public void RetreiveTower(bool facingLeft)
	{
		
		Vector3 testTowerPos = TowerPlacementLocation.position;
		testTowerPos.z = -3;
		
		Collider2D[] possibleTowers = Physics2D.OverlapPointAll(testTowerPos);
		foreach (var possibleTower in possibleTowers)
		{
			if (possibleTower.CompareTag("Tower"))
			{
				Destroy(possibleTower.gameObject);
				NumberOfTowersPlaced--;
			}
		}
	}
	
}
