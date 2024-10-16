using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public Camera targetCamera;
    public Transform target;
    public Transform targetUI;
    private Vector3 worldOffset;
    private RectTransform parentUI;
    // Start is called before the first frame update
    void Start()
    {
        parentUI = targetUI.parent.GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnUpdatePosition()
    {

    }

    
}
