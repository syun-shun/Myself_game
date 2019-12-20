using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test : MonoBehaviour
{
	public block block;
	// Use this for initialization
	void Start ()
	{
		//配列の参照(blockのクラスから)
		int[,] array2 = new int[20, 20];
		for (int i = 0; i < (array2.Length) / array2.GetLength (0); i++) {
			for (int j = 0; j < array2.GetLength (0); j++) {
				array2 [i, j] = block.array [i, j];
				print (i + "," + j + "=" + block.array [i, j]);
			}
		}
		/*int[,] array = new int[5, 5];
		for (int i = 0; i < 5; i++) {
			for (int j = 0; j < 5; j++) {
				if (i == 0 && j == 0) {
					array [i, j] = 10;
				}
				if (i == 4 && j == 4) {
					array [i, j] = 10;
				}
				int a = Random.Range (0, 2);
				array [i, j] = a;
				print (i + "," + j + "=" + array [i, j]);
			}
		}*/




	}
	
	// Update is called once per frame
	void Update ()
	{
		
	}
}
