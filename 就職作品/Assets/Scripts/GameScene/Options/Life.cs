using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Life : MonoBehaviour
{
    [SerializeField] private int life;
    public readonly int DieLayer = 10;

    public void Damage()
    {
        life--;
    }

    public void Increase()
    {
        life++;
    }

    public int GetLifeCount()
    {
        return life;
    }

    public bool IsDie()
    {
        if (life <= 0) return true;

        return false;
    }
}
