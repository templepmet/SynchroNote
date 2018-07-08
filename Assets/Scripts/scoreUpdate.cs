//SynchroNote 寺西勇裕 11月25日
using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class scoreUpdate : MonoBehaviour {

	Text judgeInfo;
	Text scoreInfo;

	int notesNumber;
	float[] rate = new float[4]{ 1f, 0.7f, 0.3f, 0f };

	// Use this for initialization
	void Start () {
		judgeInfo = GameObject.Find ("judgeInfo").GetComponent<Text> ();
		scoreInfo = GameObject.Find ("scoreInfo").GetComponent<Text> ();
		notesNumber = global.notes [global.id].Count;
	}
	
	public void scoreAdd (int judge) {
		if (judge < 2) {
			global.combo++;
			if (global.maxCombo < global.combo) {
				global.maxCombo = global.combo;
			}
		} else {
			global.combo = 0;
		}
		global.judge [judge]++;

		global.score += 1000000f / notesNumber * rate[judge];

		scoreInfo.text = global.score.ToString("0000000");
		judgeInfo.text = "PERFECT:" + global.judge[0];
		judgeInfo.text += "    GOOD:" + global.judge [1];
		judgeInfo.text += "    BAD:" + global.judge [2];
		judgeInfo.text += "    MISS:" + global.judge[3];
		judgeInfo.text += "    COMBO:" + global.combo;
	}
}
