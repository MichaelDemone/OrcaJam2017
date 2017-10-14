using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTowerInteractions : MonoBehaviour
{

	public GameObject Tower;
	public Transform TowerPlacementLocation;
	
	
	public void PlaceTower(bool facingLeft)
	{
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
			}
		}
	}
	
}
