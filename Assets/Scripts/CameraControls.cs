using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControls : MonoBehaviour
{
	[Header("Basics")]
	public float dragSpeed = 2;
	private Vector2 dragOrigin;
	private Vector2 previousMousePos;
	private Vector2 mousePos;
	private bool dragging;

	[Header("Smoothness")]
	public float decelerationRate = 0.01f;
	public Vector2 currentSpeed;



	void LateUpdate()
	{
		mousePos = new Vector2 (Input.mousePosition.x, Input.mousePosition.y);

		if (Input.GetMouseButtonDown(0))
		{
			dragging = true;
			dragOrigin = mousePos;
			previousMousePos = dragOrigin;
			return;
		}

		if (!Input.GetMouseButton(0)) 
		{
			dragging = false;	
		}


		if (dragging)
		{
			currentSpeed = (previousMousePos-mousePos);
			previousMousePos = mousePos;
		}
		else
		{
			//currentSpeed -= new Vector2(decelerationRate*Mathf.Sign(currentSpeed.x),decelerationRate);
			float mag = currentSpeed.magnitude;
			Vector2 temp = currentSpeed.normalized;
			mag *= decelerationRate;
			currentSpeed = temp*Mathf.Clamp(mag,0,Mathf.Infinity);

		}

		this.transform.Translate(currentSpeed*dragSpeed);

	}


}