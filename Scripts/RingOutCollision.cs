using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Unity.MLAgents;

public class RingOutCollision : MonoBehaviour
{
    public Agent agent1;
    public Agent agent2;
    public GameObject sumo1prefab;

    private float deathTimer = 0.2f;
    private bool dead;

    void OnCollisionEnter2D(Collision2D collision)
    {
        String name = collision.gameObject.name;
        if (name.Contains("1") || name.Contains("2"))
        {
            //Do this same thing for agent 2
            if (agent1.gameObject.name == collision.gameObject.name)
                agent1.SetReward(-1.0f);
            else agent1.SetReward(1.0f);

            PlayerPrefs.SetFloat("Iterator", PlayerPrefs.GetFloat("Iterator") + 1);
            Debug.Log(PlayerPrefs.GetFloat("Iterator"));

            Destroy(agent1.gameObject);
            deathTimer = 0.2f;
            dead = true;
            Debug.Log(GameObject.Find("Clock").GetComponent<GameTime>().time);
            GameObject.Find("Clock").GetComponent<GameTime>().time = 0;
        }
    }

    void FixedUpdate()
    {
        if (deathTimer <= 0.0f)
        {
            GameObject sumo1 = Instantiate(sumo1prefab);
            agent1 = sumo1.GetComponent<Agent1>();
            dead = false;
            deathTimer = 0.2f;
        }

        if (dead)
        {
            deathTimer -= Time.deltaTime;
        }
    }
}
