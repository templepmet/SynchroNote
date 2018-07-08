//SynchroNote 寺西勇裕 11月25日
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using System.IO;
using UnityEngine.UI;

public class takeOver : MonoBehaviour {
	public SerialHandler serialHandler;
	public static string serialMessage;

	public AudioClip pushSound;

	Text musicText;
	Text judgeText;
	Text scoreText;
	Text levelText;
	SpriteRenderer backGround;
	AudioSource audioSource;

	// Use this for initialization
	void Start () {
		DontDestroyOnLoad (gameObject);

		musicText = GameObject.Find ("musicInfo").GetComponent<Text> ();
		judgeText = GameObject.Find ("judgeInfo").GetComponent<Text> ();
		scoreText = GameObject.Find ("scoreInfo").GetComponent<Text> ();
		levelText = GameObject.Find ("levelInfo").GetComponent<Text> ();
		backGround = GameObject.Find ("backGround").GetComponent<SpriteRenderer> ();
		audioSource = GetComponent<AudioSource> ();

		serialHandler.OnDataReceived += OnDataReceived;

		selectUpdate ();
	}

	void OnDataReceived(string str) {
		serialMessage = str;
		Debug.Log (str);
	}

	void selectUpdate() {
		backGround.sprite = global.background[global.id];
		backGround.transform.localScale = new Vector2 (18 / global.background [global.id].bounds.size.x, 10 / global.background [global.id].bounds.size.y);

		musicText.text = "Music:" + global.musicname [global.id];
		musicText.text += "     Artist:" + global.artist [global.id];
		musicText.text += "     Bpm:" + global.bpm[global.id];
		scoreText.text = global.user_score[global.id].ToString("0000000");
		judgeText.text = "PERFECT:" + global.user_judge[global.id, 0];
		judgeText.text += "    GOOD:" + global.user_judge [global.id, 1];
		judgeText.text += "    BAD:" + global.user_judge [global.id, 2];
		judgeText.text += "    MISS:" + global.user_judge[global.id, 3];
		judgeText.text += "    COMBO:" + global.user_maxCombo[global.id];
		levelText.text = global.level[global.id].ToString();

		audioSource.Stop ();
		audioSource.clip = global.musicpreview [global.id];
		audioSource.Play ();
	}

	public void nextPush() {
		global.id = (global.id + 1) % global.size;
		selectUpdate ();
		audioSource.PlayOneShot (pushSound);
	}

	public void prevPush() {
		global.id = (global.id + global.size - 1) % global.size;
		selectUpdate ();
		audioSource.PlayOneShot (pushSound);
	}

	public void exitPush() {
		audioSource.PlayOneShot (pushSound);
		Destroy (gameObject);
		SceneManager.LoadScene ("start");
	}
}
