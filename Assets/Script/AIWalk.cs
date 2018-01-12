using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIWalk : MonoBehaviour {
	public Vector2 moveOffset;
	public float moveSpeed;
	PlayerController _controller;
	Vector2 initPos;
	int moveDir;
	void Awake()
	{
		_controller = GetComponent<PlayerController>();
		initPos = transform.position;
		moveDir = _controller.isFaceRight ? 1 : -1;
	}
	void FixedUpdate()
	{
		Vector3 temp = transform.position;
		temp.x = temp.x - moveOffset.x;
		Debug.Log("transform right =========");
		Debug.DrawLine(transform.position, temp, Color.red);
		temp.x = transform.position.x + moveOffset.y;
		Debug.DrawLine(transform.position, temp, Color.red);
		if(_controller){
			if(transform.position.x >= initPos.x + moveOffset.y || transform.position.x <= initPos.x - moveOffset.x){
				_controller.Flip();
				moveDir = _controller.isFaceRight ? 1 : -1;
			}
			_controller.SetHorizontalMovement(moveDir * moveSpeed);
		}
	}
}
