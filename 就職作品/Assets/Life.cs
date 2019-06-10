using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Life : MonoBehaviour
{
    [SerializeField] private int life;
    private float frameTime;

    // Start is called before the first frame update
    void Start()
    {
        frameTime = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Damage()
    {
        if (Time.time == frameTime) return;
        frameTime = Time.time;
        life--;
    }

    public void Increase()
    {
        if (Time.time == frameTime) return;
        frameTime = Time.time;
        life++;
    }

    public bool IsDie()
    {
        if (life <= 0) return true;

        return false;
    }
}
