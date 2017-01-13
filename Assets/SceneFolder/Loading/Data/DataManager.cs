using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

using System.IO;

public class DataManager : MonoBehaviour
{

    public static DataManager Instance = null;

    public string sceneToLoad = "";

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
           
            // 씬이 바뀌어도 현재 게임오브젝트를 삭제하지 않도록 설정.
            DontDestroyOnLoad(gameObject);
        }

        // 이미 만들어진 Manager가 있는 경우에는 더이상 만들지 않도록 게임 오브젝트 삭제.
        else
        {
            Destroy(gameObject);
        }
    }

    public void ChangeScene(string scene)
    {
        sceneToLoad = scene;
        SceneManager.LoadScene("Loading_Scene");
    }
}
