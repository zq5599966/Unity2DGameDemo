using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {
	public Transform target;
	public float smoothTime = 0.3f;
	
	Vector3 _currentVelocity;

	void LateUpdate()
	{
		if(target){
			Vector3 newPos = Vector3.SmoothDamp(transform.position, target.position, ref _currentVelocity, smoothTime);
			newPos.z = transform.position.z;
			transform.position = newPos;
		}
	}
}
