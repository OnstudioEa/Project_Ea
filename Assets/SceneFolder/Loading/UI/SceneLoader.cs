using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour {

    // 화면에 씬 로딩 작업의 진행 상태를 보여줄 UI 컴포넌트.
    public Image progressar;
    public Text progressText;

    // 백그라운드 씬 로딩 작업 상태 정보를 받아올 변수.
    private AsyncOperation async;

    // Use this for initialization
    void Start()
    {
        async = SceneManager.LoadSceneAsync(DataManager.Instance.sceneToLoad);
    }

    // Update is called once per frame
    void Update()
    {
        // 씬 로딩 작업이 완료되지 않은 경우,
        if (async.isDone == false)
        {
            // 로딩 작업 %를 이미지 fillAmount 값에 연결.
            progressar.fillAmount = async.progress;

            // 화면에 몇 %인지 보여줄 텍스트 설정 후 값 할당.
            float fProgress = async.progress * 100f;
            int iProgress = (int)fProgress;
            progressText.text = iProgress.ToString() + "%";
        }

        // 씬 로딩 작업 완료된 경우,
        else
        {
            // 화면에 100% 로딩 상태 표시.
            progressar.fillAmount = 1f;
            progressText.text = "100%";
        }
    }
}
