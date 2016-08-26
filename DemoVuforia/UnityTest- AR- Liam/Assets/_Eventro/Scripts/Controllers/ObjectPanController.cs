using UnityEngine;
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
	private float rotateSpeed = 2, zoomSpeed = 100;
	private bool canZoom = false, wantTowardsScreen = false;
	private Vector3 updatedPos;

	[HideInInspector]
	public GameObject objectToBePan;

	public Transform target;

	// Use this for initialization
	void Start ()
	{
	}

	internal void SetCubePos ()
	{
		updatedPos = objectToBePan.transform.position;
	}
	
	// Update is called once per frame
	void Update ()
	{
		Rotate ();
		Zoom ();
	}

	private void Zoom ()
	{
		if (canZoom) {
			MoveCube ();
		}
	}

	private void MoveCube ()
	{
		Vector3 directionOfTravel;
		
		if (Vector3.Distance (objectToBePan.transform.position, target.position) > .1f) { 
			if (wantTowardsScreen)
				directionOfTravel = target.position - objectToBePan.transform.position;
			else
				directionOfTravel = target.position + objectToBePan.transform.position;
			directionOfTravel.Normalize ();
			objectToBePan.transform.Translate (
				(directionOfTravel.x * zoomSpeed * Time.deltaTime),
				(directionOfTravel.y * zoomSpeed * Time.deltaTime),
				(directionOfTravel.z * zoomSpeed * Time.deltaTime),
				Space.World);
		}
	}


	void Rotate ()
	{
		switch (rotation) {
		case RotationClicked.Up:
			RotationHori (-rotateSpeed);
			break;
		case RotationClicked.Down:
			RotationHori (rotateSpeed);
			break;
		case RotationClicked.Left:
			RotationVer (rotateSpeed);
			break;
		case RotationClicked.Right:
			RotationVer (-rotateSpeed);
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

	public void ZoomPlus (bool clicked)
	{
		canZoom = clicked;
		wantTowardsScreen = true;
	}

	public void ZoomMinus (bool clicked)
	{
		wantTowardsScreen = false;
		canZoom = clicked;
	}

}
