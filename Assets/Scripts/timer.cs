//SynchroNote 寺西勇裕 11月25日
using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class timer : MonoBehaviour {

	Image screen;

	public AudioClip playSound;
	public int waitTime = 60;
	public float speed;

	bool fadeout = false;
	float alpha = 0f;

	void Start () {
		screen = GameObject.Find ("backPanel").GetComponent<Image> ();
		StartCoroutine (CountTimer ());
	}

	IEnumerator CountTimer() {
		while (true) {
			GetComponent<Text> ().text = (waitTime--).ToString ();
			yield return new WaitForSeconds(1f);
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (fadeout == false && waitTime < 1) {
			StopCoroutine (CountTimer ());
			GameObject.Find ("takeOver").GetComponent<AudioSource> ().Stop ();
			GetComponent<AudioSource> ().PlayOneShot (playSound);
			fadeout = true;
		} else if (fadeout == true) {
			if (alpha > 0.7f) {
				SceneManager.LoadScene ("play");
			}
			screen.color = new Color (1f, 1f, 1f, alpha);
			alpha += speed;
		}
	}
}
