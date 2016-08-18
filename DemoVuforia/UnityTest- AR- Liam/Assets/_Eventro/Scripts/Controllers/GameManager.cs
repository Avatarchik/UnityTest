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

		private void OnEnable(){
			Delegate (true);
		}

		private void OnDisable(){
			Delegate (false);
		}
			
		private void Delegate( bool state){
			if (state) {
				DefaultTrackableEventHandler.TrackerState += DefaultTrackableEventHandler_TrackerState;
			} else {
				DefaultTrackableEventHandler.TrackerState -= DefaultTrackableEventHandler_TrackerState;
			}
		}

		#endregion

		// Update is called once per frame
		void Update ()
		{
			if (Input.GetKeyDown (KeyCode.E)) {
				SetGameMode (GameMode.AR);	
			} else if (Input.GetKeyDown (KeyCode.R)) {
				SetGameMode (GameMode.VR);	
			}
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

		#region Object Font And Lost 
		void DefaultTrackableEventHandler_TrackerState (bool obj)
		{
			if (obj) { // Object Found
			
			} else { // Object Lost
			
			}
		}
		#endregion
	
	}
}