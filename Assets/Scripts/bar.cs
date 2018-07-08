//SynchroNote 寺西勇裕 11月25日
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class bar : MonoBehaviour {

	public static bool play;

	public AudioClip tempoSound;
	AudioSource audioSource;

	List<float[]> notes;
	float duration;
	float musicTime;

	// Use this for initialization
	void Start () {
		play = false;
		audioSource = GetComponent<AudioSource> ();
		duration = 0;
		musicTime = global.music [global.id].length;
		StartCoroutine (cycleBar ());
	}

	IEnumerator cycleBar() {
		Vector3 startPosition = new Vector3(0f, -4f, 0f);
		Vector3 endPosition = new Vector3(0f, 4f, 0f);
		int turnCnt = 0;
		float elapsedTime = Time.time;
		float turnTime = (60 * 2) / global.bpm[global.id];

		while(true){
			if (Time.time > elapsedTime) {
				if (turnCnt < 4) {
					audioSource.PlayOneShot (tempoSound);
				} else if (turnCnt == 4) {
					audioSource.clip = global.music [global.id];
					audioSource.Play ();
					duration = 0;
				}
				if (turnCnt == 3) {
					play = true;
				}

				var temp = startPosition;
				startPosition = endPosition;
				endPosition = temp;

				turnCnt++;
				elapsedTime = Time.time  + turnTime;
			}

			float rate = (elapsedTime - Time.time) / turnTime;
			transform.position = Vector3.Lerp (startPosition, endPosition, rate);
			yield return new WaitForSeconds((float)1/1000);
		}
	}
	
	// Update is called once per frame
	void Update () {
		duration += Time.deltaTime;
		if (duration > musicTime) {
			SceneManager.LoadScene ("result");
		}
	}
}
