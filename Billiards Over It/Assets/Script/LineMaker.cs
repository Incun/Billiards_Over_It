using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class LineMaker : MonoBehaviour
{
	LineRenderer line;  // 라인렌더러
	Transform tf;  // Circle Alpha의 트랜스폰
	Vector3 mouseDownPos = Vector3.zero;  // 마우스 클릭 위치
	Vector3 mouseCurPos = Vector3.zero;  // 마우스 텐 위치
	float tempScale = 0;  // 임시 스케일
	
	public BallCtrl bc;  // BallCtrl 스크립트
	public GameObject CircleLine;  // CircleLine Sprite
	public GameObject CircleLineA;  // CircleLine Alpha Sprite
	public GameObject pauseCanvas;  // 일시정지 캔버스
	public GameObject optionCanvas;  // 옵션 캔버스
	void Start()
	{
		line = gameObject.GetComponent<LineRenderer>();  // 라인렌더러
		tf = CircleLineA.GetComponent<Transform>();  // CircleLina Alpha의 트랜스폼
	}
	void Update()
	{
		DragLine();
		if(mouseDownPos == Vector3.zero)  // 마우스가 원점에서 눌리면
		{
			tempScale = 0;  // 임시 스케일을 0으로
		}
		tf.localScale = new Vector3(tempScale, tempScale, 1);  // CircleLine Alpha의 스케일 할당
		tempScale = bc.tempP * 0.2f;  // 임시 스케일을 BallCtrl의 20%만큼 저장
		CalculateAngle(mouseCurPos, mouseDownPos);
	}


	//  ----Function----
	void DragLine()
	{
		if (bc.isMoving == false && pauseCanvas.activeSelf == false && optionCanvas.activeSelf == false && GameManager.instance.curDrag < GameManager.instance.maxDrag)
		{		// 일시정지와 옵션 캔버스가 꺼져있고 공이 정지해있을 때
			mouseCurPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);  // 마우스의 현재 좌표
			mouseCurPos = new Vector3(mouseCurPos.x, mouseCurPos.y, -2);  // 마우스의 현재 좌표 Z좌표 -1로 재정의
			if (Input.GetMouseButtonDown(0) && bc.isOnBtn == false)  // 공이 정지해있고 마우스를 클릭했을 때
			{
				tf.localScale = Vector3.zero;  // Circle의 스케일을 0으로
				CircleLine.SetActive(true);  // CircleLine 활성화
				CircleLineA.SetActive(true);  // CircleLine Alpha 활성화
				mouseDownPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);  // 마우스 클릭한 위치
				mouseDownPos = new Vector3(mouseDownPos.x, mouseDownPos.y, -2);  // Z좌표 -1로 재정의
			}
			if (Input.GetMouseButtonUp(0))  // 공이 정지해있고 최대 드래그횟수를 채우지 않고 마우스를 땠을 때
			{
				CircleLine.SetActive(false);  // CircleLine 비활성화
				CircleLineA.SetActive(false);  // CircleLine Alpha 비활성화
				mouseDownPos = Vector3.zero;  // 마우스 누른 위치 초기화
				mouseCurPos = Vector3.zero;  // 마우스 뗀 위치 초기화
				line.SetPosition(0, mouseDownPos);  // 라인 제거
				line.SetPosition(1, mouseCurPos);  // 라인 제거
				tf.localScale = Vector3.zero;  // 스케일 초기화
				tempScale = 0;
			}
			if (mouseDownPos != Vector3.zero)
			{
				line.SetPosition(0, mouseDownPos);  // 라인 그리기
				line.SetPosition(1, mouseCurPos);  // 라인 그리기
			}
		}
	}
	void CalculateAngle(Vector3 from, Vector3 to)
	{
		tf.rotation = Quaternion.Euler(0, 0, Quaternion.FromToRotation(Vector3.up, to - from).eulerAngles.z);
	}  // CircleLine Alpha의 회전
}
