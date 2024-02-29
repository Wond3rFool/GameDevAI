using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float arcHeight = 5f; // Adjust this value to control the height of the arc
    public float speed = 5f;

    private Vector3 targetDirection;
    private Vector3 initialPosition;

    private void Start()
    {
        initialPosition = transform.position;
    }

    public void SetTargetDirection(Vector3 direction)
    {
        targetDirection = direction.normalized;
        LaunchProjectile();
    }

    private void LaunchProjectile()
    {
        Vector3 launchDirection = CalculateLaunchDirection();
        GetComponent<Rigidbody>().velocity = launchDirection * speed;
    }

    private Vector3 CalculateLaunchDirection()
    {
        float time = Mathf.Sqrt(-2 * arcHeight / Physics.gravity.y) + Mathf.Sqrt(2 * (arcHeight) / Physics.gravity.y);

        Vector3 velocityXZ = targetDirection * (Vector3.Distance(initialPosition, initialPosition + targetDirection) / time);
        Vector3 launchDirection = velocityXZ + Vector3.up * Mathf.Sqrt(-2 * Physics.gravity.y * arcHeight);

        return launchDirection.normalized;
    }
}