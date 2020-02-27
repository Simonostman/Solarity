using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SolarWindGenerator : MonoBehaviour
{
    public float windSpeed = 5.0f;

    // Temp
    public Sprite tempSpriteHodler;

    private List<GameObject> winds = new List<GameObject>();
    private AttractionPoint[] attractionPoints;

    void Start()
    {
        attractionPoints = FindObjectsOfType<AttractionPoint>();

        GameObject windParent = new GameObject("Winds");
        for (int i = 0; i < 10; i++)
        {
            GameObject wind = new GameObject("Wind");
            wind.transform.parent = windParent.transform;

            Rigidbody2D rigid = wind.AddComponent<Rigidbody2D>();
            rigid.gravityScale = 0;
            rigid.AddForce(Vector2.left * windSpeed);

            SpriteRenderer renderer = wind.AddComponent<SpriteRenderer>();
            renderer.sprite = tempSpriteHodler;

            winds.Add(wind);
        }
    }

    void FixedUpdate()
    {
        foreach (var point in attractionPoints)
        {
            Vector3 lookDir = (point.transform.position - transform.position).normalized;
            Quaternion lookRot = Quaternion.LookRotation(lookDir);
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRot, Time.deltaTime * 5);
            Debug.Log(lookDir);
        }
    }
}
