using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DoorScript : MonoBehaviour
{
    private Animator anim;
    private BoxCollider boxCollider;
    public int ID;
    private bool PassingFlag = false;
    public bool OpenFlag = false;
    // Start is called before the first frame update
    void Start()
    {
      
        anim = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider>();
        boxCollider.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Open_Door()
    {
        boxCollider.enabled = true;
        OpenFlag = true;
        anim.SetBool("Open", true);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player" && !PassingFlag)
        {
            Debug.Log("第二ステージスタート");
            PassingFlag = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player" && PassingFlag)
        {
            anim.SetBool("Open", false);
            boxCollider.enabled = false;
        }
    }

    public bool GetSetPassingFlag
    {
        get { return PassingFlag; }
    }
}
