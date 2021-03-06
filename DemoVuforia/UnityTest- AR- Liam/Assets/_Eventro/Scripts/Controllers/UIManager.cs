﻿using UnityEngine;
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

		[SerializeField]
		private GameObject cubeMovementControls;

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
			print ("Focus Complete "  + objName);
			switch (objName) {
			case "TransformerVideo":
				// Play transformer video
				print ("Play transformer video");
				GameManager.Instance.PlayVideoOf (objName);
				break;

			case "3DDoneButton":
				
				EnableTracking ();
				break;
			}
		}

		#endregion
	
		#region Done Button
		// This button will enable the tracking state
		public void EnableTracking(){
			GameManager.Instance.TargetTracking (true);
		}
		#endregion

		#region Cube Movement Controls
		internal void CubeMovementControls( bool state){
			cubeMovementControls.SetActive (state);
		}
		#endregion


	}
}