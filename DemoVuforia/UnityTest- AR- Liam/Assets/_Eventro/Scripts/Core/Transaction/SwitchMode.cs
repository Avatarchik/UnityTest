using UnityEngine;
using System.Collections;
using Eventro.Testapp.Enums;
using Eventro.Testapp.Controllers;

public class SwitchMode : MonoBehaviour {

	public TransitionManager tm;

	// Use this for initialization
	void Start () {
	
	}

	internal void SwitchStereo (){
		if (GameManager.Instance)
			GameManager.Instance.SetMixedRealityMode (MixedRealityMode.AR_STEREO); 
		tm.mTransitionCursor = 0; 
	}


	internal void SwitchMono (){
		if (GameManager.Instance)
			GameManager.Instance.SetMixedRealityMode (MixedRealityMode.AR_MONO); 
		tm.mTransitionCursor = 1; 
	}

	private bool toggle = false;
	public void SwitchM(){ // Switch b/w modes 
		tm.runUpdate = true;
		if (!toggle) { // Mono
			SwitchMono();
		} else { // Stereo
			SwitchStereo();
		}
		toggle = !toggle;
	}
		
	public void SwitchScnes(string scneName){
		print ("Switch scneds "  + scneName );
		UnityEngine.SceneManagement.SceneManager.LoadScene (scneName.ToString());
	}

}
