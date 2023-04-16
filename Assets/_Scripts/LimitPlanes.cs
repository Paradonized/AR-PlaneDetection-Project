using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class LimitPlanes : MonoBehaviour
{
    public ARPlane[] planes;
    public ARPlaneManager arPlaneManager;
    public TMP_Text debugMessage;

    // Update is called once per frame
    void Update()
    {
       if(arPlaneManager.enabled)
       {
            planes = (ARPlane[])ARPlane.FindObjectsOfType(typeof(ARPlane));
            //debugMessage.SetText(planes.Length.ToString());
            if (planes.Length >= 1)
            {
                //arPlaneManager.enabled = false;
            }
       }
    }
}
