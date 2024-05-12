using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorController : MonoBehaviour
{
    public PlayerController playerController = null;
    private Animator animator = null;


    // Start is called before the first frame update
    void Start()
    {
        animator = this.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (playerController.isDead)
        {
            animator.SetBool("dead", true);
        }

        if (playerController.jumpStart)
        {
            animator.SetBool("jumpStart", true);
        } else if (playerController.isJumping)
        {
            animator.SetBool("jump", true);
        }
        else
        {
            animator.SetBool("jump", false);
            animator.SetBool("jumpStart", false);
        }

        if (!playerController.isIdle) return;

        if (Input.GetKeyDown(KeyCode.UpArrow)) { gameObject.transform.rotation = Quaternion.Euler(0, 0, 0); }
        if (Input.GetKeyDown(KeyCode.DownArrow)) { gameObject.transform.rotation = Quaternion.Euler(0, 180, 0); }
        if (Input.GetKeyDown(KeyCode.LeftArrow)) { gameObject.transform.rotation = Quaternion.Euler(0, -90, 0); }
        if (Input.GetKeyDown(KeyCode.RightArrow)) { gameObject.transform.rotation = Quaternion.Euler(0, 90, 0); }
    }
}
