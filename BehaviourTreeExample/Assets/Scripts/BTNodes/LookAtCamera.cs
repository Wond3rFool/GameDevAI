using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtCamera : MonoBehaviour
{
    // Start is called before the first frame update
    private void Start()
    {
        


    }

    // Update is called once per frame
    private void Update()
    {
        transform.LookAt(Camera.main.transform);
        transform.Rotate(Vector3.up - new Vector3(0, 180, 0));
    }
}
