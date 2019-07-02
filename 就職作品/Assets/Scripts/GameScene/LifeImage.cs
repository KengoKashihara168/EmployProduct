using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeImage : MonoBehaviour
{
    [SerializeField] private GameObject life;
    [SerializeField] private Life playerLife;
    private List<GameObject> instantLife;

    // Start is called before the first frame update
    void Start()
    {
        instantLife = new List<GameObject>();

        for(int i = 0;i < playerLife.GetLifeCount();i++)
        {
            Increment();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(instantLife.Count > playerLife.GetLifeCount())
        {
            Decrement();
        }

        if(instantLife.Count < playerLife.GetLifeCount())
        {
            Increment();
        }
    }

    private void Decrement()
    {
        Destroy(instantLife[playerLife.GetLifeCount()]);
        instantLife.RemoveAt(playerLife.GetLifeCount());
    }

    private void Increment()
    {
        instantLife.Add(Instantiate(life, transform));
    }
}
