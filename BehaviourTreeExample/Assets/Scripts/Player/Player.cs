using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour, IDamageable
{
    public Transform CameraTransofrm;
    [SerializeField] private float rotationSpeed = 180f;
    [SerializeField] private float moveSpeed = 3;
    [SerializeField] private float deathForce = 1000;
    [SerializeField] private GameObject ragdoll;
    [SerializeField] private GameObject projectile;
    [SerializeField] private Transform firePoint;
    private Rigidbody rb;
    private float throwStrength = 140f;
    private Sound walking;
    private Sound sneaking;
    private Animator animator;
    private float vert = 0;
    private float hor = 0;
    private Vector3 moveDirection;
    private Collider mainCollider;

    public static bool beingAttacked;

    // Start is called before the first frame update
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponentInChildren<Animator>();
        mainCollider = GetComponent<Collider>();
        var rigidBodies = GetComponentsInChildren<Rigidbody>();
        foreach (Rigidbody rib in rigidBodies)
        {
            rib.isKinematic = true;
            rib.useGravity = true;
        }

        var cols = GetComponentsInChildren<Collider>();
        foreach (Collider col in cols)
        {
            if (col.isTrigger) { continue; }
            col.enabled = false;
        }
        mainCollider.enabled = true;
        rb.isKinematic = false;
    }

    // Update is called once per frame
    private bool isCrouching = false;
    private float crouchSpeedMultiplier = 0.8f; // Adjust this value based on your preference

    // Update is called once per frame
    private void Update()
    {
        vert = Input.GetAxis("Vertical");
        hor = Input.GetAxis("Horizontal");

        walking = new Sound(transform.position, 10, Sound.SoundType.Interesting);
        sneaking = new Sound(transform.position, 10, Sound.SoundType.Sneaky);

        // Check if the player is holding the Shift key to crouch
        isCrouching = Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift);

        // Adjust move speed based on crouching state
        float currentMoveSpeed = isCrouching ? moveSpeed * crouchSpeedMultiplier : moveSpeed;

        Vector3 forwardDirection = Vector3.Scale(new Vector3(1, 0, 1), CameraTransofrm.transform.forward);
        Vector3 rightDirection = Vector3.Cross(Vector3.up, forwardDirection.normalized);
        moveDirection = forwardDirection.normalized * vert + rightDirection.normalized * hor;

        if (moveDirection != Vector3.zero)
        {
            transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(moveDirection.normalized, Vector3.up), rotationSpeed * Time.deltaTime);
        }

        transform.position += moveDirection.normalized * currentMoveSpeed * Time.deltaTime;

        bool isMoving = hor != 0 || vert != 0;
        if (isCrouching)
        {
            ChangeAnimation(isMoving ? "Walk Crouch" : "Crouch Idle", isMoving ? 0.05f : 0.15f);
            Sounds.MakeSound(sneaking);
        }
        else
        {
            ChangeAnimation(isMoving ? "Rifle Walk" : "Idle", isMoving ? 0.05f : 0.15f);
            if (isMoving)
            {
                Sounds.MakeSound(walking);
            }
            else
            {
                Sounds.MakeSound(sneaking);
            }
        }

        if (Input.GetKeyDown(KeyCode.Mouse0)) 
        {
            GameObject proPrefab = Instantiate(projectile, firePoint.position, Quaternion.identity);
            Rigidbody proPrefabRB = proPrefab.GetComponent<Rigidbody>();

            proPrefabRB.AddForce(CameraTransofrm.forward * throwStrength, ForceMode.Impulse);
        }

    }
    public void TakeDamage(GameObject attacker, int damage)
    {
        animator.enabled = false;
        var cols = GetComponentsInChildren<Collider>();
        foreach (Collider col in cols)
        {
            col.enabled = true;
        }
        mainCollider.enabled = false;

        var rigidBodies = GetComponentsInChildren<Rigidbody>();
        foreach (Rigidbody rib in rigidBodies)
        {
            rib.isKinematic = false;
            rib.useGravity = true;
            rib.AddForce(Vector3.Scale(new Vector3(1, 0.5f, 1), (transform.position - attacker.transform.position).normalized * deathForce));
        }
        ragdoll.transform.SetParent(null);

        gameObject.SetActive(false);
    }

    private void GetComponentsRecursively<T>(GameObject obj, ref List<T> components)
    {
        T component = obj.GetComponent<T>();
        if (component != null)
        {
            components.Add(component);
        }
        foreach (Transform t in obj.transform)
        {
            if (t.gameObject == obj) { continue; }
            GetComponentsRecursively<T>(t.gameObject, ref components);
        }
    }

    private void ChangeAnimation(string animationName, float fadeTime)
    {
        if (!animator.GetCurrentAnimatorStateInfo(0).IsName(animationName) && !animator.IsInTransition(0))
        {
            animator.CrossFade(animationName, fadeTime);
        }
    }
}
