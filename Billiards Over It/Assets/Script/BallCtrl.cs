using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class BallCtrl : MonoBehaviour
{
	Vector3 mouseCurPos;  // 마우스의 현재 좌표
	Vector3 mouseUpPos;  // 마우스를 뗀 좌표
	Vector3 mouseDownPos;  // 마우스를 클릭한 좌표

	Vector2 startPos;  // 공의 시작 좌표

	float maxPower = 5;  // 공을 치는 최대 힘

	public Vector3 swipeDis = Vector3.zero;  // 스와이프 거리 초기화
	public Vector3 reflectVector = Vector3.zero;  // 반사벡터 초기화

	public float curSpeed;  // 현재 속도
	public float power = 0;  // 공을 치는 힘
	public float tempP;  // 임시 힘
	public bool isMoving = false;  // 공이 움직이고 있는지
	public bool isOnBtn;  // 마우스가 버튼 위에 있는지

	Rigidbody2D rb;  // 공의 Rigidbody2D
	SpriteRenderer sr;  // 공의 SpriteRenderer

	bool timecheck = true;

	public GameObject pauseCanvas;  // 일시정지 Canvas
	public GameObject optionCanvas;  // 옵션 Canvas

	public IEnumerator BallCoroutine;  // 볼 코루틴
	
	public BGMCtrl bgm_c;
	public SFXCtrl sfx_c;
	public StarCtrl[] sc = new StarCtrl[0];

	void Start()
	{
		rb = gameObject.GetComponent<Rigidbody2D>();
		sr = gameObject.GetComponent<SpriteRenderer>();


	}

	void Update()
	{
		if(mouseDownPos == mouseUpPos)
		{
			mouseDownPos = Vector3.zero;
			mouseUpPos = Vector3.zero;
		}
		if (mouseCurPos.x > 6.2 && mouseCurPos.x < 9 && mouseCurPos.y > 3.7 && mouseCurPos.y < 5)
		{
			isOnBtn = true;
			if(mouseDownPos != Vector3.zero)
			{

			}
		}  // 마우스가 버튼 위에 있는지
		else if (mouseCurPos.x > -1.7 && mouseCurPos.x < 1.7 && mouseCurPos.y > -5 && mouseCurPos.y < -3.7)
		{
			isOnBtn = true;
		}  // 마우스가 버튼 위에 있는지
		else
		{
			isOnBtn = false;
		}  // 마우스가 버튼 위에 있는지


		SwipeBall();

		if (rb.velocity.magnitude != 0)
		{
			isMoving = true;
		}  // 공이 움직일때
		else
		{
			isMoving = false;
		}  // 공이 멈춰있을때
	}

	void OnCollisionEnter2D(Collision2D col)
	{
		sfx_c.sfx.Stop();
		sfx_c.sfx.Play();
		// 입사벡터를 알아본다. (충돌할때 충돌한 물체의 입사 벡터 노말값)
		Vector3 incomingVector = col.contacts[0].point - startPos;
		incomingVector = incomingVector.normalized;
		// 충돌한 면의 법선 벡터를 구해낸다.
		Vector3 normalVector = col.contacts[0].normal;
		// 법선 벡터와 입사벡터을 이용하여 반사벡터를 알아낸다.
		reflectVector = Vector3.Reflect(incomingVector, normalVector); //반사각
		reflectVector = reflectVector.normalized;
		startPos = gameObject.transform.position;

		if (BallCoroutine != null)  // 볼 코루틴이 실행중이면 정지시키기
		{
			StopCoroutine(BallCoroutine);
		}
		
		curSpeed = curSpeed * 0.85f;  // 벽과 충돌시 속도 15%% 감소
		
		BallCoroutine = BallMove(reflectVector, curSpeed);  // 반사벡터와 10% 감소된 속도로 볼  코루틴 할당
		StartCoroutine(BallCoroutine);  // 볼 코루틴 실행
	}  // 공이 콜라이더에 충돌했을 때

	void OnTriggerEnter2D(Collider2D col)
	{
		if(col.gameObject.tag.Equals("Hole"))
		{
			StartCoroutine(BallFadeOut());
		}
	}




	void SwipeBall()
	{
		if (isMoving == false && pauseCanvas.activeSelf == false && optionCanvas.activeSelf == false && GameManager.instance.curDrag < GameManager.instance.maxDrag)  // 공이 정지해있고 일시정지Canvas가 Off일때
		{
			mouseCurPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);  // 현재 마우스 좌표
			//line.SetPosition(1, mouseCurPos);
			if (mouseDownPos != Vector3.zero)  // 마우스를 클릭한 좌표가 원점이 아닐 때
			{
				power = Vector3.Distance(mouseDownPos, mouseCurPos);
				if (power >= maxPower)  // 최대파워 고정
				{
					power = maxPower;
				}
				tempP = power;
				power = power * 200;  // 공 치는 힘
			}

			if (Input.GetMouseButtonDown(0) && isOnBtn == false)  // 버튼위에있지 않고 마우스가 클릭되었을때
			{
				mouseDownPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);  // 마우스가 클릭된 좌표
			}

			else if (Input.GetMouseButtonUp(0) && mouseDownPos != Vector3.zero)
			{
				mouseUpPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);  // 마우스를 뗀 곳의 좌표
				swipeDis = mouseDownPos - mouseUpPos;  // 마우스를 클릭한 곳의 좌표 - 마우스를 뗀 곳의 좌표(방향벡터)

				swipeDis = swipeDis.normalized;  // 노멀라이즈
				if(swipeDis != Vector3.zero)  // 제자리 방지
				{
					startPos = gameObject.transform.position;  // 원래 공의 위치

					if (BallCoroutine != null)  // 볼 코루틴 정지
					{
						StopCoroutine(BallCoroutine);
					}

					BallCoroutine = BallMove(swipeDis, power);  // 방향벡터와 파워로 공을 친다
					StartCoroutine(BallCoroutine);  // 코루틴 실행
					power = 0;  // 힘 초기화
					mouseDownPos = Vector3.zero;  // 마우스 누른 좌표 초기화
					mouseUpPos = Vector3.zero;  // 마우스 뗀 좌표 초기화
					GameManager.instance.curDrag++;
				}
			}
		}
	}


	// Coroutine

	public IEnumerator BallMove(Vector3 dis, float speed)
	{
		while (true)
		{
			while (pauseCanvas.activeSelf == false)  // 일시정지 Canvas와 옵션 Canvas가 없을 때
			{
				rb.velocity = dis * speed * Time.deltaTime;  // 공의 속도 = 방햑 * 속도 * Time.deltaTime
				speed = speed * 0.99f;  // 공의 속도 저하

				if (speed < 15)  // 속도가 20 미만이면
				{
					rb.velocity = Vector2.zero;  // 공 정지
					speed = 0;  // 속도 0으로 초기화
					curSpeed = speed;  // 현재 속도 = 속도
					if(GameManager.instance.curDrag >= GameManager.instance.maxDrag)
					{
						StartCoroutine(GameManager.instance.OverPanelFadeIn());
					}
					yield break;  // 코루틴 종료
				}
				curSpeed = speed;  // 현재 속도 = 속도
				yield return GameManager.instance.fixupdate;  // 고정된 시간만큼 기다림
			}
			yield return null;  // 1프레임 기다림
		}
	}  // 공 이동 코루틴

	public IEnumerator BallFadeOut()
	{
		GameManager.instance.isclear = 1;
		Color color = sr.color;
		while (true)
		{
			StopCoroutine(BallCoroutine);
			rb.velocity = Vector3.zero;
			color.a = color.a - 0.05f;
			sr.color = color;
			if(color.a <= 0)
			{
				PlayerPrefs.SetInt(SceneManager.GetActiveScene().buildIndex.ToString(), GameManager.instance.isclear);
				StartCoroutine(GameManager.instance.ClearPanelFadeIn());
				yield break;
			}
			yield return GameManager.instance.fixupdate;
		}
	}  // 공 FadeOut 코루틴
}
