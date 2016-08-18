using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Eventro.Testapp.Enums;
using Vuforia;

public class TransactionModes : MonoBehaviour
{
	[Range (2, 5)]
	public float transitionDuration = 2f;
	// seconds
	public Animator fadeAnimator;
	public GameObject[] VRAssets;
	public GameObject[] ARAssets;

	private GameMode currentMode, previousMode;
	private SwitchMode switchM;
	private bool inAR;

	void Start ()
	{
		switchM = gameObject.GetComponent<SwitchMode> ();
		LoadAssetsOf (MixedRealityMode.AR_MONO);
	}

	#region Switch Mixed Reality mode with Animation

	/// This Method will play Fade animation 
	public void SwitchMode (MixedRealityMode mode, GameMode gMode, bool requiredAnimation = false)
	{
		currentMode = gMode;
		if (currentMode == previousMode) {
			return;
		}			
		StartCoroutine (PlayAnimation (mode, gMode, requiredAnimation));
	}
		
	// Animation for Fade
	IEnumerator PlayAnimation (MixedRealityMode mode, GameMode gMode, bool requiredAnimation)
	{
	
		if (requiredAnimation)
			Animation (true);
		yield return new WaitForSeconds (0.25f); // ANIMATION DURATION // All the work should be behiend the black shade 
		inAR = (mode == MixedRealityMode.AR_MONO);
		if (!inAR) {
			// IN VR
			switchM.SwitchVR ();
		} else {
			// IN AR
			switchM.SwitchAR ();
		}
//		 Loading Assets
		LoadAssetsOf ( mode);

		yield return new WaitForSeconds ((0.25f));
//		SetARCamPos (); // Try to enable it 
		if (requiredAnimation)
			Animation (false);
		previousMode = gMode;
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
		foreach (var item1 in VRAssets) {
			item1.SetActive (mode == MixedRealityMode.VR_STEREO);
		}

		foreach (var item1 in ARAssets) {
			item1.SetActive (mode == MixedRealityMode.AR_MONO);
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
