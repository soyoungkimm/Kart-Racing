using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager ManagerIns = null; // 싱글턴 객체(GameManager 대표 객체)   

    GameObject scorePan;
    GameObject menu;
    public AudioClip countSound;
    public AudioClip GoSound;
    public AudioClip goalSound;
    AudioSource adio;
    public Text currentScoreTxt;
    public Text bestScoreTxt;
    public Text countDownTxt;
    float currentScore;
    float bestScore;
    string cScoreStr;
    string bScoreStr;
    public bool isUseGMFunc = false;
    bool isCheckpoint = false;

    void Awake()
    {
        if (ManagerIns == null) ManagerIns = this;
    }

    private void Start()
    {
        scorePan = GameObject.Find("Canvas").transform.Find("ScorePan").gameObject;
        scorePan.SetActive(false);
        Timer.time = 0f;
        adio = gameObject.GetComponent<AudioSource>();
        menu = GameObject.Find("Canvas").transform.Find("Menu").gameObject;
        StartCoroutine("ReadyCount"); // 게임 시작할 때 카운트다운
    }

    // 게임 끝나고 스코어 출력
    public void PrintScore()
    {
        isUseGMFunc = true;
        scorePan.SetActive(true);
        currentScore = Timer.time;
        bestScore = PlayerPrefs.GetFloat("BestScore", 0f); // 최고점수 불러오기
        if (currentScore < bestScore)
        {
            bestScore = currentScore;
            PlayerPrefs.SetFloat("BestScore", bestScore); // 최고점수 저장
        }
        cScoreStr = "" + currentScore.ToString("00.00");
        cScoreStr = cScoreStr.Replace(".", ":");

        bScoreStr = "" + bestScore.ToString("00.00");
        bScoreStr = bScoreStr.Replace(".", ":");

        bestScoreTxt.text = "Best : " + bScoreStr;
        currentScoreTxt.text = "Score : " + cScoreStr;
    }

    // 편법 방지를 위해 체크 포인트를 하나 생성해둠
    public void CheckPoint()
    {
        isCheckpoint = true;
        Debug.Log("CheckPoint!");
    }

    // 골인
    public void Goal()
    { 
        if (isCheckpoint)
        {
            isUseGMFunc = true;
            adio.clip = goalSound;
            adio.Play();
            PrintScore();
        }
    }

    // 카운트 다운 코루틴
    IEnumerator ReadyCount()
    {
        isUseGMFunc = true;
        for (int i = 3; i >= 1; i--)
        {
            countDownTxt.text = i.ToString();
            adio.clip = countSound;
            adio.Play();
            yield return new WaitForSeconds(1.0f);
            if (i == 1)
            {
                countDownTxt.text = "Go!";
                adio.clip = GoSound;
                adio.Play();
                yield return new WaitForSeconds(0.5f);
                countDownTxt.gameObject.SetActive(false);
            }
        }
        isUseGMFunc = false;
    }

    // 게임 도중 메뉴창
    public void ManageMenu()
    {
        if (menu.activeSelf)
        {
            menu.SetActive(false);
            isUseGMFunc = false;
        }
        else
        {
            menu.SetActive(true);
            isUseGMFunc = true; // 카운트 멈추기, 모든 동작 멈추기
        }
        
    }
}
