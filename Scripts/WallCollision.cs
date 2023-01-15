using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Unity.MLAgents;

public class WallCollision : MonoBehaviour
{
    void OnCollisionEnter2D(Collision2D collision)
    {
        String name = collision.gameObject.name;
        if (name.Contains("Rat"))
        {
            RatAgentGrid rat = collision.gameObject.GetComponent<RatAgentGrid>();
            //rat.SetReward(-Math.Abs(GameObject.Find("Goal Cheese").transform.localPosition.x - collision.gameObject.transform.localPosition.x) * 3.0f);
            rat.SetReward(-10.0f);
            if (transform.parent.gameObject.name.Equals("Environment"))
                Debug.Log("Reward before death: " + rat.GetCumulativeReward());

            Destroy(rat.gameObject);
        }
    }
}
