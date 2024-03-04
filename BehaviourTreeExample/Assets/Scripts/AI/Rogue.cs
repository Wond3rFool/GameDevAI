using System.Collections.Generic;
using TMPro;
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

    [SerializeField]
    private TextMeshPro text;

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
                new SavePlayer(transform, targetLayer),
                new DisplayText(text, "Throwing Stone"),
                new WaitFor(1.5f),
                new StunTarget(transform, targetLayer),
                new FunctionNode(() => Player.beingAttacked = false)
            }),
            new Parallel(new List<BTBaseNode>
            {
                new FollowPlayer(transform, followDistance, "Friend"),
                new DisplayText(text, "Following Player")
            }),
        });
    }
}
