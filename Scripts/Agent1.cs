using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Sensors;
using Unity.MLAgents.Actuators;
using TMPro;

public class Agent1 : Agent
{
    public HingeJoint2D[] joints;

    private JointMotor2D motor;
    private float headStartingPos;
    private GameObject clock;

    void Awake()
    {
        clock = GameObject.Find("Clock");
    }

    public override void OnEpisodeBegin()
    {
        //transform.localPosition = new Vector3(-2.0f, -1.0f, 0.04123116f);
        GameObject.Find("Number").GetComponent<TextMeshProUGUI>().text = CompletedEpisodes.ToString();
    }

    void FixedUpdate()
    {

          /*  if (Math.Abs(GameObject.Find("1Body").transform.rotation.z) < 10.0f)
            {
                SetReward(10.0f);
            }
            else SetReward(-Math.Abs(GameObject.Find("1Body").transform.rotation.z));*/
    }

    public override void CollectObservations(VectorSensor sensor)
    {
        sensor.AddObservation(GameObject.Find("1Head").transform.localPosition.x);
        sensor.AddObservation(GameObject.Find("1Head").transform.localPosition.y);
        sensor.AddObservation(GameObject.Find("1Body").transform.rotation.z);
        sensor.AddObservation(GameObject.Find("1Body").transform.localPosition.y);
        sensor.AddObservation(GameObject.Find("1LCalf").transform.rotation.z);
        sensor.AddObservation(GameObject.Find("1RCalf").transform.rotation.z);
        sensor.AddObservation(GameObject.Find("1LFoot").transform.rotation.z);
        sensor.AddObservation(GameObject.Find("1RFoot").transform.rotation.z);
    }

    /*
     The actions corresponding to each joint.
     Each value of the array is some joint, where the value
     represents one of the following movements.
     0 - No movement
     1 - Weak contraction
     2 - Strong contraction
     3 - Weak extension
     4 - Heavy extension
    */

    public override void OnActionReceived(ActionBuffers actions)
    {
        int[] behaviour =
        {
            actions.DiscreteActions[0],
            actions.DiscreteActions[1],
            actions.DiscreteActions[2],
            actions.DiscreteActions[3],
            actions.DiscreteActions[4],
            actions.DiscreteActions[5],
            actions.DiscreteActions[6],
            actions.DiscreteActions[7],
            actions.DiscreteActions[8],
            actions.DiscreteActions[9],
        };

        for (int joint = 0; joint < 10; joint++)
        {
            switch (behaviour[joint])
            {
                case 0:
                    motor = joints[joint].motor;
                    motor.motorSpeed = 0.0f;
                    motor.maxMotorTorque = 50.0f;
                    joints[joint].motor = motor;
                    break;
                case 1:
                    motor = joints[joint].motor;
                    motor.motorSpeed = 20.0f;
                    motor.maxMotorTorque = 50.0f;
                    joints[joint].motor = motor;
                    break;
                case 2:
                    motor = joints[joint].motor;
                    motor.motorSpeed = 40.0f;
                    motor.maxMotorTorque = 50.0f;
                    joints[joint].motor = motor;
                    break;
                case 3:
                    motor = joints[joint].motor;
                    motor.motorSpeed = -20.0f;
                    motor.maxMotorTorque = 50.0f;
                    joints[joint].motor = motor;
                    break;
                case 4:
                    motor = joints[joint].motor;
                    motor.motorSpeed = -40.0f;
                    motor.maxMotorTorque = 50.0f;
                    joints[joint].motor = motor;
                    break;
            }
        }
    }
}
