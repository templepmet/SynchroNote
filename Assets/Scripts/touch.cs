//SynchroNote 寺西勇裕 11月25日
using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class touch : MonoBehaviour {

	public AudioClip sound;
	AudioSource audioSource;

	public float speed;
	float alpha, red, green, blue;
	bool fadeout = false;

	// Use this for initialization
	void Start () {
		audioSource = GetComponent<AudioSource> ();

		alpha = 0f;
	}
	
	// Update is called once per frame
	void Update () {
		if (fadeout == false && (Input.touchCount > 0 || Input.GetMouseButtonDown(0))) {
			audioSource.Stop ();
			audioSource.PlayOneShot (sound);

			fadeout = true;
		} else if (fadeout == true) {
			if (alpha > 1f) {
				SceneManager.LoadScene ("musicSelect");
			}
			GetComponent<Image> ().color = new Color (0f, 0f, 0f, alpha);
			alpha += speed;
		}
	}
}
