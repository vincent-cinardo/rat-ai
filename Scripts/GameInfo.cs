using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameInfo : MonoBehaviour
{
    public bool reset;

    void Start()
    {
        if (reset || !(PlayerPrefs.HasKey("Iteration")))
        {
            PlayerPrefs.SetFloat("Iteration", 0.0f);
            reset = false;
        }
    }
}
