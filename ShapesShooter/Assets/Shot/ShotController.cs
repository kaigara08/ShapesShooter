using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotController : MonoBehaviour
{
    private Vector3 Force;
    Rigidbody rb;
    public void Fire(Vector3 value)
    {
        //GetComponent<Rigidbody>().AddForce(value * 4000);
        Force = value.normalized * 50.0f;
    }

    private void OnCollisionEnter(Collision obj)
    {
        if (obj.gameObject.tag != "EnemyShot")
        {
            Destroy(gameObject);
        }

    }

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if(transform.position.y<=-3)
        {
            Destroy(gameObject);
        }

        rb.velocity = Force;
    }
}
