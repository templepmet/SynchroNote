//SynchroNote 寺西勇裕 11月25日
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class notesGenerator : MonoBehaviour {

	public GameObject normalNote;
	public GameObject airNote;
	GameObject barObject;

	List<float[]> notes;
	float turnTime;
	int notesNumber;

	// Use this for initialization
	void Start () {
		barObject = GameObject.Find ("bar");

		notes = global.notes[global.id];
		turnTime = (60 * 2) / global.bpm[global.id];
		notesNumber = notes.Count;
		StartCoroutine (generate ());
	}

	IEnumerator generate() {
		int suffix = 0;
		while (bar.play == false) {
			yield return new WaitForSeconds ((float)1 / 1000);
		}

		float startTime = Time.time;
		while (true) {
			while (suffix < notesNumber && Time.time > startTime + turnTime * (notes [suffix] [1] / 16)) {
				if (notes [suffix] [0] == 0) {
					Instantiate (normalNote, new Vector2 (notes [suffix] [2], -barObject.transform.position.y), Quaternion.identity);
				} else {
					Instantiate (airNote, new Vector2 (notes [suffix] [2], -barObject.transform.position.y), Quaternion.identity);
				}
				suffix++;
			}

			yield return new WaitForSeconds ((float)1 / 1000);
		}
	}
}
