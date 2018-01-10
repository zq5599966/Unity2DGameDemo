using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

	public float moveSpeed = 8;
	public LayerMask colliderLayers;
	Vector2 _speed;
	Vector2 _moveStepPos;
	bool isFaceRight;
	BoxCollider2D colliderBox;
	Vector2[] topColliderPoints, bottomColliderPoints, leftColliderPoints, rightColliderPoints;	//check dir collider points;
	float colliderOffsetValue = 0.01f;
	
	void Awake()
	{
		isFaceRight = true;
		colliderBox = GetComponent<BoxCollider2D>();
		topColliderPoints = new Vector2[3];
		bottomColliderPoints = new Vector2[3];
		leftColliderPoints = new Vector2[3];
		rightColliderPoints = new Vector2[3];
	}
	void Update () {
		InputEvent();
	}
	void FixedUpdate()
	{
		ResetColliderParams();
		CheckColliderSide();
		CheckColliderBelow();
		// CheckColliderAbove();
		
		transform.Translate(_moveStepPos);
		Flip();
	}
	void InputEvent(){
		float horValue = Input.GetAxis("Horizontal");
		SetHorizontalMovement(horValue);
	}
	void SetHorizontalMovement(float horValue){
		_moveStepPos.x = _speed.x = moveSpeed * horValue * Time.deltaTime;
	}
	void Flip(){
		if(isFaceRight && _speed.x < 0 || !isFaceRight && _speed.x > 0){
			isFaceRight = !isFaceRight;
			var tmpScale = transform.localScale;
			tmpScale.x = -1 * tmpScale.x;
			transform.localScale = tmpScale;
		}
	} 
	void ResetColliderParams(){
		topColliderPoints[0] = new Vector2(colliderBox.bounds.center.x, colliderBox.bounds.max.y);
		topColliderPoints[1] = new Vector2(colliderBox.bounds.min.x + colliderOffsetValue, colliderBox.bounds.max.y);
		topColliderPoints[2] = new Vector2(colliderBox.bounds.max.x - colliderOffsetValue, colliderBox.bounds.max.y);
		bottomColliderPoints[0] = new Vector2(colliderBox.bounds.center.x, colliderBox.bounds.min.y);
		bottomColliderPoints[1] = new Vector2(colliderBox.bounds.min.x + colliderOffsetValue, colliderBox.bounds.min.y);
		bottomColliderPoints[2] = new Vector2(colliderBox.bounds.max.x - colliderOffsetValue, colliderBox.bounds.min.y);
		leftColliderPoints[0] = new Vector2(colliderBox.bounds.min.x, colliderBox.bounds.center.y);
		leftColliderPoints[1] = new Vector2(colliderBox.bounds.min.x, colliderBox.bounds.min.y + colliderOffsetValue);
		leftColliderPoints[2] = new Vector2(colliderBox.bounds.min.x, colliderBox.bounds.max.y - colliderOffsetValue);
		rightColliderPoints[0] = new Vector2(colliderBox.bounds.max.x, colliderBox.bounds.center.y);
		rightColliderPoints[1] = new Vector2(colliderBox.bounds.max.x, colliderBox.bounds.min.y + colliderOffsetValue);
		rightColliderPoints[2] = new Vector2(colliderBox.bounds.max.x, colliderBox.bounds.max.y - colliderOffsetValue);
	}
	void CheckColliderSide(){
		for(int i = 0; i < leftColliderPoints.Length; i++){
			Vector2 leftPoint = leftColliderPoints[i];
			Vector2 rightPoint = rightColliderPoints[i];
			Debug.DrawLine(leftPoint, leftPoint - (Vector2)transform.right, Color.black);
			Debug.DrawLine(rightPoint, rightPoint + (Vector2)transform.right, Color.black);
			if(_speed.x < 0){
				RaycastHit2D leftRayHit = Physics2D.Raycast(leftPoint, -transform.right, Mathf.Abs(_moveStepPos.x), colliderLayers);
				if(leftRayHit){
					_speed.x = 0;
					_moveStepPos.x = -leftRayHit.distance;
					break;
				}
			}
			else if(_speed.x > 0){
				RaycastHit2D rightRayHit = Physics2D.Raycast(rightPoint, transform.right, Mathf.Abs(_moveStepPos.x), colliderLayers);
				if(rightRayHit){
					_speed.x = 0;
					_moveStepPos.x = rightRayHit.distance;
					break;
				}
			}
		}
	}
	void CheckColliderBelow(){
		_speed.y += Physics2D.gravity.y * Time.deltaTime;
		_moveStepPos.y = _speed.y * Time.deltaTime;
		for(int i = 0; i < bottomColliderPoints.Length; i++){
			Vector2 point = bottomColliderPoints[i];
			Debug.DrawLine(point, point - (Vector2)(transform.up * 1f), Color.red);
			RaycastHit2D rayHit = Physics2D.Raycast(point, -transform.up, Mathf.Abs(_moveStepPos.y), colliderLayers);
			if(rayHit){
				_speed.y = 0;
				_moveStepPos.y = -rayHit.distance;
				break;
			}
		}
	}

	
}
