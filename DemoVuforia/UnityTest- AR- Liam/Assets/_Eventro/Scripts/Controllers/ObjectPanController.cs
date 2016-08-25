﻿using UnityEngine;
using System.Collections;

public class ObjectPanController : MonoBehaviour
{

	private enum RotationClicked
	{
		Idle,
		Up,
		Down,
		Left,
		Right,

	}

	private RotationClicked rotation;
	private float speed = 2;
	public GameObject objectToBePan;

	// Use this for initialization
	void Start ()
	{
	
	}
	
	// Update is called once per frame
	void Update ()
	{
		Rotate ();
	}

	void Rotate ()
	{
		switch (rotation) {
		case RotationClicked.Up:
			RotationHori (speed);
			break;
		case RotationClicked.Down:
			RotationHori (-speed);
			break;
		case RotationClicked.Left:
			RotationVer (speed);
			break;
		case RotationClicked.Right:
			RotationVer (-speed);
			break;
		default:
			break;
		}

	}

	private void RotationHori (float speed)
	{
		objectToBePan.transform.localEulerAngles = new Vector3 (
			objectToBePan.transform.localEulerAngles.x + speed,
			objectToBePan.transform.localEulerAngles.y,
			objectToBePan.transform.localEulerAngles.z
		);
	}

	private void RotationVer (float speed)
	{
		objectToBePan.transform.localEulerAngles = new Vector3 (
			objectToBePan.transform.localEulerAngles.x,
			objectToBePan.transform.localEulerAngles.y + speed,
			objectToBePan.transform.localEulerAngles.z
		);
	}


	public void Up (bool clicked)
	{
		if (clicked)
			rotation = RotationClicked.Up;
		else
			rotation = RotationClicked.Idle;

	}


	public void Down (bool clicked)
	{
		if (clicked)
			rotation = RotationClicked.Down;
		else
			rotation = RotationClicked.Idle;

	}

	public void Left (bool clicked)
	{
		if (clicked)
			rotation = RotationClicked.Left;
		else
			rotation = RotationClicked.Idle;

	}

	public void Right (bool clicked)
	{
		if (clicked)
			rotation = RotationClicked.Right;
		else
			rotation = RotationClicked.Idle;

	}

	public void ZoomPlus ()
	{

	}

	public void ZoomMinus ()
	{

	}

}