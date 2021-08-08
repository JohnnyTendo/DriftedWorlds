using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HouseScript : MonoBehaviour
{
    public PlayerController player;
    public GameObject PopUp;
    public GameObject Mouth;
    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        PopUp.SetActive(false);
    }
    public void Interact()
    {
        player.viewCollider.enabled = true;
        Transform transform = player.mask.GetComponent<Transform>();
        transform.localScale = new Vector3(player.maxInsanity, player.maxInsanity, transform.localScale.z);

        Mouth.GetComponent<Animator>().SetBool("isCloseing", true);
        player.DamageEvent();
        player.DieEvent();
        player.Die();
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        PopUp.SetActive(true);
        if (collision.gameObject.tag == "Player"
            && collision.gameObject.tag != "Mask"
            && Input.GetKey(KeyCode.E))
        {
            Interact();
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            PopUp.SetActive(false);
        }
    }
}
