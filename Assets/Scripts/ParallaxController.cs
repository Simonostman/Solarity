using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxController : MonoBehaviour
{
    [System.Serializable]
    public class ParallaxSprite {
        public Sprite sprite;
        public float speed;
        public int layer;

        [HideInInspector] public float width;
        [HideInInspector] public float height;
    }

    [SerializeField]
    public ParallaxSprite[] parallaxSprites;
    
    private List<GameObject> parallaxObjects = new List<GameObject>();

    void Start()
    {
        for (int i = 0; i < parallaxSprites.Length; i++)
        {
            string name = "Sprite_" + i;
            parallaxObjects.Add(GenerateParallaxObject(parallaxSprites[i], name));
        }
    }

    void Update()
    {
        for (int i = 0; i < parallaxObjects.Count; i++)
        {
            foreach (Transform sprite in parallaxObjects[i].transform)
            {
                sprite.position = new Vector3(sprite.transform.position.x - Time.deltaTime * parallaxSprites[i].speed, 0, 100);
                if(parallaxSprites[i].speed > 0 && sprite.position.x < - parallaxSprites[i].width)
                    sprite.position = new Vector3(parallaxSprites[i].width, 0, 100);    
                if(parallaxSprites[i].speed < 0 && sprite.position.x > parallaxSprites[i].width)
                    sprite.position = new Vector3(-parallaxSprites[i].width, 0, 100);
            }
        }
    }

    GameObject GenerateParallaxObject(ParallaxSprite p, string name)
    {
        GameObject parallaxObject = new GameObject(name);
        parallaxObject.transform.parent = transform;

        for (int i = 0; i < 2; i++)
        {
            GameObject objectSprite = new GameObject(name + "_" + i);
            objectSprite.transform.parent = parallaxObject.transform;
            objectSprite.transform.position = new Vector3(0,0, 100);

            SpriteRenderer renderer = objectSprite.AddComponent<SpriteRenderer>();
            renderer.sprite = p.sprite;
            renderer.sortingLayerName = "Parallax";


            float boundsWidth = renderer.sprite.bounds.size.x;
            float boundsHeight = renderer.sprite.bounds.size.y;
            p.height = (float)(Camera.main.orthographicSize * 2.0);
            p.width = p.height / Screen.height * Screen.width;

            objectSprite.transform.localScale = new Vector3(p.width / boundsWidth, p.height / boundsHeight, 0);
            objectSprite.transform.position = new Vector3(objectSprite.transform.position.x + (p.width * i), 0, 100);
        }

        return parallaxObject;
    }
}