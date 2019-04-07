using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasCtrl : MonoBehaviour
{

	public GameObject pauseCanvas;
	public GameObject optionPannel;

	public void PauseMenuToggle()
	{
		if(pauseCanvas.activeSelf == false)  // 일시정지가 아닐때
		{
			Time.timeScale = 0;  // 시간정지
			pauseCanvas.SetActive(true);
		}


		else if(pauseCanvas.activeSelf == true)  // 일시정지일때
		{
			Time.timeScale = 1;  // 시간 원래대로
			pauseCanvas.SetActive(false);  // 일시정지 캔버스 비활성화
		}
	}

	public void OptionMenuToggle()
	{
		if(optionPannel.activeSelf == false && pauseCanvas.activeSelf == true)  // 옵션창이 없을때
		{
			pauseCanvas.SetActive(false);  // 일시정지 캔버스 비활성화
			optionPannel.SetActive(true);  // 옵션 캔버스 활성화
		}

		else if(optionPannel.activeSelf == true && pauseCanvas.activeSelf == false)  // 옵션창이 있을때
		{
			optionPannel.SetActive(false);  // 옵션 캔버스 비활성화
			pauseCanvas.SetActive(true);  // 일시정지 캔버스 활성화
		}
	}
}
