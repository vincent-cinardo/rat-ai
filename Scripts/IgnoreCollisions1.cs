using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IgnoreCollisions1 : MonoBehaviour
{
    void OnEnable()
    {
        GameObject[] Body = new GameObject[]{
            GameObject.Find("1RThigh"), GameObject.Find("1RCalf"),
            GameObject.Find("1RFoot"), GameObject.Find("1Body"),
            GameObject.Find("1Head"), GameObject.Find("1LThigh"),
            GameObject.Find("1LCalf"), GameObject.Find("1LFoot"),
           // GameObject.Find("1LArm"), GameObject.Find("1LForearm"),
            GameObject.Find("1RArm"), GameObject.Find("1RForearm")
        };

        for (int i = 0; i < 10; i++)
        {
            //if (gameObject.name == Body[i].name) continue;
            Physics2D.IgnoreCollision(GetComponent<Collider2D>(), Body[i].GetComponent<Collider2D>());
        }
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
