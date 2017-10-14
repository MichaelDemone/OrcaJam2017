using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTowerInteractions : MonoBehaviour
{

	public GameObject Tower;
	public float PlacingDistance;
	
	
	public void PlaceTower(bool facingLeft)
	{
		Vector3 playerPosition = transform.position;
		if (facingLeft) playerPosition.x -= PlacingDistance;
		else playerPosition.x += PlacingDistance;

		playerPosition.z = -2;
		
		Instantiate(Tower, playerPosition, Quaternion.identity);
	}

	public void RetreiveTower(bool facingLeft)
	{
		Vector2 testTowerPos = transform.position;
		testTowerPos.x += facingLeft ? -PlacingDistance : PlacingDistance;
		
		Collider2D[] possibleTowers = Physics2D.OverlapPointAll(testTowerPos);
		foreach (var possibleTower in possibleTowers)
		{
			if (possibleTower.CompareTag("Tower"))
			{
				Destroy(possibleTower);
			}
		}
	}
	
}
