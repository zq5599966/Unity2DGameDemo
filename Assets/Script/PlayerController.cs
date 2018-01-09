using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

	public float moveSpeed = 8;

	Vector2 _speed;
	bool isFaceRight;
	void Awake()
	{
		isFaceRight = true;
	}
	void Update () {
		InputEvent();
	}
	void FixedUpdate()
	{
		transform.Translate(moveSpeed * _speed * Time.deltaTime);
		Flip();
	}
	void InputEvent(){
		float horValue = Input.GetAxis("Horizontal");
		SetHorizontalMovement(horValue);
	}
	void SetHorizontalMovement(float horValue){
		_speed.x = horValue;
	}
	void Flip(){
		if(isFaceRight && _speed.x < 0 || !isFaceRight && _speed.x > 0){
			isFaceRight = !isFaceRight;
			var tmpScale = transform.localScale;
			tmpScale.x = -1 * tmpScale.x;
			transform.localScale = tmpScale;
		}
	}
}
