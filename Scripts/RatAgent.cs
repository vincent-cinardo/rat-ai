using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;
using Unity.MLAgents;
using Unity.MLAgents.Sensors;
using Unity.MLAgents.Actuators;

public class RatAgent : Agent
{
    public Sprite[] sprites;
    public float hunger;
    private Rigidbody2D rb;
    private RaycastHit2D hitRight, hitLeft, hitUp, hitDown;
    private ArrayList detected, explored;

    void Awake()
    {
        detected = new ArrayList();
        explored = new ArrayList();
        sprites = new Sprite[4];
        rb = GetComponent<Rigidbody2D>();
        GetComponent<SpriteRenderer>().sprite = GameObject.Find("ratr").GetComponent<SpriteRenderer>().sprite;
        sprites[0] = GameObject.Find("ratr").GetComponent<SpriteRenderer>().sprite;
        sprites[1] = GameObject.Find("ratl").GetComponent<SpriteRenderer>().sprite;
        sprites[2] = GameObject.Find("ratu").GetComponent<SpriteRenderer>().sprite;
        sprites[3] = GameObject.Find("ratd").GetComponent<SpriteRenderer>().sprite;
        GetComponent<SpriteRenderer>().size = new Vector2(0.2f, 0.2f);
        hitRight = Physics2D.Raycast(transform.position + new Vector3(0.3f, 0.0f, 0.0f), Vector2.right);
        hitLeft = Physics2D.Raycast(transform.position + new Vector3(-0.3f, 0.0f, 0.0f), -Vector2.right);
        hitUp = Physics2D.Raycast(transform.position + new Vector3(0.0f, 0.3f, 0.0f), Vector2.up);
        hitDown = Physics2D.Raycast(transform.position + new Vector3(0.0f, -0.3f, 0.0f), -Vector2.up);
        hunger = 20.0f;
        ExploreUnrewarded();
    }

    public override void CollectObservations(VectorSensor sensor)
    {
        bool hr, hl, hu, hd;
        hitRight = Physics2D.Raycast(transform.position + new Vector3(0.3f, 0.0f, 0.0f), Vector2.right);
        hitLeft = Physics2D.Raycast(transform.position + new Vector3(-0.3f, 0.0f, 0.0f), -Vector2.right);
        hitUp = Physics2D.Raycast(transform.position + new Vector3(0.0f, 0.3f, 0.0f), Vector2.up);
        hitDown = Physics2D.Raycast(transform.position + new Vector3(0.0f, -0.3f, 0.0f), -Vector2.up);
        hr = hitRight.collider.gameObject.name.Contains("Cheese") || hitRight.collider.gameObject.name.Contains("Tile");
        hl = hitLeft.collider.gameObject.name.Contains("Cheese") || hitLeft.collider.gameObject.name.Contains("Tile");
        hu = hitUp.collider.gameObject.name.Contains("Cheese") || hitUp.collider.gameObject.name.Contains("Tile");
        hd = hitDown.collider.gameObject.name.Contains("Cheese") || hitDown.collider.gameObject.name.Contains("Tile");

        /*sensor.AddObservation(transform.localPosition.x);
        sensor.AddObservation(transform.localPosition.y);*/

        Explore();
        //hitRight.distance
        
        if (hr)
        {
            sensor.AddObservation(1);
        }
        else sensor.AddObservation(0);
        if (hl)
        {
            sensor.AddObservation(1);
        }
        else sensor.AddObservation(0);
        if (hu)
        {
            sensor.AddObservation(1);
        }
        else sensor.AddObservation(0);
        if (hd)
        {
            sensor.AddObservation(1);
        }
        else sensor.AddObservation(0);

        if (hr || hl || hu || hd)
        {
            sensor.AddObservation(1);
        }
        else sensor.AddObservation(0);
    }

