using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SolarWindGenerator : MonoBehaviour
{
    List<GameObject> winds = new List<GameObject>();

    void Start()
    {
        GameObject windParent = new GameObject("Winds");
        for (int i = 0; i < 10; i++)
        {
            GameObject wind = new GameObject("Wind");
            wind.transform.parent = windParent.transform;
            Rigidbody2D rigid = wind.AddComponent<Rigidbody2D>();
            rigid.gravityScale = 0;
        }
    }

    void Update()
    {
        foreach (var wind in winds)
        {
            wind.GetComponent<Rigidbody2D>().AddForce(Vector2.right * 0.1f);
        }
    }
}
