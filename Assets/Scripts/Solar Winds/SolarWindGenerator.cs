using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;


public class SolarWindGenerator : MonoBehaviour
{
    [Header("Wind Properties")]
    public float windSpeed = 5.0f;
    public float lifetime = 5.0f;
    public GameObject effectPrefab;
    public GameObject effect2Prefab;

    [Header("Generator Properties")]
    public float spanwFrequency = 10;
    public float maxAngle = 10.0f;

    private List<GameObject> winds = new List<GameObject>();
    private List<GameObject> effects = new List<GameObject>();
    private float spawnTimer;

    // Temp
    // public Sprite tempSpriteHodler;

    void FixedUpdate()
    {
        spawnTimer += Time.deltaTime  % 60.0f;
        if(spawnTimer * spanwFrequency > 1)
        {
            SpawnWind();
            spawnTimer = 0;
        }

        GameObject deleteObject = null;
        for (int i = 0; i < winds.Count; i++)
        {
            SolarWindController swc = winds[i].GetComponent<SolarWindController>();
            swc.UpdateEffectPosition(transform);

            if(swc.dead)
            {
                swc.effect2.GetComponent<ParticleSystem>().Stop();
                if(swc.effect2.GetComponent<ParticleSystem>().particleCount == 0)
                {
                    Destroy(swc.effect2.gameObject);
                    deleteObject = winds[i];
                    continue;
                }
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
        swc.effect2Prefab = effect2Prefab;
        swc.effectPrefab = effectPrefab;

        // Temp
        // swc.tempSpriteHodler = tempSpriteHodler;

        winds.Add(wind);
    }
}