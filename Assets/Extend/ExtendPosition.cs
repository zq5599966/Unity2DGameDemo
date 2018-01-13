using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public static class ExtendPosition {
	public static void SetPositionX(this Transform transform, float x){
		transform.position = new Vector3(x, transform.position.y, transform.position.z);
	}
	public static void SetPositionY(this Transform transform, float y){
		transform.position = new Vector3(transform.position.x, y, transform.position.z);
	}
	public static void SetPositionZ(this Transform transform, float z){
		transform.position = new Vector3(transform.position.x, transform.position.y, z);
	}
	public static Vector3 AddPositionX(this Transform transform, float x){
		Vector3 newPos = new Vector3(transform.position.x + x, transform.position.y, transform.position.z);
		return newPos;
	}
	public static Vector3 AddPositionY(this Transform transform, float y){
		Vector3 newPos = new Vector3(transform.position.x, transform.position.y + y, transform.position.z);
		return newPos;
	}
	public static Vector3 AddPositionZ(this Transform transform, float z){
		Vector3 newPos = new Vector3(transform.position.x, transform.position.y, transform.position.z + z);
		return newPos;
	}
}
