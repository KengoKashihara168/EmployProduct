using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Light light;
    private Life life;

    // Start is called before the first frame update
    void Start()
    {
        life = GetComponent<Life>();
    }

    // Update is called once per frame
    void Update()
    {
        if(life.IsDie())
        {
            Debug.Log("GameOver");
        }
    }

    public void HitItem(string tag)
    {
        switch(tag)
        {
            case "Light":
                // 光が強くなる
                LightPowerUp();
                break;
            case "Heal":
                // ライフが回復する
                life.Increase();
                break;
        }
    }

    private void LightPowerUp()
    {
        light.range += 2;        
        light.transform.position += new Vector3(0.0f, 0.0f, -0.5f);
        Debug.Log(light.range);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {       
        if(collision.gameObject.tag.Equals("Enemy"))
        {
            life.Damage();
        }
    }
}
