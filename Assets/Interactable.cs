using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    public Sprite idleSprite;
    public Sprite activeSprite;
    private SpriteRenderer renderer;
    // Start is called before the first frame update
    void Start()
    {
        renderer = gameObject.GetComponent<SpriteRenderer>();
        renderer.sprite = idleSprite;
    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        Debug.Log("Stay");
        if (collision.gameObject.tag == "Player" && Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log("Stay -> Trigger");
            renderer.sprite = activeSprite;
            PerformAction(collision.gameObject);
        }
    }
    private void PerformAction(GameObject _gameObject)
    {
        PlayerController playerController = _gameObject.GetComponent<PlayerController>();
        playerController.viewCollider.enabled = true;
        playerController.raiseInsanity = true;
    }
}
