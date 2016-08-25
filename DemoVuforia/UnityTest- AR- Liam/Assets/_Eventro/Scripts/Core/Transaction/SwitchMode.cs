using UnityEngine;
using System.Collections;
using Eventro.Testapp.Enums;
using Eventro.Testapp.Controllers;
using Eventro.Testapp.Common;

public class SwitchMode : MonoBehaviour
{

	public TransitionManager tm;

	// Use this for initialization
	void Start ()
	{
	
	}

	internal void SwitchStereo ()
	{
		Constants.CURRENT_MIXED_REALITY_MODE = MixedRealityMode.AR_STEREO; 
		if (GameManager.Instance && (Constants.CURRENT_GAME_MODE == GameMode.CubeTest))
			GameManager.Instance.CubeMovementControls (false);
		tm.mTransitionCursor = 0; 
	}


	internal void SwitchMono ()
	{
		Constants.CURRENT_MIXED_REALITY_MODE = MixedRealityMode.AR_MONO; 
		if (Constants.CURRENT_GAME_MODE == GameMode.CubeTest)
			GameManager.Instance.CubeMovementControls (true);
		tm.mTransitionCursor = 1; 
	}

	private bool toggle = false;

	// Switch b/w Mixed Reality modes
	public void SwitchM ()
	{ 
		tm.runUpdate = true;
		if (!toggle) { // Mono
			SwitchMono ();
		} else { // Stereo
			SwitchStereo ();
		}
		toggle = !toggle;
	}

	public void SwitchScnes (string scneName)
	{
		if (scneName == Constants.SCENE_NAME_1)
			Constants.CURRENT_GAME_MODE = GameMode.CubeTest;
		else if (scneName == Constants.SCENE_NAME_2)
			Constants.CURRENT_GAME_MODE = GameMode.VideoPlayerTest;

		UnityEngine.SceneManagement.SceneManager.LoadScene (scneName.ToString ());
	}

}
