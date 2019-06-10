using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemController : MonoBehaviour
{
    int count;
    // Start is called before the first frame update
    void Start()
    {
        count = 1;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag != "Player") return;
        if (count <= 0) return;
        other.GetComponent<PlayerController>().HitItem(tag);
        Destroy(gameObject);
        count--;
    }
}
