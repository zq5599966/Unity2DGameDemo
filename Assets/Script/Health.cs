using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Health : MonoBehaviour {

	public int initHealth;
	public GameObject healthBarModel;
	public Vector2 healthBarOffset;
	int curHealth;
	Slider healthBar;
	Camera curCamera;
	void Awake()
	{
		curHealth = initHealth;
		curCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
		if(healthBarModel != null){
			var gobj = Instantiate(healthBarModel, transform.position, Quaternion.identity);
			gobj.transform.parent = transform;
			Vector3 barPos = gobj.transform.position;
			gobj.transform.position = new Vector3(barPos.x + healthBarOffset.x, barPos.y + healthBarOffset.y, 0);

			healthBar = gobj.GetComponentInChildren<Slider>();
			healthBar.value = 100;
		}
	}
	public void TakeDamage(int damage){
		curHealth -= damage;
		float percent = (float)(curHealth) / initHealth;
		healthBar.value = percent * 100;
		if(curHealth <= 0){
			Death();
		}
	}
	public void Death(){
		Destroy(gameObject);
	}
}
