using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Events;

public class ButtonScript : MonoBehaviour
{
    private Animator animator;
    private bool buttonOnFlag;
    private CapsuleCollider collider;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        collider = GetComponent<CapsuleCollider>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //private void OnTriggerEnter(Collider other)
    //{
    //    Debug.Log(other.gameObject.tag);
    //    if (other.gameObject.tag == "Player")
    //    {
    //        animator.SetBool("OnButton", true);
    //        buttonOnFlag = true;
    //    }
    //}

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log(collision.gameObject.tag);
        if (collision.gameObject.tag == "Player")
        {
            animator.SetBool("OnButton", true);
            buttonOnFlag = true;
            collider.enabled = false;
        }
    }

    public bool GetButtonOnFlag
    {
        get { return buttonOnFlag; }
    }
}
