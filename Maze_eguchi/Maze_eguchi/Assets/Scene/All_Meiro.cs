using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class All_Meiro : MonoBehaviour
{
	public static int max = 25;
	public GameObject wall;
	public GameObject start;
	public GameObject goal;
	public GameObject character;
	public GameObject clearImage;
	public GameObject menuImage;
	public GameObject UpButton;
	public GameObject DownButton;
	public GameObject RightButton;
	public GameObject LeftButton;
	public Text Times;
	public Text Score;
	public static int[,] walls;
	public static int[] startPos;
	public static int[] goalPos;
	public int[] pos = new int[2];
	public int[] canmove;
	public static int[] wallpos = new int[2];
	public int[] prepos = new int[2];
	public int flag;
	public int menuflag;
	public int Upflag;
	public int Downflag;
	public int Rightflag;
	public int Leftflag;
	public int Upf;
	public int Downf;
	public int Rightf;
	public int Leftf;

	// speedは 1 , 2 , 5 , 10 のどれかでお願いします
	public int speed = 2;

	float times;
	float starttime;
	float scoretime;


	void Awake ()
	{
		flag = 0;
		menuflag = 0;
		clearImage.SetActive (false);
		menuImage.SetActive (false);

		starttime = Time.time;
		times = starttime;
	}

	void OnGUI ()
	{
		if (menuflag == 0) {
			Time.timeScale = 0.5f;
			times += Time.deltaTime;
		}
		if (menuflag == 1) {
			Time.timeScale = 0.0f;
		}
		if (wallpos [0] == goalPos [0] && wallpos [1] == goalPos [1]) {
			Time.timeScale = 0.0f;
		}
		Times.text = "時間：" + (int)(times - starttime);
	}

	// Use this for initialization
	void Start ()
	{
		Upflag = 0;
		Downflag = 0;
		Rightflag = 0;
		Leftflag = 0;
		Upf = 0;
		Downf = 0;
		Rightf = 0;
		Leftf = 0;

		if (flag == 0) {
			//迷路の作成
			Make ();
			flag = 1;
		}
		//移動を行う
		Go ();
	}

	public void menu ()
	{
		if (menuflag == 0) {
			menuImage.SetActive (true);
			menuImage.transform.SetAsLastSibling ();
			menuflag = 1;
		} else {
			menuImage.SetActive (false);
			menuflag = 0;
		}
	}

	public void OnClickRetry ()
	{
		if (menuflag == 1) {
			menuflag = 0;
		}
		times = starttime;
		clearImage.SetActive (false);
		menuImage.SetActive (false);
		character.transform.position = new Vector2 (startPos [0] * 10 + 100, startPos [1] * 10 + 100);
		Go ();
	}

	public void OnClickNext ()
	{
		if (menuflag == 1) {
			menuflag = 0;
		}
		starttime = Time.time;
		times = starttime;
		clearImage.SetActive (false);
		Application.LoadLevel (0);
	}

	public void OnClickContinue ()
	{
		if (menuflag == 1) {
			menuflag = 0;
		}
		menuImage.SetActive (false);
	}

	void Make ()
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

	//迷路の作成
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

	void Go ()
	{
		pos [0] = startPos [0] * 10 + 100;
		pos [1] = startPos [1] * 10 + 100;

		wallpos [0] = startPos [0];
		wallpos [1] = startPos [1];

		prepos [0] = wallpos [0];
		prepos [1] = wallpos [1];

		character.transform.position = new Vector2 (pos [0], pos [1]);
	}

	// yだけ上に進む
	public void GoUp (int y)
	{
		if (walls [wallpos [0], wallpos [1] + 1] == 1) {
			character.transform.eulerAngles = new Vector3 (0, 0, 90);
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

	// yだけ下に進む
	public void GoDown (int y)
	{
		if (walls [wallpos [0], wallpos [1] - 1] == 1) {
			character.transform.eulerAngles = new Vector3 (0, 0, 270);
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

	// xだけ右に進む
	public void GoRight (int x)
	{
		if (walls [wallpos [0] + 1, wallpos [1]] == 1) {
			character.transform.eulerAngles = new Vector3 (0, 0, 0);
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

	// xだけ左に進む
	public void GoLeft (int x)
	{
		if (walls [wallpos [0] - 1, wallpos [1]] == 1) {
			character.transform.eulerAngles = new Vector3 (0, 0, 180);
			pos [0] -= x;
		}
		prepos [0] = wallpos [0];
		prepos [1] = wallpos [1];
		character.transform.position = new Vector2 (pos [0], pos [1]);
		pos [0] = (int)character.transform.position.x;
		pos [1] = (int)character.transform.position.y;
		wallpos [0] = (int)(pos [0] - 100) / 10;
		wallpos [1] = (int)(pos [1] - 100) / 10;
		Leftf = 0;
	}

	void GoUpUp (int y)
	{
		if (walls [wallpos [0], wallpos [1] + 1] == 1) {
			character.transform.eulerAngles = new Vector3 (0, 0, 90);
			if (Upflag == (int)10 / speed) {
				prepos [0] = wallpos [0];
				prepos [1] = wallpos [1];
				character.transform.position = new Vector2 (pos [0], pos [1]);
				pos [0] = (int)character.transform.position.x;
				pos [1] = (int)character.transform.position.y;
				wallpos [0] = (int)(pos [0] - 100) / 10;
				wallpos [1] = (int)(pos [1] - 100) / 10;
				Upflag = 0;
			} else {
				pos [1] += y;
				character.transform.position = new Vector2 (pos [0], pos [1]);
				Upflag += 1;
			}
		}
	}

	void GoDownDown (int y)
	{
		if (walls [wallpos [0], wallpos [1] - 1] == 1) {
			character.transform.eulerAngles = new Vector3 (0, 0, 270);
			if (Downflag == (int)10 / speed) {
				prepos [0] = wallpos [0];
				prepos [1] = wallpos [1];
				character.transform.position = new Vector2 (pos [0], pos [1]);
				pos [0] = (int)character.transform.position.x;
				pos [1] = (int)character.transform.position.y;
				wallpos [0] = (int)(pos [0] - 100) / 10;
				wallpos [1] = (int)(pos [1] - 100) / 10;
				Downflag = 0;
			} else {
				pos [1] -= y;
				character.transform.position = new Vector2 (pos [0], pos [1]);
				Downflag += 1;
			}
		}
	}

	void GoRightRight (int x)
	{
		if (walls [wallpos [0] + 1, wallpos [1]] == 1) {
			character.transform.eulerAngles = new Vector3 (0, 0, 0);
			if (Rightflag == (int)10 / speed) {
				prepos [0] = wallpos [0];
				prepos [1] = wallpos [1];
				character.transform.position = new Vector2 (pos [0], pos [1]);
				pos [0] = (int)character.transform.position.x;
				pos [1] = (int)character.transform.position.y;
				wallpos [0] = (int)(pos [0] - 100) / 10;
				wallpos [1] = (int)(pos [1] - 100) / 10;
				Rightflag = 0;
			} else {
				pos [0] += x;
				character.transform.position = new Vector2 (pos [0], pos [1]);
				Rightflag += 1;
			}
		}
	}

	void GoLeftLeft (int x)
	{
		if (walls [wallpos [0] - 1, wallpos [1]] == 1) {
			character.transform.eulerAngles = new Vector3 (0, 0, 180);
			if (Leftflag == (int)10 / speed) {
				prepos [0] = wallpos [0];
				prepos [1] = wallpos [1];
				character.transform.position = new Vector2 (pos [0], pos [1]);
				pos [0] = (int)character.transform.position.x;
				pos [1] = (int)character.transform.position.y;
				wallpos [0] = (int)(pos [0] - 100) / 10;
				wallpos [1] = (int)(pos [1] - 100) / 10;
				Leftflag = 0;
			} else {
				pos [0] -= x;
				character.transform.position = new Vector2 (pos [0], pos [1]);
				Leftflag += 1;
			}
		}
	}

	//移動できる数の取得
	int Move (int x, int y)
	{
		int num = 0;
		canmove = new int[4];

		if (walls [x, y + 1] == 1) {
			num += 1;
			canmove [0] = 1;
			UpButton.SetActive (true);
		}
		if (walls [x + 1, y] == 1) {
			num += 1;
			canmove [1] = 1;
			RightButton.SetActive (true);
		}
		if (walls [x, y - 1] == 1) {
			num += 1;
			canmove [2] = 1;
			DownButton.SetActive (true);
		}
		if (walls [x - 1, y] == 1) {
			num += 1;
			canmove [3] = 1;
			LeftButton.SetActive (true);
		}
		return num;
	}

	// Update is called once per frame
	void Update ()
	{
		if (menuflag == 0) {
			if (wallpos [0] == goalPos [0] && wallpos [1] == goalPos [1]) {
				character.transform.position = new Vector2 (goalPos [0] * 10 + 100, goalPos [1] * 10 + 100);
				clearImage.SetActive (true);
				clearImage.transform.SetAsLastSibling ();
				Score.text = "記録 : " + (int)(times - starttime) + "秒";
				wall.SetActive (false);
			} else {
				int a = Move (wallpos [0], wallpos [1]);

				if (canmove [0] == 0) {
					UpButton.SetActive (false);
				}
				if (canmove [1] == 0) {
					RightButton.SetActive (false);
				}
				if (canmove [2] == 0) {
					DownButton.SetActive (false);
				}
				if (canmove [3] == 0) {
					LeftButton.SetActive (false);
				}

				if (a > 2) {
					if (Input.GetKeyDown (KeyCode.UpArrow)) {
						GoUp (10);
					}
					if (Input.GetKeyDown (KeyCode.DownArrow)) {
						GoDown (10);
					}
					if (Input.GetKeyDown (KeyCode.RightArrow)) {
						GoRight (10);
					}
					if (Input.GetKeyDown (KeyCode.LeftArrow)) {
						GoLeft (10);
					}
				}
				//スタートが２方向の時に行う
				if (a == 2) {
					if (startPos [0] == wallpos [0] && startPos [1] == wallpos [1]) {
						if (Input.GetKeyDown (KeyCode.UpArrow)) {
							GoUp (10);
						}
						if (Input.GetKeyDown (KeyCode.DownArrow)) {
							GoDown (10);
						}
						if (Input.GetKeyDown (KeyCode.RightArrow)) {
							GoRight (10);
						}
						if (Input.GetKeyDown (KeyCode.LeftArrow)) {
							GoLeft (10);
						}
					} else {
						if (wallpos [1] == prepos [1] + 1) {
							if (canmove [0] == 1) {
								GoUpUp (speed);
							} else {
								if (canmove [1] == 1) {
									GoRightRight (speed);
								}
								if (canmove [3] == 1) {
									GoLeftLeft (speed);
								}
							}
						} else if (wallpos [1] == prepos [1] - 1) {
							if (canmove [2] == 1) {
								GoDownDown (speed);
							} else {
								if (canmove [1] == 1) {
									GoRightRight (speed);
								}
								if (canmove [3] == 1) {
									GoLeftLeft (speed);
								}
							}
						} else if (wallpos [0] == prepos [0] + 1) {
							if (canmove [1] == 1) {
								GoRightRight (speed);
							} else {
								if (canmove [0] == 1) {
									GoUpUp (speed);
								}
								if (canmove [2] == 1) {
									GoDownDown (speed);
								}
							}
						} else if (wallpos [0] == prepos [0] - 1) {
							if (canmove [3] == 1) {
								GoLeftLeft (speed);
							} else {
								if (canmove [0] == 1) {
									GoUpUp (speed);
								}
								if (canmove [2] == 1) {
									GoDownDown (speed);
								}
							}
						} 
					}
				}
				if (a == 1) {
					if (canmove [0] == 1) {
						if (Input.GetKeyDown (KeyCode.UpArrow)) {
							GoUp (10);
						}
					}
					if (canmove [1] == 1) {
						if (Input.GetKeyDown (KeyCode.RightArrow)) {
							GoRight (10);
						}
					}
					if (canmove [2] == 1) {
						if (Input.GetKeyDown (KeyCode.DownArrow)) {
							GoDown (10);
						}
					}
					if (canmove [3] == 1) {
						if (Input.GetKeyDown (KeyCode.LeftArrow)) {
							GoLeft (10);
						}
					}
				}
			}
		}
	}
}