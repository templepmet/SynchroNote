//SynchroNote 寺西勇裕 11月25日
using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class air : MonoBehaviour {
	scoreUpdate sU;

	public Sprite perfectSprite;
	public Sprite missSprite;
	public float perfectJudge;
	public AudioClip notesSound;
	public float width;

	AudioSource audioSource;
	SpriteRenderer spriteRenderer;

	float turnTime;
	float duration;
	bool expan = true;

	Vector3 startScale;
	Vector3 endScale;

	// Use this for initialization
	void Start () {
		sU = GameObject.Find ("scoreUpdate").GetComponent<scoreUpdate> ();

		audioSource = GetComponent<AudioSource> ();
		spriteRenderer = GetComponent<SpriteRenderer> ();
		spriteRenderer.sortingOrder = global.notesLayer--;

		turnTime = (60 * 2) / global.bpm[global.id];

		startScale = transform.localScale;
		endScale = new Vector3 (2, 2, 1);

		StartCoroutine (Judge ());
	}

	IEnumerator Judge () {
		float startTime = Time.time;
		float distance;
		float standard = width * 3 / 4;
		bool is_On = false;

		while (Time.time - startTime < turnTime - perfectJudge) {
			yield return new WaitForSeconds ((float)1 / 1000);
		}

		while (true) {
			if (Time.time - startTime > turnTime + perfectJudge) {
				spriteRenderer.sprite = missSprite;
				sU.scoreAdd (3);
				Destroy (gameObject, turnTime / 8);
				yield break;
			}
			/*
			if (Time.time - startTime > turnTime) {
				audioSource .PlayOneShot (notesSound);
				spriteRenderer.sprite = missSprite;
				sU.scoreAdd (3);
				Destroy (gameObject, turnTime / 8);
				yield break;
			}
			*/

			if (takeOver.serialMessage == null) {
				yield return new WaitForSeconds ((float)1 / 1000);
				continue;
			}

			distance = float.Parse (takeOver.serialMessage);
			if (is_On == false && distance < standard) {
				is_On = true;
			} else if (is_On == true && distance > standard) {
				spriteRenderer.sprite = perfectSprite;
				audioSource.PlayOneShot (notesSound);
				sU.scoreAdd (0);
				Destroy (gameObject, turnTime / 4);
				yield break;
			}

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
