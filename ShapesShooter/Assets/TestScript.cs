using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestScript : MonoBehaviour
{
    public GameObject SphereObj;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        var Dir = SphereObj.transform.position - transform.position;
        Debug.Log(Dir.magnitude);

        if(Dir.magnitude　>= 10)
        {
            Debug.Log("攻撃パターン１");
        }
        else
        {
            Debug.Log("攻撃パターン２");
        }
    }
}