    public override void OnActionReceived(ActionBuffers actions)
    {
        bool hr, hl, hu, hd, hrw, hlw, huw, hdw;
        hitRight = Physics2D.Raycast(transform.position + new Vector3(0.3f, 0.0f, 0.0f), Vector2.right);
        hitLeft = Physics2D.Raycast(transform.position + new Vector3(-0.3f, 0.0f, 0.0f), -Vector2.right);
        hitUp = Physics2D.Raycast(transform.position + new Vector3(0.0f, 0.3f, 0.0f), Vector2.up);
        hitDown = Physics2D.Raycast(transform.position + new Vector3(0.0f, -0.3f, 0.0f), -Vector2.up);
        hr = hitRight.collider.gameObject.name.Contains("Cheese") || hitRight.collider.gameObject.name.Contains("Tile");
        hl = hitLeft.collider.gameObject.name.Contains("Cheese") || hitLeft.collider.gameObject.name.Contains("Tile");
        hu = hitUp.collider.gameObject.name.Contains("Cheese") || hitUp.collider.gameObject.name.Contains("Tile");
        hd = hitDown.collider.gameObject.name.Contains("Cheese") || hitDown.collider.gameObject.name.Contains("Tile");
        hrw = hitRight.collider.gameObject.name.Contains("Wall");
        hlw = hitLeft.collider.gameObject.name.Contains("Wall");
        huw = hitUp.collider.gameObject.name.Contains("Wall");
        hdw = hitDown.collider.gameObject.name.Contains("Wall");

        switch (actions.DiscreteActions[0])
        {
            case 0:
                this.GetComponent<SpriteRenderer>().sprite = sprites[0];
                rb.velocity = new Vector3(3.0f, 0.0f, 0);
                if ((hrw && (hl || hu || hd)) || (hrw && hlw && huw && hdw))
                {
                    AddReward(-0.03f);
                }
                else AddReward(0.02f);
                break;
            case 1:
                this.GetComponent<SpriteRenderer>().sprite = sprites[1];
                rb.velocity = new Vector3(-3.0f, 0.0f, 0);
                if (hlw && (hr || hu || hd) || (hrw && hlw && huw && hdw))
                {
                    AddReward(-0.03f);
                }
                else AddReward(0.02f);
                break;
            case 2:
                this.GetComponent<SpriteRenderer>().sprite = sprites[2];
                rb.velocity = new Vector3(0.0f, 3.0f, 0);
                if (huw && (hl || hr || hd) || (hrw && hlw && huw && hdw))
                {
                    AddReward(-0.03f);
                }
                else AddReward(0.02f);
                break;
            case 3:
                this.GetComponent<SpriteRenderer>().sprite = sprites[3];
                rb.velocity = new Vector3(0.0f, -3.0f, 0);
                if (hdw && (hl || hu || hr) || (hrw && hlw && huw && hdw))
                {
                    AddReward(-0.03f);
                }
                else AddReward(0.02f);
                break;
        }
    }

    void Explore()
    {
        //Reward for finding something new.
        /*if (!(detected.Contains(hitRight.collider.gameObject.name)))
        {
            detected.Add(hitRight.collider.gameObject.name);
            AddReward(0.1f);
        }
        //else AddReward(-0.05f);
        if (!(detected.Contains(hitLeft.collider.gameObject.name)))
        {
            detected.Add(hitLeft.collider.gameObject.name);
            AddReward(0.1f);
        }
        //else AddReward(-0.05f);
        if (!(detected.Contains(hitUp.collider.gameObject.name)))
        {
            detected.Add(hitUp.collider.gameObject.name);
            AddReward(0.1f);
        }
        //else AddReward(-0.05f);
        if (!(detected.Contains(hitDown.collider.gameObject.name)))
        {
            detected.Add(hitDown.collider.gameObject.name);
            AddReward(0.1f);
        }
        //else AddReward(-0.05f);
        */
        //Reward for exploring things.
        /*if (hitRight.distance < 1.5f && !(explored.Contains(hitRight.collider.gameObject.name)))
        {
            explored.Add(hitRight.collider.gameObject.name);
            AddReward(0.2f);
        }
        if (hitLeft.distance < 1.5f && !(explored.Contains(hitLeft.collider.gameObject.name)))
        {
            explored.Add(hitLeft.collider.gameObject.name);
            AddReward(0.2f);
        }
        if (hitUp.distance < 1.5f && !(explored.Contains(hitUp.collider.gameObject.name)))
        {
            explored.Add(hitUp.collider.gameObject.name);
            AddReward(0.2f);
        }
        if (hitDown.distance < 1.5f && !(explored.Contains(hitDown.collider.gameObject.name)))
        {
            explored.Add(hitDown.collider.gameObject.name);
            AddReward(0.2f);
        }/*

        //Punish for staying in same area.
        /*
        if (hitRight.distance < 1.5f && explored.Contains(hitRight.collider.gameObject.name))
        {
            AddReward(-0.5f);
        }
        if (hitLeft.distance < 1.5f && explored.Contains(hitLeft.collider.gameObject.name))
        {
            AddReward(-0.5f);
        }
        if (hitUp.distance < 1.5f && explored.Contains(hitUp.collider.gameObject.name))
        {
            AddReward(-0.5f);
        }
        if (hitDown.distance < 1.5f && explored.Contains(hitDown.collider.gameObject.name))
        {
            AddReward(-0.5f);
        }*/
    }

    void ExploreUnrewarded()
    {
        detected.Add(hitRight.collider.gameObject.name);
        detected.Add(hitLeft.collider.gameObject.name);
        detected.Add(hitUp.collider.gameObject.name);
        detected.Add(hitDown.collider.gameObject.name);
        explored.Add(hitRight.collider.gameObject.name);
        explored.Add(hitUp.collider.gameObject.name);
        explored.Add(hitDown.collider.gameObject.name);
    }

    void FixedUpdate()
    {
        hunger -= Time.deltaTime;
        if (hunger <= 0.0f)
        {
            AddReward(-30.0f);
            if (transform.parent.gameObject.name.Equals("Environment"))
                Debug.Log("Reward before starving: " + GetCumulativeReward());
            Destroy(this.gameObject);
        }
    }
}