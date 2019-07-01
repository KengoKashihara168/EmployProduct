using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightController : MonoBehaviour
{
    [SerializeField] private Light myLight;

    private const float DefoultIntensity = 10.0f;

    /// <summary>
    /// ライトエネルギー取得
    /// </summary>
    public void PowerUp()
    {
        // ライトが照らす範囲を広げて少し離す
        myLight.range += 2;
        myLight.transform.position += new Vector3(0.0f, 0.0f, -0.5f);
    }

    /// <summary>
    /// 光量を変更
    /// </summary>
    /// <param name="intensity">変更する光量</param>
    public void ChangeIntensity(float intensity)
    {
        myLight.intensity = intensity;
    }

    public void RestoreIntensity()
    {
        myLight.intensity = DefoultIntensity;
    }
}
