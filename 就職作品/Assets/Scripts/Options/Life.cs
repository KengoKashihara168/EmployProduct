using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Life : MonoBehaviour
{
    [SerializeField] private int life;
    public readonly int DieLayer = 10;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Damage()
    {
        life--;
    }

    public void Increase()
    {
        life++;
    }

    public bool IsDie()
    {
        if (life <= 0) return true;

        return false;
    }
}
