using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShotController : MonoBehaviour
{
    private Rigidbody rigid;
    public float addForce;
    private Vector3 Force;
    private bool timeStopFlag = false;
    private bool timeRestartFlag = false;
    public void Fire(Vector3 value)
    {
        //GetComponent<Rigidbody>().AddForce(value * 2000);

        Force = value.normalized * addForce;
    }
    private void OnCollisionEnter(Collision obj)
    {
        if (obj.gameObject.tag != "PlayerShot")
        {
            Destroy(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        rigid = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {

        if (timeStopFlag)
        {
            Destroy(gameObject);
        }
        if (transform.position.y <= -3)
        {
            Destroy(gameObject);
        }

        rigid.velocity = Force;
    }

    public bool SetTimeFlag
    {
        set { timeStopFlag = value; }
    }

    public bool SetTimeRestartFlag
    {
        set { timeRestartFlag = value; }
    }
}
