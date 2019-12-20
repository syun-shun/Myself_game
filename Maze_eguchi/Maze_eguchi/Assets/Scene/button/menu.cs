using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class menu : MonoBehaviour
{
	public Canvas canvasConfirmAllHoshiDelete;
	//public move move;

	void Start ()
	{
		if (canvasConfirmAllHoshiDelete != null) {
			canvasConfirmAllHoshiDelete.enabled = false;
		}
	}

	public void OnClick ()
	{
		if (canvasConfirmAllHoshiDelete != null) {
			canvasConfirmAllHoshiDelete.enabled = true;
		}

	}

	public void onRetry ()
	{
		canvasConfirmAllHoshiDelete.enabled = false;
	}

	public void onContinue ()
	{
		canvasConfirmAllHoshiDelete.enabled = false;
	}

	public void onNext ()
	{
		canvasConfirmAllHoshiDelete.enabled = false;
	}

	void Update ()
	{
		
	}
}
