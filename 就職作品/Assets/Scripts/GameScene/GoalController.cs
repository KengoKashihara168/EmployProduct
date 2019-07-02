using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalController : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D trigger)
    {
        if(trigger.tag.Equals("Player"))
        {
            Debug.Log("Goal");
        }
    }
}
