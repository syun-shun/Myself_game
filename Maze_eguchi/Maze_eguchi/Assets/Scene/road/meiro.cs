using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class meiro : MonoBehaviour
{
	//迷路の大きさ(5以上)
	public static int max = 25;

	//壁のオブジェクトの指定
	public GameObject wall;

	//スタートオブジェクトの指定
	public GameObject start;

	//ゴールオブジェクトの指定
	public GameObject goal;

	//迷路の配列　0：壁　1：道
	public static int[,] walls;

	//スタートの位置
	public static int[] startPos;

	//ゴールの位置
	public static int[] goalPos;

	// Use this for initialization
	void Start ()
	{
		//壁の配列生成(今中身は全て0)つまり全て道
		walls = new int[max, max];
		MakeWall (max, max);
		for (int i = 0; i < max; i++) {
			for (int j = 0; j < max; j++) {
				if (walls [i, j] == 0) {
					GameObject wallObj = Instantiate (wall, new Vector3 (i * 10 + 100, j * 10 + 100, 0), Quaternion.identity) as GameObject;
					wallObj.transform.parent = transform;
				}
			}
		}

		//スタートの位置の取得
		startPos = Position ();
		while (walls [startPos [0], startPos [1]] == 0) {
			startPos = Position ();
		}

		GameObject startObj = Instantiate (start, new Vector3 (startPos [0] * 10 + 100, startPos [1] * 10 + 100, 0), Quaternion.identity) as GameObject;
		startObj.transform.parent = transform;

		//ゴールの位置の取得
		goalPos = Position ();
		while (walls [goalPos [0], goalPos [1]] == 0 || startPos == goalPos) {
			goalPos = Position ();
		}
		GameObject goalObj = Instantiate (goal, new Vector3 (goalPos [0] * 10 + 100, goalPos [1] * 10 + 100, 0), Quaternion.identity) as GameObject;
		goalObj.transform.parent = transform;
	}

	int[] Position ()
	{
		int ran = (int)Random.Range (1, max - 1);
		int ran1 = (int)Random.Range (1, max - 1);
		int[] position = { ran, ran1 };
		return(position);
	}

	void MakeWall (int width, int height)
	{
		//外周と内側を部分的に壁を配置
		for (int i = 0; i < width; i++) {
			for (int j = 0; j < height; j++) {
				if (i == 0 || i == width - 1 || j == 0 || j == height - 1) {
					walls [i, j] = 0;
				} else if (i % 2 == 0 && j % 2 == 0) {
					walls [i, j] = 0;
				} else {
					walls [i, j] = 1;
				}
			}
		}

		//壁を倒していく
		for (int x = 2; x < width - 1; x += 2) {
			for (int y = 2; y < height - 1; y += 2) {
				while (true) {
					int ran;
					if (y == height - 3) {
						ran = (int)Random.Range (0, 4);
					} else {
						ran = (int)Random.Range (0, 3);
					}
					int Wallx = x;
					int Wally = y;

					switch (ran) {
					case 0:
						Wallx++;
						break;
					case 1:
						Wally--;
						break;
					case 2:
						Wallx--;
						break;
					case 3:
						Wally++;
						break;
					}
					if (walls [Wallx, Wally] != 0) {
						walls [Wallx, Wally] = 0;
						break;
					}
				}
			}
		}
	}

	// Update is called once per frame
	void Update ()
	{

	}
}
