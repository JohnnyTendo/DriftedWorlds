using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    public Sprite idleSprite;
    public Sprite activeSprite;
    public GameObject PopUp;
    private SpriteRenderer renderer;
    // Start is called before the first frame update
    void Start()
    {
        renderer = gameObject.GetComponent<SpriteRenderer>();
        renderer.sprite = idleSprite;
        PopUp.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        PopUp.SetActive(true);
        if (collision.gameObject.tag == "Player" 
            && collision.gameObject.tag != "Mask"
            && Input.GetKey(KeyCode.E))
        {
            renderer.sprite = activeSprite;
            PerformAction(collision.gameObject);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            PopUp.SetActive(false);
        }
    }
    private void PerformAction(GameObject _gameObject)
    {
        PlayerController playerController = _gameObject.GetComponent<PlayerController>();
        playerController.viewCollider.enabled = true;
        playerController.raiseInsanity = true;
    }
}
