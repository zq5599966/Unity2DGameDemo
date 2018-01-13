using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputController : MonoBehaviour {

	PlayerController player;
	void Awake()
	{	
		GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
		player = playerObject.GetComponent<PlayerController>();
	}
	void Update () {
		float horValue = Input.GetAxis("Horizontal");
		player.SetHorizontalMovement(horValue);
		float verValue = Input.GetAxis("Vertical");
		player.SetDirection(horValue, verValue);
		if(Input.GetButtonDown("Jump")){
			player.StartJump();
		}
		if(Input.GetButtonDown("Fire1")){
			player.ShootOnce();
		}
		if(Input.GetButton("Fire1")){
			player.ShootStart();
		}
	}
}
