//SynchroNote 寺西勇裕 11月25日
using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using System.IO;
using UnityEngine.UI;

public class resultTimer : MonoBehaviour {

	Image screen;
	AudioSource audioSource;

	public int waitTime = 30;
	public float speed;
	public AudioClip sound;
	public AudioClip seeYou;

	bool fadeout;
	float alpha = 0f;

	void Start () {
		StartCoroutine (CountTimer());
		screen = GameObject.Find ("frontPanel").GetComponent<Image> ();
		audioSource = GetComponent<AudioSource> ();

		fadeout = false;
	}

	IEnumerator CountTimer() {
		while (waitTime > 0) {
			GetComponent<Text> ().text = (waitTime--).ToString ();
			yield return new WaitForSeconds(1f);
		}
	}

	public void skipPush() {
		waitTime -= 2;
		GetComponent<Text> ().text = waitTime.ToString ();
		audioSource.PlayOneShot (sound);
	}

	// Update is called once per frame
	void Update () {
		if (fadeout == false && waitTime < 1) {
			audioSource.Stop ();
			audioSource.PlayOneShot (seeYou);
			Destroy(GameObject.Find("buttonCanvas"));
			fadeout = true;
		} else if (fadeout) {
			GetComponent<Text> ().text = "0";

			if (alpha > 1f) {
				global.id = 0;
				Destroy (GameObject.Find ("takeOver"));
				Destroy(GameObject.Find("personalCanvas"));

				SceneManager.LoadScene ("start");
			}

			screen.color = new Color (0f, 0f, 0f, alpha);
			alpha += speed;
		}
	}
}
