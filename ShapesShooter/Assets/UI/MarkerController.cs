using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MarkerController : MonoBehaviour
{
    private Transform target;

    private RectTransform parentUI;

    private RectTransform rectTrans;

    private Color32 imageColor = new Color32(255, 255, 255, 80);

    public Sprite[] image = new Sprite[2];

    [SerializeField]
    private int markerId;

    [SerializeField]
    private int targetId;

    public void Initialized(Transform target, int nmb,int targetId)
    {
        this.target = target;
        this.markerId = nmb;
        this.targetId = targetId;
    }
    // Start is called before the first frame update
    void Start()
    {
        parentUI = transform.parent.gameObject.GetComponent<RectTransform>();
        rectTrans = GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void Update()
    {
        Render();
       
    }

    private void Render()
    {
        var targetWorldPos = target.position;

        var targetScreenPos = Camera.main.WorldToScreenPoint(targetWorldPos);

        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            parentUI,
            targetScreenPos,
            null,
            out var uiLocalPos
            );

        rectTrans.localPosition = uiLocalPos;
    }

  
    public void RemovedMarker()
    {
        Destroy(gameObject);
    }

    public void SetAimColor()
    {
        imageColor = new Color32(0, 255, 255, 150);
        GetComponent<Image>().sprite = image[1];
        GetComponent<Image>().color = imageColor;
        Debug.Log("MarkerTargetNmb:" + targetId);
    }

    public void SetNomalColor()
    {
        imageColor = new Color32(255, 255, 255, 150);
        GetComponent<Image>().sprite = image[0];
        GetComponent<Image>().color = imageColor;
    }

    public int GetSetTargetId
    {
        get { return targetId; }
        set { targetId = value; }
    }
}
