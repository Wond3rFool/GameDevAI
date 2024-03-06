using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Guard : Tree, IHear
{
    public Transform[] waypoints;
    public Transform weaponSpot;

    public TextMeshPro text;

    public LayerMask targetLayer;

    public static float speed = 2f;
    public static float fovRange = 5f;
    public static float attackRange = 1.5f;

    public static bool hasWeapon;
    public static bool isStunned;
    public static bool canSeePlayer;

    public LayerMask obstacleLayer;

    private bool canHearPlayer;

    private Vector3 positionToCheck;

    protected override BTBaseNode SetupTree()
    {
        return new Selector(new List<BTBaseNode>
        {
            new Sequence(new List<BTBaseNode>
            {
                new ConditionNode(() => canHearPlayer),
                new DisplayText(text, "Heard Player"),
                new Parallel(new List<BTBaseNode>
                {
                    new Inverter(new SetDestination(transform, "SoundTarget")),
                    new Inverter(new ConditionNode(() => isStunned)),
                    new Inverter(new CheckForPlayer(transform, obstacleLayer)),
                }),
            }),

            new Selector(new List<BTBaseNode>
            {
                new Sequence(new List<BTBaseNode>
                {
                    new ConditionNode(() => isStunned),
                    new DisplayText(text, "Is Stunned"),
                    new PlayAnimation(transform, "Crouch Idle"),
                    new WaitFor(4f),
                    new FunctionNode(() => isStunned = false),
                    new Inverter(new FunctionNode(() => canHearPlayer = false)),

                }),
                new Sequence(new List<BTBaseNode>
                {
                    new Inverter(new CheckTargetInRange(transform, 1000, "Target", targetLayer)),
                    new Patrol(transform, waypoints, text)
                }),

                new Sequence(new List<BTBaseNode>
                {
                    new CheckForPlayer(transform, obstacleLayer),
                    new CheckTargetInRange(transform, 24, "Target", targetLayer),
                    new Inverter(new ConditionNode(HasWeapon)),
                    new WaitFor(0.5f),
                    new GrabWeapon(transform, weaponSpot,text),
                    new FunctionNode(() => hasWeapon = true)
                }),

                new Sequence(new List<BTBaseNode>
                {
                    new ConditionNode(HasWeapon),
                    new Parallel(new List<BTBaseNode>
                    {
                        new CheckForPlayer(transform, obstacleLayer),
                        new Inverter(new ConditionNode(() => isStunned)),
                        new CheckTargetInRange(transform, 24, "Target", targetLayer),
                        new ToTarget(transform, text),
                        new CheckAttackRange(transform),
                    }),
                    new Attack(text),
                    new FunctionNode(() => canHearPlayer = false)
                }),
            }),
            new Patrol(transform, waypoints, text)
        });
    }

    private bool HasWeapon() => hasWeapon;

    public void RespondToSound(Sound sound)
    {
        if (sound.soundType == Sound.SoundType.Interesting)
        {
            canHearPlayer = true;
            Blackboard.SetData("SoundTarget", sound.pos);
        }
        else 
        {
            canHearPlayer = false;
        }
    }
}
