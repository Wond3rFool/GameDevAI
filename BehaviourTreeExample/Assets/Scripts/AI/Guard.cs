using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.AI;

public class Guard : Tree
{
    public Transform[] waypoints;
    public Transform weaponSpot;

    public TextMeshPro text;

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
                new Inverter(new CheckPlayerInRange(transform)),
                new Patrol(transform, waypoints, text)
            }),

            new Sequence(new List<BTBaseNode>
            {
                new CheckForPlayer(transform, obstacleLayer),
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
                    new ToTarget(transform, text),
                    new CheckAttackRange(transform),
                }),
                new Attack(text)
            }),

            new Patrol(transform, waypoints, text)
        });
    }

    private bool HasWeapon() => hasWeapon;

    public static void toggleVision() => hasVision = !hasVision;

    private bool SeenPlayer() => hasVision;
}
