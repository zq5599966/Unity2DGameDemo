using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIShoot : MonoBehaviour {

	public int shootDistance;
	PlayerController _controller;
	void Awake()
	{
		_controller = GetComponent<PlayerController>();
	}
	void CheckShootRange(){
	    float rangePoint = _controller.isFaceRight ? transform.position.x + shootDistance : transform.position.x - shootDistance;
		Vector3 newPos = new Vector3(rangePoint, transform.position.y, transform.position.z);
		Debug.DrawLine(transform.position, newPos, Color.yellow);
		RaycastHit2D rayHit = Physics2D.Linecast(transform.position, newPos, _controller.colliderLayers);
		if(rayHit && rayHit.collider.CompareTag("Player")){
			_controller.ShootStart();
		}
	}
	void Update () {
		if(_controller){
			int horDirection = _controller.isFaceRight ? 1 : -1;
			_controller.SetDirection(horDirection, 0);
			CheckShootRange();
		}
	}
}
