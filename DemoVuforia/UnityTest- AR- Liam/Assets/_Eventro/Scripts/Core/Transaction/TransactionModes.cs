using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Eventro.Testapp.Enums;
using Eventro.Testapp.Controllers;
using Eventro.Testapp.Common;
using Vuforia;

public class TransactionModes : MonoBehaviour
{
	[Range (2, 5)]
	public float transitionDuration = 2f;
	// seconds
	public Animator fadeAnimator;
	public GameObject[] ARMonoAssets;
	public GameObject[] ARStereoAssets;

	private GameMode currentMode, previousMode;
	private SwitchMode switchM;
	private bool inMono;

	void Start ()
	{
		switchM = gameObject.GetComponent<SwitchMode> ();
		LoadAssetsOf (Constants.CURRENT_MIXED_REALITY_MODE);
	}

	#region Switch Mixed Reality mode with Animation

	/// This Method will play Fade animation 
	public void SwitchMode (MixedRealityMode mode, bool requiredAnimation = false)
	{
		/*if (currentMode == previousMode) {
			return;
		}*/			
		StartCoroutine (PlayAnimation (mode, requiredAnimation));
	}
		
	// Animation for Fade
	IEnumerator PlayAnimation (MixedRealityMode mode, bool requiredAnimation)
	{
	
		if (requiredAnimation)
			Animation (true);
		yield return new WaitForSeconds (0.25f); // ANIMATION DURATION // All the work should be behiend the black shade 
		inMono = (mode == MixedRealityMode.AR_MONO);
		if (!inMono) {
			// IN Mono
			switchM.SwitchStereo ();
		} else {
			// IN Stereo
			switchM.SwitchMono ();
		}
//		 Loading Assets
		LoadAssetsOf (mode);

		yield return new WaitForSeconds ((0.25f));
//		SetARCamPos (); // Try to enable it 
		if (requiredAnimation)
			Animation (false);
	}

	internal void Animation (bool state)
	{
		fadeAnimator.SetBool ("FadeIn", state);
	}

	private void FadeOutAnimation ()
	{
		Animation (false);
	}

	private void ActivateDatasets (bool enableDataset)
	{
		//Disable/Enable dataset
		ObjectTracker objectTracker = TrackerManager.Instance.GetTracker<ObjectTracker> ();
		IEnumerable<DataSet> datasets = objectTracker.GetDataSets ();
		foreach (DataSet dataset in datasets) {
			if (enableDataset)
				objectTracker.ActivateDataSet (dataset);
			else
				objectTracker.DeactivateDataSet (dataset);
		}
	}

	#endregion

	#region Activate Asset

	internal void LoadAssetsOf (MixedRealityMode mode)
	{
		foreach (var item1 in ARMonoAssets) {
			item1.SetActive (mode == MixedRealityMode.AR_MONO);
		}

		foreach (var item2 in ARStereoAssets) {
			item2.SetActive (mode == MixedRealityMode.AR_STEREO);
		}
//		previousMode = mode;
	}

	#endregion

	void OnApplicationPause ()
	{
//		SetARCamPos ();
	}

	#region AR camera pos setting

	/// <summary>
	/// Sets the AR cam position. You to call this method when mode switch to from VR
	/// and also when Application again activate. Like (Lock / screen sleep)
	/// </summary>
	/*	internal void SetARCamPos ()
	{
		// Setting up custom camera setting for VR 
		// AR will adjust automatically
		if (!inAR) {
			Camera pCam = Vuforia.DigitalEyewearBehaviour.Instance.PrimaryCamera;
			Camera sCam = Vuforia.DigitalEyewearBehaviour.Instance.SecondaryCamera;
			pCam.transform.parent.position = new Vector3 (0, 0.1f, -0.288f);
			pCam.transform.localPosition = sCam.transform.localPosition = Vector3.zero;
			pCam.fieldOfView = sCam.fieldOfView = 50;
		}
	}
*/

	#endregion
}
