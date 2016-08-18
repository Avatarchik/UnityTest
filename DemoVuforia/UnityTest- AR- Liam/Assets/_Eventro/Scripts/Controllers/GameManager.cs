using UnityEngine;
using System;
using System.Collections;
using Eventro.Testapp.UI;
using Eventro.Testapp.Common;
using Eventro.Testapp.Enums;
using Vuforia;

namespace Eventro.Testapp.Controllers
{

	public class GameManager : MonoBehaviour, ITrackableEventHandler
	{

		public static GameManager Instance;
		private SwitchMode switchMode;
		public static event Action<bool> TrackerState;
		#region Init

		void Awake ()
		{
			InitAwake ();
		}

		private void InitAwake ()
		{
			Instance = this;
			switchMode = gameObject.GetComponent<SwitchMode> ();
		}

		// Use this for initialization
		void Start ()
		{
			InitStart ();
		}


		private void InitStart ()
		{
		
		}

		#endregion

		// Update is called once per frame
		void Update ()
		{

		}


		#region Set Game Mode

		internal void SetGameMode (GameMode mode)
		{
			switch (mode) {
			case GameMode.AR: //Stereo
				switchMode.SwitchAR ();			
				break;

			case GameMode.VR: // Mono
				switchMode.SwitchVR ();			
				break;

			

			default:
				break;
			}
		}

		#endregion


		#region ITrackableEventHandler implementation

		public void OnTrackableStateChanged (TrackableBehaviour.Status previousStatus, TrackableBehaviour.Status newStatus)
		{
			bool trackerFound;
			if (newStatus == TrackableBehaviour.Status.DETECTED || newStatus == TrackableBehaviour.Status.TRACKED || newStatus == TrackableBehaviour.Status.EXTENDED_TRACKED) {
				OnTrackerFound ();
				trackerFound = true;
			} else {
				trackerFound = false;
				OnTrackerLost ();
			}

		}


		private void OnTrackerFound ()
		{
			//Set the cube upside
			TrackerState(true);
		}

		private void OnTrackerLost ()
		{
			TrackerState(false);
		}

		#endregion

	}
}