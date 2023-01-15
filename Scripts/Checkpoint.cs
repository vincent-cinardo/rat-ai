using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    void OnCollisionEnter2D(Collision2D collision)
    {
        String name = collision.gameObject.name;
        if (name.Contains("Rat"))
        {
            RatAgentGrid rat = collision.gameObject.GetComponent<RatAgentGrid>();
            rat.AddReward(1.0f);
            rat.hunger = 6.0f;
            if (transform.parent.gameObject.name.Equals("Environment"))
                Debug.Log("Checkpoint " + gameObject.name + " reached.");
            Destroy(gameObject);
        }
    }
}
