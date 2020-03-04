using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SolarWindController : MonoBehaviour
{
    public float windSpeed = 2.0f;
    public float lifetime = 5.0f;
    public ParticleSystem effect;
    public bool dead = false;

    private Rigidbody2D rigid;
    private SpriteRenderer render;
    private AttractionPoint[] attractionPoints;
    private float lifeTimer;

    // Temp
    public Sprite tempSpriteHodler;

    void Start()
    {
        rigid = gameObject.AddComponent<Rigidbody2D>();
        rigid.gravityScale = 0;

        render = gameObject.AddComponent<SpriteRenderer>();
        render.sprite = tempSpriteHodler;
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
            dead = true;
        }
    }
}
