using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EyeTracker : MonoBehaviour
{
    public GameObject Player;
    Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        animator = gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        gameObject.transform.LookAt(Player.transform);
        gameObject.transform.Rotate(new Vector3(0,-90,0));

        PlayerController playerController = Player.GetComponent<PlayerController>();
        if (playerController.insanity > playerController.maxInsanity / 2)
        {
            animator.SetBool("isInsane", true);
        }
        else
        {
            animator.SetBool("isInsane", false);
        }
    }
}
