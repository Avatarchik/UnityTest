using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using Vuforia;
using Eventro.Testapp.UI;
using Eventro.Testapp.Common;
using Eventro.Testapp.Enums;

namespace Eventro.JazzTour.Controllers
{

	public class UIManager : MonoBehaviour, ITrackableEventHandler
	{

		public static UIManager Instance;
		private TrackableBehaviour trackableBehaviour;

		#region Init

		void Awake ()
		{
		}

		void Start ()
		{
			InitStart ();
		}

		void InitStart ()
		{
			Instance = this;
			trackableBehaviour.RegisterTrackableEventHandler (this);
		}

		void OnEnable ()
		{
			SetupDelegates (true);
		}

		void OnDisable ()
		{
			SetupDelegates (false);
		}

		#endregion

		#region Delegates

		private void SetupDelegates (bool state)
		{
			if (state) {
				Buttons.buttonFoucsedComplete += Buttons_buttonFoucsedComplete;
			} else {
				Buttons.buttonFoucsedComplete -= Buttons_buttonFoucsedComplete;
			
			}
		
		}

		void Buttons_buttonFoucsedComplete (string obj)
		{
			HandleFoucusedComplete (obj);	
		}


		#endregion

		// Update is called once per frame
		void Update ()
		{
			#region Back Button
			if (Input.GetKeyDown (KeyCode.Escape)) {
				Application.Quit ();
			}
			#endregion
		}

		#region Handle Buttons Foucsed Completed/Lost UI

		private void HandleFoucusedComplete (string objName)
		{

			switch (objName) {

			// Custom 3D  // There are some duplicate buttons present in the Hierarchy of ARHUD Exterior / ARHUD Interior, So please Stay AWAKE
		
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

		}

		private void OnTrackerLost ()
		{

		}

		#endregion

		#region Car Body paint

		// It will set the body color and colorname on the bar
		internal void CarBodyColorSelected (string colorName)
		{
		}

		[System.Obsolete ("This button is removed as per 20 July 2016 Build. Now this button is no more present in hierarchy")]
		internal void BackButtonColorBar ()
		{
		}

		#endregion

	}
}