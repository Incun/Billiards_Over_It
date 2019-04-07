using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarCtrl : MonoBehaviour
{
	public static StarCtrl instance;

	SpriteRenderer sr;
	Color color;
	Collider2D boxCol;

	private void Start()
	{
		sr = GetComponent<SpriteRenderer>();
		boxCol = GetComponent<BoxCollider2D>();
	}

	private void OnCollisionEnter2D(Collision2D col)
	{
		if(col.gameObject.tag.Equals("Ball"))
		{
			StartCoroutine(StarFadeOut());
		}
	}



	// Coroutine
	public IEnumerator StarFadeOut()
	{

		GameManager.instance.demandStar--;  // 요구 별 갯수 감소
		boxCol.enabled = false;  // Star의 박스콜라이더 Off
		color.a = 1;  // Color알파값 초기화
		while (true)
		{
			if(sr.color.a < 0)
			{
				gameObject.SetActive(false);
				yield break;
			}
			color.a -= 0.05f;
			sr.color = color;
			yield return GameManager.instance.fixupdate;
		}
	}
}
