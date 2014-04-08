﻿using UnityEngine;
using System.Collections;

public class EnemyRocket : MonoBehaviour {
	public GameObject explosion;		// Prefab of explosion effect.
	public float bulletRange = 1.2f;
	
	void Start () 
	{
		// Destroy the rocket after 2 seconds if it doesn't get destroyed before then.
		Destroy(gameObject, bulletRange);
	}
	
	
	void OnExplode()
	{
		// Create a quaternion with a random rotation in the z-axis.
		Quaternion randomRotation = Quaternion.Euler(0f, 0f, Random.Range(0f, 360f));
		
		// Instantiate the explosion where the rocket is with the random rotation.
		if(explosion) Instantiate(explosion, transform.position, randomRotation);
	}
	
	void OnTriggerEnter2D (Collider2D col) 
	{
		// If it hits an enemy...
		if(col.tag == "Player")
		{
			// Call the explosion instantiation.
			OnExplode();
			
			// Destroy the rocket.
			Destroy (gameObject);
		}
		// Otherwise if it hits a bomb crate...
		else if(col.tag == "BombPickup")
		{
			// ... find the Bomb script and call the Explode function.
			col.gameObject.GetComponent<Bomb>().Explode();
			
			// Destroy the bomb crate.
			Destroy (col.transform.root.gameObject);
			
			// Destroy the rocket.
			Destroy (gameObject);
		}

		else if(col.tag != "Enemy" && col.tag != "EnemyFlyer" && col.tag != "EnemyGunner" && col.tag != "Gun" && col.tag != "Ground")
		{
			// ... find the Bomb script and call the Explode function.
			OnExplode();
			
			// Destroy the rocket.
			Destroy (gameObject);
		}
	}
}
