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

		private SwitchMode switchMode;
		private TransactionModes transactionMode;
		internal MixedRealityMode currentMixedRealityMode;
		internal VideoPlaybackController videoPlayController;

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

		#region Set Mixed Reality Mode

		internal void SetMixedRealityMode (MixedRealityMode mode)
		{
			transactionMode.SwitchMode (mode);	
		}

		#endregion

		#region Set Game Mode

		internal void SetGameMode (GameMode mode)
		{
			switch (mode) {
			case GameMode.CubeTest: //Stereo
				
				break;

			case GameMode.VideoPlayerTest: // Mono
				
				break;
			default:
				break;
			}
		}

		#endregion

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

	}
}