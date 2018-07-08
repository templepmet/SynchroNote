//SynchroNote 寺西勇裕 11月25日
using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class note : MonoBehaviour {
	scoreUpdate sU;

	public Sprite perfectSprite;
	public Sprite goodSprite;
	public Sprite badSprite;
	public Sprite missSprite;
	public float perfectJudge;
	public float goodJudge;
	public float badJudge;
	public AudioClip notesSound;

	AudioSource audioSource;
	SpriteRenderer spriteRenderer;

	float turnTime;
	float duration;
	bool expan = true;

	Vector3 startScale;
	Vector3 endScale;

	// Use this for initialization
	void Start () {
		sU = GameObject.Find("scoreUpdate").GetComponent<scoreUpdate> ();

		audioSource = GetComponent<AudioSource> ();
		spriteRenderer = GetComponent<SpriteRenderer> ();
		spriteRenderer.sortingOrder = global.notesLayer--;

		turnTime = (60 * 2) / global.bpm[global.id];

		startScale = transform.localScale;
		endScale = new Vector3 (2, 2, 1);

		StartCoroutine (Judge ());
	}

	IEnumerator Judge() {
		float startTime = Time.time;

		while (true) {
			for (int i = 0; i < Input.touchCount; ++i) {
				Touch touch = Input.GetTouch (i);
				if (touch.phase != TouchPhase.Began) {
					continue;
				}

				Collider2D overlap = Physics2D.OverlapPoint (Camera.main.ScreenToWorldPoint (touch.position));

				if (overlap != null && overlap.gameObject == gameObject) {
					audioSource.PlayOneShot (notesSound);

					var time = Mathf.Abs (turnTime - (Time.time - startTime));
					if (time < perfectJudge) {
						spriteRenderer.sprite = perfectSprite;
						sU.scoreAdd (0);
					} else if (time < goodJudge) {
						spriteRenderer.sprite = goodSprite;
						sU.scoreAdd (1);
					} else if (time < badJudge) {
						spriteRenderer.sprite = badSprite;
						sU.scoreAdd (2);
					} else {
						spriteRenderer.sprite = missSprite;
						sU.scoreAdd (3);
					}

					Destroy (gameObject, turnTime / 8);
					yield break;
				}
			}
			if (Input.GetMouseButtonDown (0)) {
				Collider2D overlap = Physics2D.OverlapPoint (Camera.main.ScreenToWorldPoint (Input.mousePosition));
				if (overlap != null && overlap.gameObject == gameObject) {
					audioSource.PlayOneShot (notesSound);

					var time = Mathf.Abs (turnTime - (Time.time - startTime));
					if (time < perfectJudge) {
						spriteRenderer.sprite = perfectSprite;
						sU.scoreAdd (0);
					} else if (time < goodJudge) {
						spriteRenderer.sprite = goodSprite;
						sU.scoreAdd (1);
					} else if (time < badJudge) {
						spriteRenderer.sprite = badSprite;
						sU.scoreAdd (2);
					} else {
						spriteRenderer.sprite = missSprite;
						sU.scoreAdd (3);
					}

					Destroy (overlap.gameObject, turnTime / 4);
					yield break;
				}
			}

			if (Time.time - startTime > turnTime + badJudge) {
				spriteRenderer.sprite = missSprite;
				sU.scoreAdd (3);
				Destroy (gameObject, turnTime / 8);
				yield break;
			}
			/*
			if (Time.time - startTime > turnTime) {
				audioSource.PlayOneShot (notesSound);
				sU.scoreAdd (3);
				Destroy (gameObject, turnTime / 8);
				yield break;
			}
			*/

			yield return new WaitForSeconds ((float)1 / 1000);
		}
	}

	// Update is called once per frame
	void Update ()
	{
		duration += Time.deltaTime;

		if (expan) {
			if (duration > turnTime) {
				expan = false;
			}
			var rate = duration / turnTime;
			transform.localScale = Vector3.Lerp (startScale, endScale, rate);
		}
	}
}
