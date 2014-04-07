﻿using UnityEngine;
using System.Collections;

public class PickupSpawner : MonoBehaviour
{
	public GameObject[] pickups;				// Array of pickup prefabs with the bomb pickup first and health second.
	public float pickupDeliveryTime = 5f;		// Delay on delivery.
	public float dropRangeLeft;					// Smallest value of x in world coordinates the delivery can happen at.
	public float dropRangeRight;				// Largest value of x in world coordinates the delivery can happen at.
	public float highHealthThreshold = 75f;		// The health of the player, above which only bomb crates will be delivered.
	public float lowHealthThreshold = 25f;		// The health of the player, below which only health crates will be delivered.

	public float dropRate = .2f;

	private PlayerHealth playerHealth;			// Reference to the PlayerHealth script.


	void Awake ()
	{
		// Setting up the reference.
		playerHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth>();
	}


	void Start ()
	{
		// Start the first delivery.
		//StartCoroutine(DeliverPickup());
	}


	public IEnumerator DeliverPickup()
	{
		yield return new WaitForSeconds(pickupDeliveryTime);

		// Create a random x coordinate for the delivery in the drop range.
		float dropPosX = Random.Range(dropRangeLeft, dropRangeRight);

		// Create a position with the random x coordinate.
		Vector3 dropPos = new Vector3(dropPosX, 15f, 1f);

		// If the player's health is above the high threshold...
		if(playerHealth.health >= highHealthThreshold)
			// ... instantiate a bomb pickup at the drop position.
			Instantiate(pickups[0], dropPos, Quaternion.identity);
		// Otherwise if the player's health is below the low threshold...
		else if(playerHealth.health <= lowHealthThreshold)
			// ... instantiate a health pickup at the drop position.
			Instantiate(pickups[1], dropPos, Quaternion.identity);
		// Otherwise...
		else
		{
			// ... instantiate a random pickup at the drop position.
			int pickupIndex = Random.Range(0, pickups.Length);
			Instantiate(pickups[pickupIndex], dropPos, Quaternion.identity);
		}
	}
	public void DeliverPickup(Vector3 position)
	{	
		float roll = Random.Range(0f, 1f);
		if(roll <= dropRate){
			// If the player's health is above the high threshold...
			if(playerHealth.health >= highHealthThreshold)
				// ... instantiate a bomb pickup at the drop position.
				Instantiate(pickups[0], position, Quaternion.identity);
			// Otherwise if the player's health is below the low threshold...
			else if(playerHealth.health <= lowHealthThreshold)
				// ... instantiate a health pickup at the drop position.
				Instantiate(pickups[1], position, Quaternion.identity);
			// Otherwise...
			else
			{
				// ... instantiate a random pickup at the drop position.
				int pickupIndex = Random.Range(0, pickups.Length);
				Instantiate(pickups[pickupIndex], position, Quaternion.identity);
			}
		}
	}
}
