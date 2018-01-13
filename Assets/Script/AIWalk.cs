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
	void Update()
	{
		if(_controller){
			CheckIsChangeDirection();
			_controller.SetHorizontalMovement(moveDir * moveSpeed);
		}
	}
	void CheckIsChangeDirection(){
		if(_controller.getIsColliderWall() || isMoveBoundary()){
			_controller.Flip();
			moveDir = _controller.isFaceRight ? 1 : -1;
		}
	}
	bool isMoveBoundary(){
		if(moveOffset.x == 0 && moveOffset.y == 0){
			return false;
		}
		return transform.position.x > initPos.x + moveOffset.y || transform.position.x < initPos.x - moveOffset.x;
	}
}
