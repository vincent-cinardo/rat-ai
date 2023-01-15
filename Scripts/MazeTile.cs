using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeTile : MonoBehaviour
{
    void OnCollisionEnter2D(Collision2D collision)
    {
        String name = collision.gameObject.name;
        if (name.Contains("Rat"))
        {
            RatAgentGrid rat = collision.gameObject.GetComponent<RatAgentGrid>();
            Destroy(gameObject);
        }
    }
}
