using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class SolarWindController : MonoBehaviour
{
    public float windSpeed = 2.0f;
    public float lifetime = 5.0f;
    public bool dead = false;

    private Rigidbody2D rigid;
    private AttractionPoint[] attractionPoints;
    private float lifeTimer;

    // Effect
    public GameObject effect;
    public GameObject effectPrefab;
    private EffectPositionHandler eph;

    public GameObject effect2;
    public GameObject effect2Prefab;

    void Start()
    {
        rigid = gameObject.AddComponent<Rigidbody2D>();
        rigid.gravityScale = 0;
    }

    public void UpdateEffectPosition(Transform parent)
    {
        // if(effect == null)
        // {
        //     effect = Instantiate(effectPrefab);
        //     effect.transform.parent = parent;
        //     eph = effect.GetComponent<EffectPositionHandler>();
        // }

        // eph.target.transform.position = transform.position;
        // eph.effect.transform.position = parent.position;

        if(effect2 == null)
        {
            effect2 = Instantiate(effect2Prefab, parent);
            effect2.transform.position = transform.position;
        }
        else
        {
            effect2.transform.position = transform.position;
        }
    }

    private void FixedUpdate()
    {
        attractionPoints = FindObjectsOfType<AttractionPoint>();
        foreach (var point in attractionPoints)
        {
            if(point.activated)
            {
                Vector3 lookDir = (point.transform.position - transform.position).normalized;
                float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg;
                Quaternion lookRot = Quaternion.AngleAxis(angle, Vector3.forward);
                
                float dst = Vector3.Distance(transform.position, point.transform.position);
                float pull = point.GetComponent<AttractionPoint>().gravityStrenght / Mathf.Pow(dst, 2);

                transform.rotation = Quaternion.Slerp(transform.rotation, lookRot, Time.deltaTime * pull);
            }
        }

        GetComponent<Rigidbody2D>().velocity = transform.right * windSpeed;

        lifeTimer += Time.deltaTime % 60;
        if(lifeTimer > lifetime)
        {
            //effect.GetComponent<EffectPositionHandler>().Stop();
            dead = true;
        }
    }
}
