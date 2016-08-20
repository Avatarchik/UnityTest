using UnityEngine;
using System.Collections;

public class SwitchMode : MonoBehaviour {

	public TransitionManager tm;
	// Use this for initialization
	void Start () {
	
	}

	internal void SwitchStereo (){
		tm.mTransitionCursor = 0; 
	}


	internal void SwitchMono (){
		tm.mTransitionCursor = 1; 
	}

	private bool toggle = false;
	public void SwitchM(){
		if (!toggle) { // Mono
			SwitchMono();
		} else { // Stereo
			SwitchStereo();
		}
		toggle = !toggle;
	}

}
