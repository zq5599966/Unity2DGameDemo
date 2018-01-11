using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour {
	public float moveSpeed = 20f;
	public LayerMask colliderLayers;
	int faceDir = 1;
	public void initBulletParam(bool faceRight, Direction dir){
		faceDir = faceRight ? 1 : -1;
		switch(dir){
			case Direction.UP:
				transform.rotation = Quaternion.Euler(0, 0, 90);
				break;
			case Direction.DOWN:
				transform.rotation = Quaternion.Euler(0, 0, -90);
				break;
			case Direction.RIGHT:
				transform.rotation = Quaternion.identity;
				break;
			case Direction.LEFT:
				transform.rotation = Quaternion.Euler(0, 0, 180);
				break;
		}
	}
	void FixedUpdate()
	{
		transform.Translate(Vector3.right * moveSpeed * Time.deltaTime);
	}
	void OnTriggerEnter2D(Collider2D other)
	{
		if((colliderLayers & 1 << other.gameObject.layer) == 0){
			return;
		}
		Destroy(gameObject);
	}
}
