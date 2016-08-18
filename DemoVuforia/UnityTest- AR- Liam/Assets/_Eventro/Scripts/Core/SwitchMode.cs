using UnityEngine;
using System.Collections;
using System.Collections.Generic;

using Eventro.Testapp.Enums;
using Eventro.Testapp.Core;

public class SwitchMode : MonoBehaviour
{
	public SSTransitionManager tm;

	void Start ()
	{
		InitialMixedRealityMode ();
	}

	private void InitialMixedRealityMode ()
	{
//		switch (GameManager.Instance.gameMode) {
//		case GameMode.Showroom:
//		case GameMode.Interior:
//			SwitchVR ();
//			break;
//		case GameMode.CutCar:
//		case GameMode.Exterior:
//			SwitchAR ();
//			break;
//		default:
//			break;
//		} 
	}

	public void SwitchAR ()
	{
		tm.mTransitionCursor = 0;
	}

	public void SwitchVR ()
	{
		tm.mTransitionCursor = 1;
	}
}
