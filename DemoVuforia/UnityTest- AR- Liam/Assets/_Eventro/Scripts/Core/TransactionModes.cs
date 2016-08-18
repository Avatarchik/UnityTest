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
	public GameObject[] interiorAssetsVR;
	public GameObject[] exteriorAssetsAR;
	public GameObject[] cutCarAssetsAR;
	public GameObject[] showroomAssetsVR;

	private GameMode currentMode, previousMode;
	private SwitchMode switchM;
	private bool inAR;

	void Start ()
	{
		switchM = gameObject.GetComponent<SwitchMode> ();
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

	
		// Loading Assets



		yield return new WaitForSeconds ((0.25f));
		SetARCamPos ();
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

	#region Switch Mixed Reality Without Animation

	/// <summary>
	/// Swiths the Mixed reality mode without fade animation.
	/// </summary>
	/// <param name="mode">Mode.</param>

	internal void SwithMode (MixedRealityController.Mode mode, GameMode gmode)
	{
		if (mode == MixedRealityController.Mode.VIEWER_VR) {
	//			Debug.LogError ("Setting up VR MODE ");

			MixedRealityController.Instance.SetMode (MixedRealityController.Mode.VIEWER_VR); // was viewer ar 
			VuforiaBehaviour.Instance.SetWorldCenterMode (VuforiaAbstractBehaviour.WorldCenterMode.CAMERA);

//			VideoBackgroundManager.Instance.SetVideoBackgroundEnabled (false);
//			DigitalEyewearBehaviour.Instance.SetViewerActive (false);
//			DigitalEyewearBehaviour.Instance.SetMode (Device.Mode.MODE_VR);
			ActivateDatasets (false);
		} else if (mode == MixedRealityController.Mode.ROTATIONAL_VIEWER_AR) {
			//			Debug.LogError ("Setting up AR MODE ");
			ActivateDatasets (true);
			MixedRealityController.Instance.SetMode (MixedRealityController.Mode.ROTATIONAL_HANDHELD_AR);
			VideoBackgroundManager.Instance.SetVideoBackgroundEnabled (true);
		}

		// Load assets here
	}

	#endregion

	#region Activate Asset

	internal void LoadAssetsOf (GameMode mode)
	{
		previousMode = mode;
	}

	#endregion

	void OnApplicationPause ()
	{
		SetARCamPos ();
	}

	#region AR camera pos setting

	/// <summary>
	/// Sets the AR cam position. You to call this method when mode switch to from VR 
	/// and also when Application again activate. Like (Lock / screen sleep) 
	/// </summary>
	internal void SetARCamPos ()
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

	#endregion
}
