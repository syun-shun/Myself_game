using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class block : MonoBehaviour
{
	public move move;
	public GameObject cube;
	public GameObject start;
	public GameObject finish;
	public GameObject parentObject;
	public static int ran;
	public static int ran2;
	public static int ran3;
	public static int ran4;
	public static int[,] array = new int[22, 22];
	// Use this for initialization

	void Awake ()
	{
		ran = (int)Random.Range (1, 20);   
		ran2 = (int)Random.Range (1, 20);
		ran3 = (int)Random.Range (1, 20);   
		ran4 = (int)Random.Range (1, 20);
		if (ran > ran3) {
			int a = ran3;
			ran3 = ran;
			ran = a;
		}
		if (ran2 > ran4) {
			int a = ran4;
			ran4 = ran2;
			ran2 = a;
		}
	
		
		for (int i = 0; i < (array.Length) / array.GetLength (0); i++) {
			for (int j = 0; j < array.GetLength (0); j++) {
				array [i, j] = 1;

				//スタート位置
				if (i == ran && j == ran2) {
					array [i, j] = 0;
					Instantiate (start);
					GameObject childObject = Instantiate (start, new Vector2 (i * 10 + 100, j * 10 + 100), Quaternion.identity) as GameObject;
					childObject.transform.parent = parentObject.transform;
				}

				//ゴール位置
				if (i == ran3 && j == ran4) {
					array [i, j] = 0;
					Instantiate (finish);
					GameObject childObject = Instantiate (finish, new Vector2 (i * 10 + 100, j * 10 + 100), Quaternion.identity) as GameObject;
					childObject.transform.parent = parentObject.transform;
				}
			}
		}

		int n = ran;
		int m = ran2;

		//スタートからゴールまで
		while (n < ran3 || m < ran4) {
			int a = Random.Range (0, 4);
			//print (a);
			if (a == 0 && n < ran3) {
				array [n + 1, m] = 0;
				n += 1;
			}
			if (a == 1 && m < ran4) {
				array [n, m + 1] = 0;
				m += 1;
			}
			if (a == 2 && n >= 2) {
				array [n - 1, m] = 0;
				n -= 1;
			}
			if (a == 3 && m >= 2) {
				array [n, m - 1] = 0;
				m -= 1;
			}
		}

		//ブロック配置 0なら通路　1なら壁
		for (int i = 0; i < (array.Length) / array.GetLength (0); i++) {
			for (int j = 0; j < array.GetLength (0); j++) {
				if (array [i, j] == 1) {
					Instantiate (cube);
					GameObject childObject = Instantiate (cube, new Vector2 (i * 10 + 100, j * 10 + 100), Quaternion.identity) as GameObject;
					childObject.transform.parent = parentObject.transform;
				}
			}
		}
	}
	
	// Update is called once per frame
	void Update ()
	{
	}
}
