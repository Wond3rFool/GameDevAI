using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Guard : Tree
{
    public Transform[] waypoints;
    public Transform weaponSpot;

    public TextMeshPro text;

    public LayerMask targetLayer;

    public static float speed = 2f;
    public static float fovRange = 5f;
    public static float attackRange = 2f;

    public static bool hasWeapon = false;
    public static bool hasVision = false;

    public LayerMask obstacleLayer;

    protected override BTBaseNode SetupTree()
    {
        return new Selector(new List<BTBaseNode>
        {
            new Sequence(new List<BTBaseNode>
            {
                new Inverter(new CheckTargetInRange(transform, 1000, "Target", targetLayer)),
                new Patrol(transform, waypoints, text)
            }),

            new Sequence(new List<BTBaseNode>
            {
                new CheckForPlayer(transform, obstacleLayer),
                new CheckTargetInRange(transform, 8, "Target", targetLayer),
                new Inverter(new ConditionNode(HasWeapon)),
                new GrabWeapon(transform, weaponSpot,text),
                new FunctionNode(() => hasWeapon = true)
            }),

            new Sequence(new List<BTBaseNode>
            {
                new ConditionNode(HasWeapon),
                new Parallel(new List<BTBaseNode>
                {
                    new CheckForPlayer(transform, obstacleLayer),
                    new CheckTargetInRange(transform, 8, "Target", targetLayer),
                    new ToTarget(transform, text),
                    new CheckAttackRange(transform),
                }),
                new Attack(text)
            }),

            new Patrol(transform, waypoints, text)
        });
    }

    private bool HasWeapon() => hasWeapon;
}
