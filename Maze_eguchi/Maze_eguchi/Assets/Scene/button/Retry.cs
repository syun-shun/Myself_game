using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Retry : MonoBehaviour
{
	//Retryのボタンを押したら実行
	public void ButtonPush ()
	{
		SceneManager.LoadScene ("Test");
	}
	
	// Update is called once per frame
	void Update ()
	{
		
	}
}
