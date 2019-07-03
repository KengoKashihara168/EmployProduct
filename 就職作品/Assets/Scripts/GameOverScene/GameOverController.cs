using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverController : MonoBehaviour
{
    private int sceneCangeCount;
    private readonly int SceneCangeTime = 120; // シーン遷移開始までの時間

    // Start is called before the first frame update
    void Start()
    {
        sceneCangeCount = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if(sceneCangeCount == SceneCangeTime)
        {
            SceneController.Instance.ChangeScene("TitleScene", 1.0f);
        }

        sceneCangeCount++;
    }
}
