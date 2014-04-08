﻿using UnityEngine;
using System.Collections;

public class EndLevel : MonoBehaviour {

	public AudioClip finishClip;
	private bool finished = false;

	void OnTriggerEnter2D (Collider2D other)
	{
		// If the player enters the trigger zone...
		if(other.tag == "Player" && !finished)
		{
			StartCoroutine(Finish());
		}
	}

	IEnumerator Finish ()
	{
		finished = true;
		AudioSource.PlayClipAtPoint(finishClip, transform.position);
		GameObject.Find ("hero").GetComponent<PlayerControl> ().enabled = false;
		yield return new WaitForSeconds (3);

		if (PlayerControl.level < (Application.levelCount-2))
						PlayerControl.level = Application.loadedLevel+1;

		Application.LoadLevel (Application.loadedLevel + 1);
	}
}