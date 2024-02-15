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

    public static bool hasWeapon = false;
    public static bool hasVision = false;

    protected override TreeNode SetupTree()
    {
        TreeNode findWeapon = new Sequence(new List<TreeNode>
        {
            new ConditionNode(SeenPlayer),
            new Inverter(new ConditionNode(HasWeapon)),
            new GrabWeapon(transform, weaponSpot,text),
        });

        TreeNode chasePlayer = new Sequence(new List<TreeNode>
        {
            new ConditionNode(HasWeapon),
            new ToTarget(transform, text),
            new CheckAttackRange(transform),
            new Attack(transform, text)
        });

        TreeNode patrol = new Selector(new List<TreeNode> 
        {
            new CheckPlayerInRange(transform),
            new Patrol(transform, waypoints, text)
        });

        TreeNode root = new Selector(new List<TreeNode>
        {
            findWeapon,
            chasePlayer,
            patrol
        });

        return root;
    }

    private void toggleWeapon() => hasWeapon = !hasWeapon;

    private bool HasWeapon() => hasWeapon;

    public static void toggleVision() => hasVision = !hasVision;

    private bool SeenPlayer() => hasVision;
}
