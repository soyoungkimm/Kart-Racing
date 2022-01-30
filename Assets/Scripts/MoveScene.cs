using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MoveScene : MonoBehaviour
{
    public void GoToGameScene()
    {
        SceneManager.LoadScene("GameScene");
    }

    public void Exit()
    {
        Application.Quit();
    }

    public void GoToMain()
    {
        SceneManager.LoadScene("StartScene");
    }


}
