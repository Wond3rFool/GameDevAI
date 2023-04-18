using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.AI;

public class Guard : Tree
{
    public Transform[] waypoints;

    public static float speed = 2f;
    public static float fovRange = 5f;
    public static float attackRange = 1f;

    protected override Node SetupTree()
    {
        Node root = new Selector(new List<Node>
        {
            new Sequence(new List<Node>
            {
                new CheckAttackRange(transform),
                new Attack(transform),
            }),
            new Sequence(new List<Node>
            {
                new CheckFOV(transform),
                new ToTarget(transform),
            }),
            new Patrol(transform, waypoints),
        }); ; ;

        return root;
    }
}
