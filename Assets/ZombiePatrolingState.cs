using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ZombiePatrolingState : StateMachineBehaviour
{

    float timer;
    public float PatrolingTime = 10f;
    Transform player;
    UnityEngine.AI.NavMeshAgent agent;

    public float detectionArea = 18f;
    public float patrolSpeed = 2f;
    List<Transform> waypointsList = new List<Transform>();
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // --- Initialization --- //

        player = GameObject.FindGameObjectWithTag("Player").transform;
        agent = animator.GetComponent<UnityEngine.AI.NavMeshAgent>();

        agent.speed = patrolSpeed;
        timer = 0;

        // --- Get all waypoints and Move to First Waypoint --- //

        GameObject waypointCluster = GameObject.FindGameObjectWithTag("Waypoints");
        foreach (Transform t in waypointCluster.transform)
        {
            waypointsList.Add(t);
        }

        Vector3 nextPosition = waypointsList[Random.Range(0, waypointsList.Count)].position;
        agent.SetDestination(nextPosition);
    }
    //OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

        if (SoundManager.Instance.zombieChannel.isPlaying == false)
        {
            SoundManager.Instance.zombieChannel.clip = SoundManager.Instance.ZombieWalking;
            SoundManager.Instance.zombieChannel.PlayDelayed(1f);
        }
        // --- If agent arrived at waypoint, move to next waypoint --- //

        if (agent.remainingDistance <= agent.stoppingDistance)
        {
            agent.SetDestination(waypointsList[Random.Range(0, waypointsList.Count)].position);
        }

        // --- Transition to Idle State --- //

        timer += Time.deltaTime;
        if (timer > PatrolingTime)
        {
            animator.SetBool("isPatroling", false);
        }

        // --- Transition to Chase State --- //

        float distanceFromPlayer = Vector3.Distance(player.position, animator.transform.position);
        if (distanceFromPlayer < detectionArea)
        {
            animator.SetBool("isChasing", true);
        }

    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        agent.SetDestination(agent.transform.position);
        SoundManager.Instance.zombieChannel.Stop();
    }


}
