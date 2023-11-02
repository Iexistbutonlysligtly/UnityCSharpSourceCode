using UnityEngine;
using System.Collections;

public class ThirdPersonCamera : MonoBehaviour
{

	public bool lockCursor;
	public float mouseSensitivity = 10;
	public Transform thirdPersonTarget;
	public Transform firstPersonTarget;
	public float dstFromTarget = 2;
	public float DistenceForFirstPerson = -30;
	float ThiredPersonDistenceHolder;
	public Vector2 pitchMinMax = new Vector2(-40, 85);

	public float rotationSmoothTime = .12f;


	Vector3 rotationSmoothVelocity;
	Vector3 currentRotation;


	public bool Active = true;
	public bool FirstPersonToggle;

	float yaw;
	float pitch;

	void Start()
	{

		ThiredPersonDistenceHolder = dstFromTarget;

		if (lockCursor)
		{
			Cursor.lockState = CursorLockMode.Locked;
			Cursor.visible = true;
		}
	}

	void LateUpdate()
	{
		if (Active)
        {

			if (Input.GetMouseButton(0))
			{

				yaw += Input.GetAxis("Mouse X") * mouseSensitivity;
				pitch -= Input.GetAxis("Mouse Y") * mouseSensitivity;
				pitch = Mathf.Clamp(pitch, pitchMinMax.x, pitchMinMax.y);

				currentRotation = Vector3.SmoothDamp(currentRotation, new Vector3(pitch, yaw), ref rotationSmoothVelocity, rotationSmoothTime);
				transform.eulerAngles = currentRotation;
			}
			

		


			if (FirstPersonToggle)
            {

				transform.position = firstPersonTarget.position - transform.forward * dstFromTarget;

			}

			if (!FirstPersonToggle)
			{

				transform.position = thirdPersonTarget.position - transform.forward * dstFromTarget;

			}


		}

		if (Input.GetKeyDown("space"))
		{
			IsActive();
		}


		if (Input.GetKeyDown(KeyCode.F))
		{
			FirstPersonT();
		}

		
	}


	void IsActive()
    {

	 if (!(Active))
        {


			Active = true;

			Cursor.lockState = CursorLockMode.Locked;
			Cursor.visible = false;

		}

		else if (Active)
        {

			Active = false;

			Cursor.lockState = CursorLockMode.Confined;
			Cursor.visible = true;

		}


	}


	void FirstPersonT()
    {

		

		if (FirstPersonToggle)
        {


			dstFromTarget = ThiredPersonDistenceHolder;
			FirstPersonToggle = false;

			


        }



		else if (!FirstPersonToggle)

		{

			dstFromTarget = DistenceForFirstPerson;
			FirstPersonToggle = true;

			

		}


	}


	

}