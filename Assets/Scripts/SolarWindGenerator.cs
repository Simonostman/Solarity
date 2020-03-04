using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SolarWindGenerator : MonoBehaviour
{
    [Header("Wind Properties")]
    public float windSpeed = 5.0f;
    public float lifetime = 5.0f;
    public ParticleSystem effect;

    [Header("Generator Properties")]
    public float spanwFrequency = 10;
    public float maxAngle = 10.0f;

    private List<GameObject> winds = new List<GameObject>();
    private float spawnTimer;

    // Temp
    public Sprite tempSpriteHodler;

    void FixedUpdate()
    {
        spawnTimer += Time.deltaTime  % 60.0f;
        if(spawnTimer * spanwFrequency > 1)
        {
            SpawnWind();
            spawnTimer = 0;
        }

        GameObject deleteObject = null;
        foreach (var w in winds)
        {
            if(w.GetComponent<SolarWindController>().dead)
            {
                deleteObject = w;
                continue;
            }
        }

        winds.Remove(deleteObject);
        Destroy(deleteObject);

    }

    void SpawnWind()
    {
        GameObject wind = new GameObject("Wind");
        wind.transform.position = transform.position;
        wind.transform.rotation = transform.rotation;
        wind.transform.parent = transform;

        SolarWindController swc = wind.AddComponent<SolarWindController>();
        swc.windSpeed = windSpeed;
        swc.lifetime = lifetime;
        swc.tempSpriteHodler = tempSpriteHodler;

        winds.Add(wind);
    }
}
