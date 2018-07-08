//SynchroNote 寺西勇裕 11月25日
using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class play : MonoBehaviour {

	Image screen;

	public Sprite playButton;
	public AudioClip pushSound;
	public AudioClip playSound;
	public float speed;

	AudioSource audioSource;

	bool is_on, fadeout;
	float alpha;

	// Use this for initialization
	void Start () {
		screen = GameObject.Find ("backPanel").GetComponent<Image>();
		audioSource = GetComponent<AudioSource> ();
		alpha = 0.5f;
		is_on = false;
		fadeout = false;
	}
	
	// Update is called once per frame
	void Update () {
		if (fadeout == false) {
			if (Input.touchCount > 0) {
				Touch touch = Input.GetTouch (0);
				if (touch.phase != TouchPhase.Began) {
					return;
				}

				Vector2 tapPoint = Camera.main.ScreenToWorldPoint (touch.position);
				if (is_on == false) {
					if (tapPoint.y < 4.5 && tapPoint.y > -4.5) {
						audioSource.PlayOneShot (pushSound);
						GetComponent<SpriteRenderer> ().sprite = playButton;
						screen.color = new Color (0f, 0f, 0f, alpha);
						is_on = true;
					}
				} else {
					Collider2D collition = Physics2D.OverlapPoint (tapPoint);
					if (collition) {
						GameObject.Find ("takeOver").GetComponent<AudioSource> ().Stop ();
						audioSource.PlayOneShot (playSound);
						GetComponent<SpriteRenderer> ().sprite = null;
						alpha = 0f;
						fadeout = true;
					} else {
						audioSource.PlayOneShot (pushSound);
						GetComponent<SpriteRenderer> ().sprite = null;
						screen.color = new Color (0f, 0f, 0f, 0f);
						is_on = false;
					}
				}
			} else if (Input.GetMouseButtonDown (0)) {
				Vector2 tapPoint = Camera.main.ScreenToWorldPoint (Input.mousePosition);
				if (is_on == false) {
					if (tapPoint.y < 4.5 && tapPoint.y > -4.5) {
						audioSource.PlayOneShot (pushSound);
						GetComponent<SpriteRenderer> ().sprite = playButton;
						screen.color = new Color (0f, 0f, 0f, alpha);
						is_on = true;
					}
				} else {
					Collider2D collition = Physics2D.OverlapPoint (tapPoint);
					if (collition) {
						GameObject.Find ("takeOver").GetComponent<AudioSource> ().Stop ();
						audioSource.PlayOneShot (playSound);
						GetComponent<SpriteRenderer> ().sprite = null;
						alpha = 0f;
						fadeout = true;
					} else {
						audioSource.PlayOneShot (pushSound);
						GetComponent<SpriteRenderer> ().sprite = null;
						screen.color = new Color (0f, 0f, 0f, 0f);
						is_on = false;
					}
				}
			}
		} else {
			screen.color = new Color (1f, 1f, 1f, alpha);
			alpha += speed;

			if (alpha > 0.7f) {
				SceneManager.LoadScene ("play");
			}
		}
	}
}