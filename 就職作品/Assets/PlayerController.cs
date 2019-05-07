using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    new Light light;

    // Start is called before the first frame update
    void Start()
    {
        for(int i = 0;i < transform.childCount;i++)
        {
            AddLight(transform.GetChild(i));
        }        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LightPowerUp()
    {
        light.range += 2;        
        light.transform.position += new Vector3(0.0f, 0.0f, -0.5f);
        //Debug.Log(light.range);
    }

    void AddLight(Transform trans)
    {
        if (trans.tag == "Light")
        {
            light = trans.GetComponent<Light>();
        }
    }
}
