using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Zombie : MonoBehaviour
{

    [SerializeField] private int HP = 100;
    private Animator animator;

    private UnityEngine.AI.NavMeshAgent navAgent;

    public bool isDead;

    private void Start()
    {
        animator = GetComponent<Animator>();
        navAgent = GetComponent<UnityEngine.AI.NavMeshAgent>();
    }

    public void TakeDamage(int damageAmount)
    {
        HP -= damageAmount;

        if (HP <= 0)
        {
            int randomValue = Random.Range(0, 2);
            if (randomValue == 0)
            {
                animator.SetTrigger("Die1");

            }
            else
            {
                animator.SetTrigger("Die2");
            }

            isDead = true;
            //sound dead
            SoundManager.Instance.zombieChannel2.PlayOneShot(SoundManager.Instance.ZombieDeath);
        }
        else
        {
            animator.SetTrigger("Damage");
            SoundManager.Instance.zombieChannel2.PlayOneShot(SoundManager.Instance.ZombieHurt);


        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, 2.5f);
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, 18f);
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, 21f);

    }
}
