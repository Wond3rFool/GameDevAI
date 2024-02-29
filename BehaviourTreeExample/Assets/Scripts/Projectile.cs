using System.Collections;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField]
    private float initialVelocity;
    [SerializeField]
    private float angle;

    private bool fired;

    private void Update()
    {
        if (fired) 
        {
           StartFunction();
            fired = false;
        }
    }

    IEnumerator Coroutine_Movement(float v0, float angle)
    {
        float t = 0;
        while (t < 100)
        {
            float x = v0 * t * Mathf.Cos(angle);
            float y = v0 * t * Mathf.Sin(angle) - (1f / 2f) * -Physics.gravity.y * Mathf.Pow(t, 2);
            transform.position = new Vector3(x, y, 0);
            t += Time.deltaTime;
            yield return null;
        }
    }

    private void StartFunction() 
    {
        fired = true;
        float angle = this.angle * Mathf.Deg2Rad;
        StopAllCoroutines();
        StartCoroutine(Coroutine_Movement(initialVelocity, angle));
    }
}
