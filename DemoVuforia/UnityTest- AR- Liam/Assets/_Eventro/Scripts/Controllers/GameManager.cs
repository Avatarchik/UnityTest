using UnityEngine;
using System;
using System.Collections;
using Eventro.Testapp.UI;
using Eventro.Testapp.Common;
using Eventro.Testapp.Enums;
using Vuforia;

namespace Eventro.Testapp.Controllers
{

	public class GameManager : MonoBehaviour
	{

		public static GameManager Instance;

		public GameObject doneButton;
	
		[Tooltip ("Place the other HUD buttons which you want to toggle when tracker state is active/inactive")]
		public GameObject[] oppositeDoneButton;
		public GameObject[] trackingObjects;

		private SwitchMode switchMode;
		private TransactionModes transactionMode;
		private ObjectPanController objectPanController;

		internal VideoPlaybackController videoPlayController;
		internal bool isTrakingEnabled = true;

		#region Init

		void Awake ()
		{
			InitAwake ();
		}

		private void InitAwake ()
		{
			Instance = this;
			switchMode = gameObject.GetComponent<SwitchMode> ();
			transactionMode = gameObject.GetComponent<TransactionModes> ();
			videoPlayController = new VideoPlaybackController ();
//			currentMixedRealityMode = MixedRealityMode.AR_STEREO;
//			SetMixedRealityMode (currentMixedRealityMode);	
		}

		// Use this for initialization
		void Start ()
		{
			InitStart ();
		}

		private void InitStart ()
		{
			objectPanController = gameObject.GetComponent<ObjectPanController> ();
		}

		private void OnEnable ()
		{
			Delegate (true);
		}

		private void OnDisable ()
		{
			Delegate (false);
		}

		private void Delegate (bool state)
		{
			if (state) {
				//DefaultTrackableEventHandler.TrackerState += DefaultTrackableEventHandler_TrackerState;
			} else {
				//DefaultTrackableEventHandler.TrackerState -= DefaultTrackableEventHandler_TrackerState;
			}
		}

		#endregion

		// Update is called once per frame

		void Update ()
		{
			
		}

		#region Obsolete Methods 

		#region Object Font And Lost

		void DefaultTrackableEventHandler_TrackerState (bool obj)
		{
			if (obj) { // Object Found
			
			} else { // Object Lost
			
			}
		}

		#endregion
	
		#endregion


		#region Video Player Controller
		internal void PlayVideoOf(string objName){
			videoPlayController.PlayVideoOf (objName);
		}

		internal void PauseCurrentVideo(){
			videoPlayController.PauseCurrentVideo();
		}
		#endregion

		#region Tracking State
		/// <summary>
		/// It will controls the Tracking object and the Done button
		/// </summary>
		/// <param name="state">If set to <c>true</c> state then it will enable the tracking & disable Done .</param>
		/// <param name="state">If set to <c>false</c> state then it will Disable the tracking & enable Done</param>
		internal void TargetTracking(bool state){
			// it will help us to know whether the tracing is enable or not.. 
			isTrakingEnabled = state;

			foreach (var item in trackingObjects) {
				item.SetActive (state);
			}	

			foreach (var item in oppositeDoneButton) {
				item.SetActive (state);
			}

			doneButton.SetActive (!state);
		}
		#endregion
	
		#region Cube Movement Controls
		internal void CubeMovementControls( bool state){
			if (panAbleObject) {
				UIManager.Instance.CubeMovementControls (state);
			}
		}
		#endregion

		#region SetObject Pan
		GameObject panAbleObject; 
		internal void SetObjectToPan(GameObject go){
			panAbleObject = go;
			objectPanController.objectToBePan = go;
			objectPanController.SetCubePos ();
		}
		internal GameObject GetObjectToPan(){
			return panAbleObject;
		}
		#endregion

	}
}