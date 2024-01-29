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
    public static float attackRange = 1f;

    private bool hasWeapon = false;
    public static bool hasVision = true;

    protected override TreeNode SetupTree()
    {

        TreeNode findWeapon = new Sequence(new List<TreeNode>
        {
            new InverseCondition(HasWeapon),
            new CheckForPlayer(transform),
            new GrabWeapon(transform, weaponSpot, text),
            new FunctionNode(toggleWeapon)
        });

        TreeNode chasePlayer = new Parallel(new List<TreeNode>
        {
            new ConditionNode(HasWeapon),
            new CheckPlayerInRange(transform),
            new ToTarget(transform, text)
        });

        TreeNode attackPlayer = new Parallel(new List<TreeNode>
        {
            new CheckAttackRange(transform),
            new Attack(transform, text),
        });

        TreeNode patrol = new Patrol(transform, waypoints, text);

        TreeNode root = new Selector(new List<TreeNode>
        {
            attackPlayer,
            chasePlayer,
            findWeapon,
            patrol
        });

        return root;
    }

    private void toggleWeapon() => hasWeapon = !hasWeapon;

    private bool HasWeapon() => hasWeapon;

    public static void toggleVision() => hasVision = !hasVision;

    private bool SeenPlayer() => hasVision;
}
