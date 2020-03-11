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

    void Start()
    {
        rigid = gameObject.AddComponent<Rigidbody2D>();
        rigid.gravityScale = 0;
    }

    public void UpdateEffectPosition(Transform parent)
    {
        if(effect == null)
        {
            effect = Instantiate(effectPrefab, parent);
            effect.transform.position = transform.position;
        }
        else
        {
            effect.transform.position = transform.position;
        }
    }

    private void FixedUpdate()
    {
        attractionPoints = FindObjectsOfType<AttractionPoint>();
        foreach (var point in attractionPoints)
        {
            Vector3 lookDir = (point.transform.position - transform.position).normalized;
            float angle;
            if(point.positivePolarity)  angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg;
            else                        angle = -Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg;
            Quaternion lookRot = Quaternion.AngleAxis(angle, Vector3.forward);
            
            float dst = Vector3.Distance(transform.position, point.transform.position);
            float pull = 9.81f * (point.GetComponent<AttractionPoint>().gravityStrenght / 100) / Mathf.Pow(dst, 2);

            transform.rotation = Quaternion.Slerp(transform.rotation, lookRot, Time.deltaTime * pull);
        }

        GetComponent<Rigidbody2D>().velocity = transform.right * windSpeed;

        lifeTimer += Time.deltaTime % 60;
        if(lifeTimer > lifetime)
        {
            dead = true;
        }
    }
}
