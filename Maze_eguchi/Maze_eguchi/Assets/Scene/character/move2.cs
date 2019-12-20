using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class move2 : MonoBehaviour
{

	//迷路から参照する
	public meiro meiro;
	public GameObject character;
	public int[] start;
	public static int[] wallpos = new int[2];

	private int[,] wall;
	private int[] goal;
	private int[] pos = new int[2];
	private int[] canmove;
	//一個前の位置
	private int[] prepos = new int[2];

	// Use this for initialization
	void Start ()
	{
		//壁の位置の取得 (0：壁　1：道)
		wall = new int[meiro.max, meiro.max];
		for (int i = 0; i < meiro.max; i++) {
			for (int j = 0; j < meiro.max; j++) {
				wall [i, j] = meiro.walls [i, j];
			}
		}

		//スタート位置の取得
		start = new int[]{ meiro.startPos [0], meiro.startPos [1] };

		//ゴール位置の取得
		goal = new int[]{ meiro.goalPos [0], meiro.goalPos [1] };

		pos [0] = start [0] * 10 + 100;
		pos [1] = start [1] * 10 + 100;

		wallpos [0] = start [0];
		wallpos [1] = start [1];

		prepos [0] = wallpos [0];
		prepos [1] = wallpos [1];

		character.transform.position = new Vector2 (pos [0], pos [1]);
	}
	
	// yだけ上に進む
	void GoUp (int y)
	{
		if (Input.GetKeyDown (KeyCode.UpArrow)) {
			if (wall [wallpos [0], wallpos [1] + 1] == 1) {
				pos [1] += y;
			}
			prepos [0] = wallpos [0];
			prepos [1] = wallpos [1];
			character.transform.position = new Vector2 (pos [0], pos [1]);
			pos [0] = (int)character.transform.position.x;
			pos [1] = (int)character.transform.position.y;
			wallpos [0] = (int)(pos [0] - 100) / 10;
			wallpos [1] = (int)(pos [1] - 100) / 10;
		}
	}

	// yだけ下に進む
	void GoDown (int y)
	{
		if (Input.GetKeyDown (KeyCode.DownArrow)) {
			if (wall [wallpos [0], wallpos [1] - 1] == 1) {
				pos [1] -= y;
			}
			prepos [0] = wallpos [0];
			prepos [1] = wallpos [1];
			character.transform.position = new Vector2 (pos [0], pos [1]);
			pos [0] = (int)character.transform.position.x;
			pos [1] = (int)character.transform.position.y;
			wallpos [0] = (int)(pos [0] - 100) / 10;
			wallpos [1] = (int)(pos [1] - 100) / 10;
		}
	}

	// xだけ右に進む
	void GoRight (int x)
	{
		if (Input.GetKeyDown (KeyCode.RightArrow)) {
			if (wall [wallpos [0] + 1, wallpos [1]] == 1) {
				pos [0] += x;
			}
			prepos [0] = wallpos [0];
			prepos [1] = wallpos [1];
			character.transform.position = new Vector2 (pos [0], pos [1]);
			pos [0] = (int)character.transform.position.x;
			pos [1] = (int)character.transform.position.y;
			wallpos [0] = (int)(pos [0] - 100) / 10;
			wallpos [1] = (int)(pos [1] - 100) / 10;
		}
	}

	// xだけ左に進む
	void GoLeft (int x)
	{
		if (Input.GetKeyDown (KeyCode.LeftArrow)) {
			if (wall [wallpos [0] - 1, wallpos [1]] == 1) {
				pos [0] -= x;
			}
			prepos [0] = wallpos [0];
			prepos [1] = wallpos [1];
			character.transform.position = new Vector2 (pos [0], pos [1]);
			pos [0] = (int)character.transform.position.x;
			pos [1] = (int)character.transform.position.y;
			wallpos [0] = (int)(pos [0] - 100) / 10;
			wallpos [1] = (int)(pos [1] - 100) / 10;
		}
	}

	void GoUpUp (int y)
	{
		if (wall [wallpos [0], wallpos [1] + 1] == 1) {
			pos [1] += y;
		}
		prepos [0] = wallpos [0];
		prepos [1] = wallpos [1];
		character.transform.position = new Vector2 (pos [0], pos [1]);
		pos [0] = (int)character.transform.position.x;
		pos [1] = (int)character.transform.position.y;
		wallpos [0] = (int)(pos [0] - 100) / 10;
		wallpos [1] = (int)(pos [1] - 100) / 10;
	}

	void GoDownDown (int y)
	{
		if (wall [wallpos [0], wallpos [1] - 1] == 1) {
			pos [1] -= y;
		}
		prepos [0] = wallpos [0];
		prepos [1] = wallpos [1];
		character.transform.position = new Vector2 (pos [0], pos [1]);
		pos [0] = (int)character.transform.position.x;
		pos [1] = (int)character.transform.position.y;
		wallpos [0] = (int)(pos [0] - 100) / 10;
		wallpos [1] = (int)(pos [1] - 100) / 10;
	}

	void GoRightRight (int x)
	{
		if (wall [wallpos [0] + 1, wallpos [1]] == 1) {
			pos [0] += x;
		}
		prepos [0] = wallpos [0];
		prepos [1] = wallpos [1];
		character.transform.position = new Vector2 (pos [0], pos [1]);
		pos [0] = (int)character.transform.position.x;
		pos [1] = (int)character.transform.position.y;
		wallpos [0] = (int)(pos [0] - 100) / 10;
		wallpos [1] = (int)(pos [1] - 100) / 10;
	}

	void GoLeftLeft (int x)
	{
		if (wall [wallpos [0] - 1, wallpos [1]] == 1) {
			pos [0] -= x;
		}
		prepos [0] = wallpos [0];
		prepos [1] = wallpos [1];
		character.transform.position = new Vector2 (pos [0], pos [1]);
		pos [0] = (int)character.transform.position.x;
		pos [1] = (int)character.transform.position.y;
		wallpos [0] = (int)(pos [0] - 100) / 10;
		wallpos [1] = (int)(pos [1] - 100) / 10;
	}

	//移動できる数の取得
	int Move (int x, int y)
	{
		int num = 0;
		canmove = new int[4];

		if (wall [x, y + 1] == 1) {
			num += 1;
			canmove [0] = 1;
		}
		if (wall [x + 1, y] == 1) {
			num += 1;
			canmove [1] = 1;
		}
		if (wall [x, y - 1] == 1) {
			num += 1;
			canmove [2] = 1;
		}
		if (wall [x - 1, y] == 1) {
			num += 1;
			canmove [3] = 1;
		}
		return num;
	}

	// Update is called once per frame
	void Update ()
	{
		int a = Move (wallpos [0], wallpos [1]);
		if (a > 2) {
			GoUp (10);
			GoDown (10);
			GoRight (10);
			GoLeft (10);
		}
		//スタートが２方向の時に行う
		if (a == 2) {
			if (meiro.startPos [0] == wallpos [0] && meiro.startPos [1] == wallpos [1]) {
				GoUp (10);
				GoDown (10);
				GoRight (10);
				GoLeft (10);
			} else {
				if (wallpos [1] == prepos [1] + 1) {
					if (canmove [0] == 1) {
						GoUpUp (10);
					} else {
						if (canmove [1] == 1) {
							GoRightRight (10);
						}
						if (canmove [3] == 1) {
							GoLeftLeft (10);
						}
					}
				} else if (wallpos [1] == prepos [1] - 1) {
					if (canmove [2] == 1) {
						GoDownDown (10);
					} else {
						if (canmove [1] == 1) {
							GoRightRight (10);
						}
						if (canmove [3] == 1) {
							GoLeftLeft (10);
						}
					}
				} else if (wallpos [0] == prepos [0] + 1) {
					if (canmove [1] == 1) {
						GoRightRight (10);
					} else {
						if (canmove [0] == 1) {
							GoUpUp (10);
						}
						if (canmove [2] == 1) {
							GoDownDown (10);
						}
					}
				} else if (wallpos [0] == prepos [0] - 1) {
					if (canmove [3] == 1) {
						GoLeftLeft (10);
					} else {
						if (canmove [0] == 1) {
							GoUpUp (10);
						}
						if (canmove [2] == 1) {
							GoDownDown (10);
						}
					}
				} 
			}
		}
		if (a == 1) {
			if (canmove [0] == 1) {
				GoUp (10);
			}
			if (canmove [1] == 1) {
				GoRight (10);
			}
			if (canmove [2] == 1) {
				GoDown (10);
			}
			if (canmove [3] == 1) {
				GoLeft (10);
			}
		}
		if (wallpos [0] == meiro.goalPos [0] && wallpos [1] == meiro.goalPos [1]) {
			//print ("crear");
			//Destroy (character);


		}
	}
}

