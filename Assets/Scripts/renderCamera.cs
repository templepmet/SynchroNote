//SynchroNote 寺西勇裕 11月25日
using UnityEngine;
using System.Collections;

public class renderCamera : MonoBehaviour {

	// Use this for initialization
	void Start () {
		Camera camera = GetComponent<Camera> ();
		GameObject.Find("InfoCanvas").GetComponent<Canvas> ().worldCamera = camera;
		GameObject.Find("backCanvas").GetComponent<Canvas> ().worldCamera = camera;
		GameObject.Find("frontCanvas").GetComponent<Canvas> ().worldCamera = camera;
	}
}
