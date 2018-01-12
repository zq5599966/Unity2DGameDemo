using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ExtendList {
	public static T Shift<T>(this List<T> list){
		T obj = list[0];
		list.RemoveAt(0);
		return obj;
	}

	public static T Pop<T>(this List<T> list){
		int lastIndex = list.Count - 1;
		T obj = list[lastIndex];
		list.RemoveAt(lastIndex);
		return obj;
	}
}
