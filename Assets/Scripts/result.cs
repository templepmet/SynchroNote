//SynchroNote 寺西勇裕 11月25日
using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class result : MonoBehaviour {
	
	// Use this for initialization
	void Start () {
		GameObject.Find ("scoreResult").GetComponent<Text> ().text = global.score.ToString("0000000");
		GameObject.Find ("comboResult").GetComponent<Text> ().text = "MAXCOMBO:" + global.maxCombo;
		GameObject.Find ("perfectResult").GetComponent<Text> ().text = "PERFECT:" + global.judge[0];
		GameObject.Find ("goodResult").GetComponent<Text> ().text = "GOOD:" + global.judge[1];
		GameObject.Find ("badResult").GetComponent<Text> ().text = "BAD:" + global.judge[2];
		GameObject.Find ("missResult").GetComponent<Text> ().text = "MISS:" + global.judge[3];
	}
}
