using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UniRx;
using UniRx.Triggers;

public class SceneController : MonoBehaviour
{
    private static SceneController instance;                // シングルトンのインスタンス

    /// <summary>
    /// インスタンス
    /// </summary>
    public static SceneController Instance
    {
        get
        {
            if (instance == null)
            {
                GameObject obj = new GameObject("SceneController");
                instance = obj.AddComponent<SceneController>();

                Instantiate(obj);
                obj.AddComponent<FadeManager>();
            }
            return instance;
        }

        set { }        
    }

    /// <summary>
    /// シーン遷移
    /// </summary>
    /// <param name="sceneName">遷移するシーン名</param>
    /// <param name="fadeTime">遷移時のフェード時間</param>
    public void ChangeScene(string sceneName, float fadeTime = 0.0f)
    {
        this.UpdateAsObservable()
            .First()                                      // 1回だけ実行
            .Subscribe(l => Execute(sceneName,fadeTime)); // シーン遷移
    }

    public void Execute(string sceneName, float fadeTime = 0.0f)
    {        
        FadeManager.Instance.LoadScene(sceneName, fadeTime);
    }
}
