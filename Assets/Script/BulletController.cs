using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour {
	public float moveSpeed = 20f;
	public int damage;
	public LayerMask colliderLayers;
	BulletPool pool;

	void FixedUpdate()
	{
		transform.Translate(Vector3.right * moveSpeed * Time.deltaTime);
	}
	void OnTriggerEnter2D(Collider2D other)
	{
		if((colliderLayers & 1 << other.gameObject.layer) == 0){
			return;
		}
		DestoryBullet(other.gameObject);
	}
	public void setPool(BulletPool pool){
		this.pool = pool;
	}

	void DestoryBullet(GameObject obj){
		Health health = obj.GetComponent<Health>();
		if(health){
			health.TakeDamage(damage);
		}
		pool.BulletDestory(gameObject);
	}
}
