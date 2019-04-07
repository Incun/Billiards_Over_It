using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThinkingiTween : MonoBehaviour
{
	private void OnEnable()
	{
		iTween.RotateBy(gameObject, iTween.Hash("z", 2f, "easeType", "easeInOutCubic", "loopType", "pingPong", "Time", 1));
	}
}
