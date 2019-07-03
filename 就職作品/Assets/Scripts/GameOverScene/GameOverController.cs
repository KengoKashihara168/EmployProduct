using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverController : MonoBehaviour
{
    private readonly float SceneCangeSecond = 3.0f; // シーン遷移開始までの時間

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Mathf.Approximately(SceneCangeSecond - Time.time,0.0f))
        {
            Debug.Log("シーン遷移");
            //SceneController.Instance.ChangeScene("GameScene", 1.0f);
        }
    }
}
