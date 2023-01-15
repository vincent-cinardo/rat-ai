using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Unity.MLAgents;

public class Win : MonoBehaviour
{
    void OnCollisionEnter2D(Collision2D collision)
    {
        String name = collision.gameObject.name;
        if (name.Contains("Rat"))
        {
            RatAgentGrid rat = collision.gameObject.GetComponent<RatAgentGrid>();
            rat.SetReward(50.0f);
            Destroy(rat.gameObject);
        }
    }
}
