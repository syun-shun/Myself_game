using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Continue : MonoBehaviour
{
	public block block;
	public move move;

	//Continueのボタン押したら実行
	public void ButtonPush ()
	{
		SceneManager.LoadScene ("Test");

	}

	// Use this for initialization
	void Start ()
	{
		
	}
	
	// Update is called once per frame
	void Update ()
	{
		
	}
}
