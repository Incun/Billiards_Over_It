using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Text_iTween : MonoBehaviour
{
	private void OnEnable()
	{
		gameObject.transform.localScale = new Vector3(0.05f, 0.05f, 0.05f);
		iTween.ScaleBy(gameObject, iTween.Hash("x", 30, "y", 30, "easeType", "easeOutBack", "delay", .1));
		iTween.RotateBy(gameObject, iTween.Hash("z", 0.1f, "easeType", "easeInOutCubic", "loopType", "pingPong", "Time", 1));
	}
}
