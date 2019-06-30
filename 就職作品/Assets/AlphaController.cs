using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlphaController : MonoBehaviour
{
    [SerializeField] private SpriteRenderer spriteRenderer;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /// <summary>
    /// 透明度だけを変更
    /// </summary>
    /// <param name="alpha">変更する透明度</param>
    /// <returns>変更を適応したColor</returns>
    public void ChangeAlpha(float alpha)
    {
        // 元々の色情報
        Color color = spriteRenderer.material.color;
        // 透明度だけ変更する
        color.a = alpha;
        spriteRenderer.material.color = color;
    }
}
