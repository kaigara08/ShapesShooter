using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    public Transform playerObject;
    private Vector3 lastPlayerPosition;

    // Start is called before the first frame update
    void Start()
    {
        lastPlayerPosition = playerObject.position;
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    private void Move()
    {
        transform.position += playerObject.position - lastPlayerPosition;
        lastPlayerPosition = playerObject.position;
    }
}
