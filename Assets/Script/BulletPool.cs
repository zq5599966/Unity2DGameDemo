using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletPool : MonoBehaviour {

	public GameObject cacheBullet;
	public int cacheNum;

	List<GameObject> bulletPoor;
	
	void Awake()
	{
		bulletPoor = new List<GameObject>();
		for(int i = 0; i < cacheNum; i++){
			CreateNewBulletInCache();
		}
	}
	GameObject CreateNewBulletInCache(){
		GameObject bullet = Instantiate(cacheBullet, transform.position, Quaternion.identity);
		bullet.SetActive(false);
		BulletController bc = bullet.GetComponent<BulletController>();
		bc.setPool(this);
		bulletPoor.Add(bullet);
		return bullet;
	}
	public void Shoot(Vector3 position, Direction dir){
		GameObject bullet;
		if(bulletPoor.Count > 0){
			bullet = bulletPoor.Shift();
			if(!bullet || bullet.activeSelf){
				bullet = CreateNewBulletInCache();
			}
		}
		else{
			bullet = CreateNewBulletInCache();
		}
		bullet.SetActive(true);
		setBulletTransform(bullet, position, dir);
	}

	void setBulletTransform(GameObject bullet, Vector3 position, Direction dir){
		bullet.transform.position = position;
		switch(dir){
			case Direction.UP:
				bullet.transform.rotation = Quaternion.Euler(0, 0, 90);
				break;
			case Direction.DOWN:
				bullet.transform.rotation = Quaternion.Euler(0, 0, -90);
				break;
			case Direction.RIGHT:
				bullet.transform.rotation = Quaternion.identity;
				break;
			case Direction.LEFT:
				bullet.transform.rotation = Quaternion.Euler(0, 0, 180);
				break;
		}
	}

	public void BulletDestory(GameObject obj){
		obj.SetActive(false);
		bulletPoor.Add(obj);
	}
}
