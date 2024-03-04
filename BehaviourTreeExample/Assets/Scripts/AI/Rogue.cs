using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Rogue : Tree
{
    [SerializeField]
    private Transform[] safeSpots;

    [SerializeField]
    private float followDistance;

    [SerializeField]
    private float detectDistance;

    [SerializeField]
    private LayerMask targetLayer;
    [SerializeField]
    private LayerMask friendLayer;

    private BTBaseNode tree;
    private NavMeshAgent agent;
    private Animator animator;


    protected override void Awake()
    {
        base.Awake();
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponentInChildren<Animator>();
        Debug.Log("I have awoken");
    }

    protected override BTBaseNode SetupTree()
    {
        return new Selector(new List<BTBaseNode>
        {
            new Inverter(new CheckTargetInRange(transform, detectDistance, "Friend", friendLayer)),

            new Sequence(new List<BTBaseNode>{ 
                new isPlayerThreatened(),
                new FindSafeSpot(transform, safeSpots),
                new SavePlayer(transform, targetLayer),
                new WaitFor(1.5f),
                new StunTarget(transform, targetLayer),
                new FunctionNode(() => Player.beingAttacked = false)
            }),
            new FollowPlayer(transform, followDistance, "Friend"),
        });
    }
}
