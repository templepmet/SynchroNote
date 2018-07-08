//SynchroNote 寺西勇裕 11月25日
using UnityEngine;
using System.Collections;

public class beat : MonoBehaviour {

	public float bpm;

	float scalingTime;
	float duration;
	Vector3 startScale;
	Vector3 endScale;

	// Use this for initialization
	void Start () {
		scalingTime = 60 / bpm;
		startScale = new Vector2 (1.1f, 1.1f);
		endScale = new Vector2 (1, 1);
	}

	// Update is called once per frame
	void Update () {
		duration += Time.deltaTime;
		if (duration > scalingTime) {
			var temp = startScale;
			startScale = endScale;
			endScale = temp;
			duration = 0;
		}

		var rate = duration / scalingTime;
		transform.localScale = Vector3.Lerp(startScale, endScale, rate);
	}
}
