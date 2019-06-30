using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private Life life;

    // Start is called before the first frame update
    void Start()
    {
        life = GetComponent<Life>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag.Equals("Ball"))
        {
            // ダメージを受ける
            Ball ball = collision.gameObject.GetComponent<Ball>();
            if(ball.IsThrown())
            {
                life.Damage();
            }
        }
    }
}
