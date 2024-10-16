using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarkerManager : MonoBehaviour
{
    private List<MarkerController> markerList = new List<MarkerController>();
    public GameObject markerPrefab;

    [SerializeField]
    int lastTargetNmb = -1;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GenerateMarker(Transform target,int targetId)
    {
        int nmb = markerList.Count;
        GameObject marker = Instantiate(markerPrefab);
        MarkerController controller = marker.GetComponent<MarkerController>();
        controller.Initialized(target,nmb,targetId);
        marker.transform.SetParent(transform, false);
        markerList.Add(controller);
    }

    public void RemoveMarkers(int nmb)
    {
        for (int i = 0; i < markerList.Count; ++i)
        {
            if (nmb == markerList[i].GetSetTargetId)
            {
                markerList[i].GetComponent<MarkerController>().RemovedMarker();
                markerList.RemoveAt(i);
            }
        }
    }

    public void SetimageColor(int nowNmb)
    {
        
        if(nowNmb == -1)
        {
            lastTargetNmb = nowNmb;
            return;
        }

        int nowTargetNmb = nowNmb;

        if (nowTargetNmb != lastTargetNmb)
        {
            
            //markerManager.SetimageColor(nowTargetNmb, lastTargetNmb);
            for (int i = 0; i < markerList.Count; ++i)
            {
                if (lastTargetNmb == markerList[i].GetSetTargetId)
                {
                    markerList[i].SetNomalColor();
                }
                if (nowTargetNmb == markerList[i].GetSetTargetId)
                {
                    markerList[i].SetAimColor();
                }
            }
            lastTargetNmb = nowTargetNmb;

        }
        
    }

}
