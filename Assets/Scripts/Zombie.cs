using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class Zombie : MonoBehaviour
{
    [SerializeField] private int HP = 100;
    [SerializeField] private ParticleSystem bloodEffect;

    private Animator animator;
    private NavMeshAgent navAgent;

    public bool isDead = false;

    private void Start()
    {
        animator = GetComponent<Animator>();
        navAgent = GetComponent<NavMeshAgent>();
    }

    public void TakeDamage(int damageAmount)
    {
        if (isDead) return;

        HP -= damageAmount;

        // Play blood effect
        if (bloodEffect != null)
        {
            bloodEffect.Play();
            StartCoroutine(StopBloodAfterTime(0.05f));
        }

        if (HP <= 0)
        {
            Die();
        }
        else
        {
            animator.SetTrigger("Damage");
            SoundManager.Instance.zombieChannel2
                .PlayOneShot(SoundManager.Instance.ZombieHurt);
        }
    }

    private IEnumerator StopBloodAfterTime(float time)
    {
        yield return new WaitForSeconds(time);
        if (bloodEffect != null)
            bloodEffect.Stop();
    }

    private void Die()
    {
        isDead = true;

        if (navAgent != null)
        {
            navAgent.isStopped = true;
            navAgent.enabled = false;
        }

        int randomValue = Random.Range(0, 2);
        animator.SetTrigger(randomValue == 0 ? "Die1" : "Die2");

        SoundManager.Instance.zombieChannel2
            .PlayOneShot(SoundManager.Instance.ZombieDeath);

        Destroy(gameObject, 3f);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, 2.5f);
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, 30f);
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, 31f);
    }
}
