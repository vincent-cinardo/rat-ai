using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameTime : MonoBehaviour
{
    public float time;
    // Start is called before the first frame update
    void OnEnable()
    {
        time = 0.0f;
    }

    void Update()
    {
        time += Time.deltaTime;
    }
}
