//SynchroNote 寺西勇裕 11月25日
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;

public class global : MonoBehaviour {
	public string[] filename;

	static public int size;
	static public int id = 0;
	static public Sprite[] background;
	static public AudioClip[] musicpreview;
	static public AudioClip[] music;
	static public string[] musicname;
	static public string[] artist;
	static public float[] bpm;
	static public int[] level;
	static public List<List<float[]>> notes = new List<List<float[]>>();

	static public int notesLayer;
	static public float score;
	static public int combo;
	static public int maxCombo;
	static public int[] judge;

	static public float[] user_score;
	static public int[] user_maxCombo;
	static public int[,] user_judge;

	void Awake () {
		size = filename.Length;
		background = new Sprite[size];
		musicpreview = new AudioClip[size];
		music = new AudioClip[size];
		musicname = new string[size];
		artist = new string[size];
		bpm = new float[size];
		level = new int[size];
		user_score = new float[size];
		user_maxCombo = new int[size];
		user_judge = new int[size,4];
		notesLayer = 2999;
		score = 0f;
		combo = 0;
		maxCombo = 0;
		judge = new int[4];

		for (int i = 0; i < size; ++i) {
			background[i] = Resources.Load<Sprite> ("Music/" + filename[i]);
			musicpreview[i] = Resources.Load<AudioClip> ("Music/" + filename[i] + " preview");
			music[i] = Resources.Load<AudioClip> ("Music/" + filename[i]);

			TextAsset csvFile = Resources.Load<TextAsset> ("Music/" + filename[i]);
			List<string[]> csvDatas = new List<string[]> ();
			List<float[]> adder1 = new List<float[]> ();
			StringReader reader = new StringReader (csvFile.text);

			while (reader.Peek () > -1) {
				string line = reader.ReadLine ();
				csvDatas.Add (line.Split (','));
			}

			musicname [i] = csvDatas [0] [0];
			artist [i] = csvDatas [0] [1];
			bpm [i] = float.Parse (csvDatas [0] [2]);
			level [i] = int.Parse (csvDatas [0] [3]);

			for (int j = 1; j < csvDatas.Count; ++j) {
				float[] adder2 = new float[3];
				for (int k = 0; k < 3; ++k) {
					adder2 [k] = float.Parse (csvDatas [j] [k]);
				}
				if (adder2 [0] > 1) {
					break;
				}
				adder1.Add (adder2);
			}
			notes.Add (adder1);
		}
	}
}
