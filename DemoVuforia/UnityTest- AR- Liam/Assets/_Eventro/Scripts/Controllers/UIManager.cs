using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using Eventro.Testapp.UI;
using Eventro.Testapp.Common;
using Eventro.Testapp.Enums;

namespace Eventro.Testapp.Controllers
{

	public class UIManager : MonoBehaviour
	{

		public static UIManager Instance;
		private bool modeToggle = false;

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

		#region Car

		// It will set the body color and colorname on the bar
		internal void CarBodyColorSelected (string colorName)
		{
		}

		internal void BackButtonColorBar ()
		{
		}

		#endregion

		/// <summary>
		/// Toggles the Mixed Reality mode.
		/// </summary>
		public void ToggleMode ()
		{
			if (!modeToggle) { // By default app will open in Stereo So load mono
				GameManager.Instance.SetMixedRealityMode (MixedRealityMode.AR_MONO);
			} else {
				GameManager.Instance.SetMixedRealityMode (MixedRealityMode.AR_STEREO);
			}
			modeToggle = !modeToggle;	
		}
	}
}