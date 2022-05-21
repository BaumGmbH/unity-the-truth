using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    private int playAttack;

    private float attackTime;

    public bool swordOut;
    private bool isLookingLeft;

    public Animator animator;

    public GameObject attackBoxLeft;
    public GameObject attackBoxRight;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift) && gameObject.GetComponent<PlayerMovment>().isGrounded) SwapSword();
        gameObject.GetComponent<PlayerMovment>().moveStop = swordOut;

        if (Input.GetKeyDown(KeyCode.Mouse1) && swordOut && gameObject.GetComponent<PlayerMovment>().isGrounded) swordOut = false;
        else if (Input.GetKeyDown(KeyCode.Mouse1) && !swordOut && gameObject.GetComponent<PlayerMovment>().isGrounded) swordOut = true;

        animator.SetBool("SwordIsOut", swordOut);

        if(swordOut && Input.GetKeyDown(KeyCode.Mouse0))
        {
            Debug.Log("attack");

            playAttack = Random.Range(1, 4);

            if (playAttack == 1) animator.SetTrigger("Attack1");
            else if (playAttack == 2) animator.SetTrigger("Attack2");
            else if (playAttack == 3) animator.SetTrigger("Attack3");

            playAttack = 0;

            Attack();
        }

        if (!swordOut)
        {
            if (Input.GetAxis("Horizontal") < 0) isLookingLeft = true;
            else if ((Input.GetAxis("Horizontal") > 0)) isLookingLeft = false;
        }
        
        if(attackTime <= 0)
        {
            attackBoxLeft.SetActive(false);
            attackBoxRight.SetActive(false);
        }
        else attackTime--;
    }

    void SwapSword()
    {
        if (swordOut) swordOut = false;
        else swordOut = true;
    }

    void Attack()
    {
        attackBoxLeft.SetActive(false);
        attackBoxRight.SetActive(false);

        if (isLookingLeft)
        {
            attackBoxLeft.SetActive(true);
            attackTime = 1.5f;
        }
        else
        {
            attackBoxRight.SetActive(true);
            attackTime = 1.5f;
            Debug.Log(attackTime);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("HI");    
    }
}
