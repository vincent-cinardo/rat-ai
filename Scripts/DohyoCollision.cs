using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Unity.MLAgents;

public class DohyoCollision : MonoBehaviour
{
    public Agent agent1;
    public Agent agent2;
    public GameObject sumo1prefab;

    private float deathTimer = 0.2f;
    private bool dead;

    void OnCollisionEnter2D(Collision2D collision)
    {
        String name = collision.gameObject.name;
        if (!(name.Contains("Foot")) && !(name.Contains("Floor")))
        {
            //Do this same thing for agent 2
            if (agent1.gameObject.name == collision.gameObject.transform.parent.name)
            {
                agent1.SetReward(GameObject.Find("Clock").GetComponent<GameTime>().time);
            }
            else agent1.SetReward(1.0f);

            Destroy(agent1.gameObject);
            deathTimer = 0.2f;
            dead = true;
        }
    }

    void FixedUpdate()
    {
        if (deathTimer <= 0.0f)
        {
            GameObject sumo1 = Instantiate(sumo1prefab, transform.parent);
            agent1 = sumo1.GetComponent<Agent1>();
            dead = false;
            deathTimer = 0.2f;
            Debug.Log("Time Survied: " + GameObject.Find("Clock").GetComponent<GameTime>().time.ToString() + "s");
            GameObject.Find("Clock").GetComponent<GameTime>().time = 0.0f;

        }

        if (dead)
        {
            deathTimer -= Time.deltaTime;
        }
    }
}
