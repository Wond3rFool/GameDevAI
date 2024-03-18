using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Guard : Tree, IHear, IDamageable
{
    [SerializeField]
    private Transform[] waypoints;
    [SerializeField]
    private Transform weaponSpot;
    [SerializeField]
    private Transform viewTransform;
    [SerializeField]
    private TextMeshPro text;
    [SerializeField]
    private LayerMask targetLayer;
    [SerializeField]
    private LayerMask obstacleLayer;

    private Player player;

    private float attackRange = 1.5f;
    private bool isStunned;
    private bool hasWeapon;
    private bool canHearPlayer;

    protected override void Start()
    {
        GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
        player = playerObject.GetComponent<Player>();
        base.Start();
    }

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
                    new SetDestination(transform, "SoundTarget"),
                    new Inverter(new ConditionNode(() => isStunned)),
                    new Inverter(new CheckForTarget(transform, obstacleLayer, "Target", 45.0f)),
                }),
                new PlayAnimation(transform, "Idle"),
                new Inverter(new LookRandomly(transform, 0.5f)),
            }),

            new Selector(new List<BTBaseNode>
            {
                new Sequence(new List<BTBaseNode>
                {
                    new ConditionNode(() => isStunned),
                    new DisplayText(text, "Is Stunned"),
                    new PlayAnimation(transform, "Scared"),
                    new FunctionNode(() => isStunned = false),
                    new Inverter(new FunctionNode(() => canHearPlayer = false)),

                }),
                new Sequence(new List<BTBaseNode>
                {
                    new Inverter(new CheckTargetInRange(transform, 1000, "Target", targetLayer)),
                    new Patrol(transform, text)
                }),

                new Sequence(new List<BTBaseNode>
                {
                    new CheckForTarget(transform, obstacleLayer, "Target", 100.0f),
                    new CheckTargetInRange(transform, 8, "Target", targetLayer),
                    new Inverter(new ConditionNode(() => hasWeapon)),
                    new DisplayText(text, "Finding weapon"),
                    new GrabWeapon(transform, weaponSpot),
                    new PlayAnimation(transform, "Side Kick"),
                    new DisplayText(text, "Found a weapon"),
                    new FunctionNode(() => hasWeapon = true)
                }),

                new Sequence(new List<BTBaseNode>
                {
                    new ConditionNode(() => hasWeapon),
                    new CheckForTarget(transform, obstacleLayer, "Target", 100.0f),
                    new Parallel(new List<BTBaseNode>
                    {
                        new Inverter(new ConditionNode(() => isStunned)),
                        new FunctionNode(() => player.SetBeingAttacked(true)),
                        new CheckTargetInRange(transform, 8, "Target", targetLayer),
                        new ToTarget(transform, text),
                        new CheckAttackRange(transform, attackRange, "Target", "Kick"),
                    }),
                    new Attack(transform, text, "Target"),
                    new FunctionNode(() => canHearPlayer = false)
                }),
            }),
            new Patrol(transform, text)
        });
    }

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
    public void TakeDamage(GameObject attacker, int damage){}

    public void BeStunned()
    {
        isStunned = true;
    }
}
