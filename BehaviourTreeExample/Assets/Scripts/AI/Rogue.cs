using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.AI;

public class Rogue : Tree
{

    private BTBaseNode tree;
    private NavMeshAgent agent;
    private Animator animator;
    protected override void Awake()
    {
        base.Awake();
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponentInChildren<Animator>();
    }

    protected override BTBaseNode SetupTree()
    {
        return new Selector(new List<BTBaseNode>
        {
            new FollowPlayer(transform)
        });
    }
}
