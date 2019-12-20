using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class time : MonoBehaviour
{
	public Text Times;
	float times;
	float starttime;
	// Use this for initialization
	void Start ()
	{
		times = 0f;
		starttime = Time.time;
	}

	void OnGUI ()
	{
		Time.timeScale = 0.5f;
		times += Time.deltaTime;
		Times.text = "時間：" + (int)((times - starttime));
	}


	// Update is called once per frame
	void Update ()
	{
		
	}
}
