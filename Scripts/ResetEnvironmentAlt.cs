using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetEnvironmentAlt : MonoBehaviour
{
    public GameObject rat;
    public GameObject ratPrefab;
    public GameObject[] checkpoints;
    public GameObject[] checkpointPrefabs;
    public GameObject[] tiles;
    public GameObject[] tilePrefabs;
    private float deathTimer = 0.5f;

    void FixedUpdate()
    {
        if (deathTimer >= 0.0f && rat == null)
        {
            deathTimer -= Time.deltaTime;
        }
        else if (deathTimer <= 0.0f && rat == null)
        {
            rat = Instantiate(ratPrefab, transform.parent);
            deathTimer = 0.5f;
            int ctr0 = 0, ctr1 = 0;
            foreach (GameObject checkpoint in checkpoints)
            {
                if (checkpoint == null)
                {
                    checkpoints[ctr0] = Instantiate(checkpointPrefabs[ctr0], transform.parent);
                }
                ctr0++;
            }

            foreach (GameObject tile in tiles)
            {
                if (tile == null)
                {
                    tiles[ctr1] = Instantiate(tilePrefabs[ctr1], transform.parent);
                }
                ctr1++;
            }
        }
    }
}
