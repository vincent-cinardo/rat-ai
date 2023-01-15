using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;
using Unity.MLAgents;
using Unity.MLAgents.Sensors;
using Unity.MLAgents.Actuators;

public class RatAgentGrid : Agent
{
    public Sprite[] sprites;
    private Rigidbody2D rb;
    private RaycastHit2D hitRight, hitLeft, hitUp, hitDown;
    private float rewardCumulative;
    public float hunger;

    void Awake()
    {
        hunger = 6.0f;
        rewardCumulative = 0.0f;
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

        if (hr)
        {
            sensor.AddObservation(1);
        }
        else sensor.AddObservation(-1);
        if (hl)
        {
            sensor.AddObservation(2);
        }
        else sensor.AddObservation(-2);
        if (hu)
        {
            sensor.AddObservation(3);
        }
        else sensor.AddObservation(-3);
        if (hd)
        {
            sensor.AddObservation(4);
        } 
        else sensor.AddObservation(-4);
    }

    public override void OnActionReceived(ActionBuffers actions)
    {
        bool hr, hl, hu, hd, hrc, hlc, huc, hdc;
        hitRight = Physics2D.Raycast(transform.position + new Vector3(0.3f, 0.0f, 0.0f), Vector2.right);
        hitLeft = Physics2D.Raycast(transform.position + new Vector3(-0.3f, 0.0f, 0.0f), -Vector2.right);
        hitUp = Physics2D.Raycast(transform.position + new Vector3(0.0f, 0.3f, 0.0f), Vector2.up);
        hitDown = Physics2D.Raycast(transform.position + new Vector3(0.0f, -0.3f, 0.0f), -Vector2.up);
        hr = hitRight.collider.gameObject.name.Contains("Cheese") || hitRight.collider.gameObject.name.Contains("Tile");
        hl = hitLeft.collider.gameObject.name.Contains("Cheese") || hitLeft.collider.gameObject.name.Contains("Tile");
        hu = hitUp.collider.gameObject.name.Contains("Cheese") || hitUp.collider.gameObject.name.Contains("Tile");
        hd = hitDown.collider.gameObject.name.Contains("Cheese") || hitDown.collider.gameObject.name.Contains("Tile");
        hrc = hitRight.collider.gameObject.name.Contains("Cheese");
        hlc = hitLeft.collider.gameObject.name.Contains("Cheese");
        huc = hitUp.collider.gameObject.name.Contains("Cheese");
        hdc = hitDown.collider.gameObject.name.Contains("Cheese");

        switch (actions.DiscreteActions[0])
        {
            case 0:
                this.GetComponent<SpriteRenderer>().sprite = sprites[0];
                transform.localPosition += new Vector3(2.0f, 0.0f, 0);
                if (hr)
                    Reward(hrc);
                else
                    Punish();
                break;
            case 1:
                this.GetComponent<SpriteRenderer>().sprite = sprites[1];
                transform.localPosition += new Vector3(-2.0f, 0.0f, 0);
                if (hl)
                    Reward(hlc);
                else
                    Punish();
                break;
            case 2:
                this.GetComponent<SpriteRenderer>().sprite = sprites[2];
                transform.localPosition += new Vector3(0.0f, 2.0f, 0);
                if (hu)
                    Reward(huc);
                else
                    Punish();
                break;
            case 3:
                this.GetComponent<SpriteRenderer>().sprite = sprites[3];
                transform.localPosition += new Vector3(0.0f, -2.0f, 0);
                if (hd)
                    Reward(hdc);
                else
                    Punish();
                break;
        }
    }

    void Reward(bool cheese)
    {
        if (cheese)
        {
            AddReward(0.33333f);
            rewardCumulative += 0.3333f;
        }
    }

    void Punish()
    {
        AddReward(-0.43333f);
        rewardCumulative -= 0.4333f;
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