using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetEnvironment : MonoBehaviour
{
    public GameObject rat;
    public GameObject ratPrefab;
    public GameObject[] checkpoints;
    public GameObject[] checkpointPrefabs;
    private float deathTimer = 0.2f;

    // Update is called once per frame
    void FixedUpdate()
    {
        if (deathTimer >= 0 && rat == null)
        {
            deathTimer -= Time.deltaTime;
        }
        else if (rat == null)
        {
            rat = Instantiate(ratPrefab, transform.parent);
            deathTimer = 0.5f;
            for (int i = 0; i < 1; i++)
            {
                if (checkpoints[i] == null)
                {
                    checkpoints[i] = Instantiate(checkpointPrefabs[i], transform.parent);
                }
            }
        }
    }
}
