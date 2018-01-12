using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Direction{
	UP,
	DOWN,
	LEFT,
	RIGHT,
	UP_RIGHT,
	UP_LEFT,
	DOWN_RIGHT,
	DOWN_LEFT
}

public class PlayerController : MonoBehaviour {
	public float moveSpeed = 8;
	public float jumpHeight = 3.0f;
	public LayerMask colliderLayers;
	public Transform shootSpawn;
	public GameObject bullet;
	Vector2 _speed;
	Vector2 _moveStepPos;
	public bool isFaceRight;
	bool isInGround;
	bool isjumping;
	BoxCollider2D colliderBox;
	Vector2[] topColliderPoints, bottomColliderPoints, leftColliderPoints, rightColliderPoints;	//check dir collider points;
	float colliderOffsetValue = 0.01f;
	Direction curDir;
	BulletPool bulletPool;
	void Awake()
	{
		isFaceRight = true;
		colliderBox = GetComponent<BoxCollider2D>();
		bulletPool = GetComponent<BulletPool>();
		topColliderPoints = new Vector2[3];
		bottomColliderPoints = new Vector2[3];
		leftColliderPoints = new Vector2[3];
		rightColliderPoints = new Vector2[3];
	}
	// void Update () {
	// 	InputEvent();
	// }
	// void LateUpdate()
	void FixedUpdate()
	{
		ResetColliderParams();
		CheckColliderSide();
		CheckColliderBelow();
		CheckColliderAbove();
		
		transform.Translate(_moveStepPos);
		if(isFaceRight && _speed.x < 0 || !isFaceRight && _speed.x > 0){
			Flip();
		}
		
	}
	void InputEvent(){
		float horValue = Input.GetAxis("Horizontal");
		SetHorizontalMovement(horValue);
		float verValue = Input.GetAxis("Vertical");
		SetDirection(horValue, verValue);

		if(Input.GetButtonDown("Jump")){
			StartJump();
		}

		if(Input.GetButtonDown("Fire1")){
			ShootOnce();
		}
	}
	public void StartJump(){
		_speed.y = Mathf.Sqrt(2 * jumpHeight * Mathf.Abs(Physics2D.gravity.y));
	}
	public void SetHorizontalMovement(float horValue){
		_moveStepPos.x = _speed.x = moveSpeed * horValue * Time.deltaTime;
	}
	public void SetDirection(float horValue, float verValue){
		if(horValue == 0 && verValue > 0){
			curDir = Direction.UP;
		}
		else if(horValue == 0 && verValue < 0){
			curDir = Direction.DOWN;
		}
		else if(verValue == 0){
			curDir = isFaceRight ? Direction.RIGHT : Direction.LEFT;
		}
	}
	public void Flip(){
		isFaceRight = !isFaceRight;
		var tmpScale = transform.localScale;
		tmpScale.x = -1 * tmpScale.x;
		transform.localScale = tmpScale;
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
	public GameObject model;
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
					// float angle = Vector2.Angle(rightRayHit.normal, transform.up);
					// model.transform.rotation = Quaternion.Euler(Vector3.forward * angle);
					// Debug.Log("collider side ======" + angle);
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
		if(_speed.y < 0){
			for(int i = 0; i < bottomColliderPoints.Length; i++){
				Vector2 point = bottomColliderPoints[i];
				Debug.DrawLine(point, point - (Vector2)transform.up, Color.red);
				RaycastHit2D rayHit = Physics2D.Raycast(point, -transform.up, Mathf.Abs(_moveStepPos.y), colliderLayers);
				if(rayHit){
					// float angle = Vector2.Angle(rayHit.normal, transform.up);
					// model.transform.rotation = Quaternion.Euler(Vector3.forward * angle);
					// Debug.Log("collider side ======" + angle);
					_speed.y = 0;
					_moveStepPos.y = -rayHit.distance + 0.01f;
					isInGround = true;
					isjumping = false;
					break;
				}
			}	
		}
	}
	void CheckColliderAbove(){
		if(_speed.y > 0){
			for(int i = 0; i < topColliderPoints.Length; i++){
				Vector2 point = topColliderPoints[i];
				Debug.DrawLine(point, point + (Vector2)transform.up, Color.blue);
				RaycastHit2D rayHit = Physics2D.Raycast(point, transform.up, Mathf.Abs(_moveStepPos.y), colliderLayers);
				if(rayHit && rayHit.collider.gameObject.layer != LayerMask.NameToLayer("OneWayPlatforms")){
					_speed.y = 0;
					_moveStepPos.y = rayHit.distance;
				}
			}
		}
	}
	public void ShootOnce(){
		// BulletController bc = Instantiate(bullet, shootSpawn.position, Quaternion.identity).GetComponent<BulletController>();
		// bc.initBulletParam(isFaceRight, curDir);
		bulletPool.Shoot(shootSpawn.position, curDir);
	}
}