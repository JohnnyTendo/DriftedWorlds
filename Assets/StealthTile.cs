using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StealthTile : MonoBehaviour
{
    private SpriteRenderer renderer;
    private BoxCollider2D collider;
    public bool hideOnCollision = false;

    // Start is called before the first frame update
    void Start()
    {
        renderer = gameObject.GetComponent<SpriteRenderer>();
        renderer.maskInteraction = hideOnCollision ? SpriteMaskInteraction.VisibleOutsideMask : SpriteMaskInteraction.VisibleInsideMask;
        collider = gameObject.GetComponent<BoxCollider2D>();
        collider.isTrigger = !hideOnCollision;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Mask")
        {
            collider.isTrigger = hideOnCollision;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Mask")
        {
            collider.isTrigger = !hideOnCollision;
        }
    }
}
