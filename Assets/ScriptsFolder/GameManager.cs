using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

using System.IO;

public enum State
{
    idle, move, attack, died,test
}
public class GameManager : MonoBehaviour
{

    public State state;
    public Camera uiCam;
    public GameObject frameCheck;
    public string sceneToLoad;

    public GameObject test_BackGround;

    public UIPanel ingame_PausePanel;
    void Awake()
    {
        Application.targetFrameRate = 60;
       // frameCheck.gameObject.SetActive(false);
        state = State.idle;
        StartCoroutine(FSM());
    }
    IEnumerator FSM()
    {
        while (true)
        {
            yield return StartCoroutine(state.ToString());
        }
    }
    IEnumerator idle()
    {
        if (state == State.idle)
        {
            RayCast();
        }
        yield return new WaitForEndOfFrame();
    }
    void RayCast()
    {
        if (0 < Input.touchCount)
        {
            for (int i = 0; i < Input.touchCount; i++)
            {
                Ray ray = uiCam.ScreenPointToRay(Input.GetTouch(i).position);
                RaycastHit hit2;
                if (Input.touchCount > 0 && Input.touchCount <= 1)
                {
                    if (Physics.Raycast(ray, out hit2, 300.0f))
                    {
                        switchOn(hit2);
                    }
                }
                else if (Input.touchCount > 1 && Input.touchCount <= 2)
                {
                    if (Physics.Raycast(ray, out hit2, 300.0f))
                    {
                        switchOn(hit2);
                    }
                }
            }
        }
    }
    void switchOn(RaycastHit hit)
    {
        switch (hit.collider.gameObject.tag)
        {
            case "LButton":
                break;
            case "RButton":
                break;
        }
    }
    public void FrameOn()
    {
        frameCheck.gameObject.SetActive(true);
    }
    public void FrameOff()
    {
        frameCheck.gameObject.SetActive(false);
    }
    public void GameStartButton()
    {
        DataManager.Instance.ChangeScene(sceneToLoad);
        Time.timeScale = 1;
    }
    public void IngameStart()
    {
        SceneManager.LoadScene(sceneToLoad);
    }
    public void GameQuit()
    {
        Application.Quit();
    }
    public void InGame_PauseButton()
    {
        ingame_PausePanel.gameObject.SetActive(true);
        Time.timeScale = 0;
    }
    public void InGame_Yes()
    {
        DataManager.Instance.ChangeScene(sceneToLoad);
        Time.timeScale = 1;
    }
    public void InGame_No()
    {
        ingame_PausePanel.gameObject.SetActive(false);
        Time.timeScale = 1;
    }

    public bool testtest;
    
    public void Ground_Test()
    {
        if (testtest == false)
        {
            test_BackGround.gameObject.SetActive(true);
            Debug.Log("켜짐");
            testtest = true;
            return;
        }
        else
        {
            test_BackGround.gameObject.SetActive(false);
            testtest = false;
            Debug.Log("꺼짐");
        }
    }
}
