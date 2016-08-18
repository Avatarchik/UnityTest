/*============================================================================== 
 * Copyright (c) 2015 Qualcomm Connected Experiences, Inc. All Rights Reserved. 
 * ==============================================================================*/
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Eventro.Testapp.UI;

public class CameraRay : MonoBehaviour
{
    #region PUBLIC_MEMBER_VARIABLES
	//	public ViewTrigger[] viewTriggers;// Original

	public Buttons[] buttons;
    #endregion // PUBLIC_MEMBER_VARIABLES

    #region MONOBEHAVIOUR_METHODS
    void Update()
    {
        // Check if the Head gaze direction is intersecting any of the ViewTriggers
        RaycastHit hit;
        Ray cameraGaze = new Ray(this.transform.position, this.transform.forward);
        Physics.Raycast(cameraGaze, out hit, Mathf.Infinity);
		Debug.DrawLine(cameraGaze.origin, hit.point,Color.red );

		foreach (var trigger in buttons)
        {
			trigger.isFocused = hit.collider && (hit.collider.gameObject == trigger.gameObject);
			//	print ("Ray cast hitted to the object" + hit.collider);
        }
    }
    #endregion // MONOBEHAVIOUR_METHODS
}

