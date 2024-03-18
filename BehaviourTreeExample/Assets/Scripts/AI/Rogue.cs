using System.Collections.Generic;
using TMPro;
using UnityEngine;

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

    [SerializeField]
    private TextMeshPro text;

    private bool foundSafeSpot;

    protected override void Awake()
    {
        base.Awake();
    }

    protected override BTBaseNode SetupTree()
    {
        return new Selector(new List<BTBaseNode>
        {
            new Inverter(new CheckTargetInRange(transform, detectDistance, "Friend", friendLayer)),

            new Sequence(new List<BTBaseNode>{ 
                new isPlayerThreatened(),
                new DisplayText(text, "Finding hiding spot"),
                new FindSafeSpot(transform, safeSpots),
                new DisplayText(text, "In Hiding"),
                new Inverter(new FunctionNode(() => foundSafeSpot = true))

            }),
            new Sequence(new List<BTBaseNode>
            {
                new ConditionNode(() => foundSafeSpot),
                new SavePlayer(transform),
                new CheckTargetInRange(transform, 500, "Target", targetLayer),
                new DisplayText(text, "Throwing Stone"),
                new WaitFor(1.0f),
                new StunTarget(transform, targetLayer, "Target"),
                new FunctionNode(() => Player.beingAttacked = false),
                new Inverter(new FunctionNode(() => foundSafeSpot = false))

            }),
            new Parallel(new List<BTBaseNode>
            {
                new FollowPlayer(transform, followDistance, "Friend"),
                new DisplayText(text, "Following Player")
            }),
        });
    }
}
