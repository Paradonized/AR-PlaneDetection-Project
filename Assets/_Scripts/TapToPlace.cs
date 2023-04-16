using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using TMPro;
using UnityEngine.SceneManagement;

public class TapToPlace : MonoBehaviour
{
    public TMP_Text debugMessage;
    public GameObject tapVisualizer;
    public Vector2 touchPos;
    public ARRaycastManager arRaycastManager;
    private List<ARRaycastHit> hits = new List<ARRaycastHit>();
    public Camera arCamera;
    private bool result;
    public GameObject placedObject;
    public GameObject placedPrefab;
    public GameObject placedPrefabHorizontal;
    public GameObject placedPrefabVertical;
    public ARPlaneManager arPlaneManager;
    public GameObject choisePanel;
    public ARSession arSession;

    void Start()
    {
        debugMessage.SetText("Select Portal Type.");
        arPlaneManager.enabled = false;
    }

    void Update()
    {
        //debugMessage.SetText(arPlaneManager.requestedDetectionMode.ToString());
        if (Input.touchCount > 0)
        {
            touchPos = Input.GetTouch(0).position;
            Ray ray = arCamera.ScreenPointToRay(touchPos);
            tapVisualizer.SetActive(true);
            tapVisualizer.transform.position = touchPos;
            result = arRaycastManager.Raycast(ray, hits, trackableTypes: UnityEngine.XR.ARSubsystems.TrackableType.PlaneWithinPolygon);
            //debugMessage.SetText(result.ToString());
            if (result)
            {
                Pose hitPose = hits[0].pose;
                if (placedObject == null)
                {
                    placedObject = Instantiate(placedPrefab, hitPose.position, hitPose.rotation);
                    arPlaneManager.SetTrackablesActive(false);
                    arPlaneManager.enabled = false;
                    debugMessage.transform.parent.gameObject.SetActive(false);
                }
            }
        }
    }

    public void HorizontalBtn()
    {
        arPlaneManager.enabled = true;
        arPlaneManager.requestedDetectionMode = UnityEngine.XR.ARSubsystems.PlaneDetectionMode.Horizontal;
        placedPrefab = placedPrefabHorizontal;
        choisePanel.SetActive(false);
        debugMessage.SetText("Slowly scan for flat horizontal surfaces, then tap to spawn a protal.");
    }
    public void VerticalBtn()
    {
        arPlaneManager.enabled = true;
        arPlaneManager.requestedDetectionMode = UnityEngine.XR.ARSubsystems.PlaneDetectionMode.Vertical;
        placedPrefab = placedPrefabVertical;
        choisePanel.SetActive(false);
        debugMessage.SetText("Slowly scan for flat vertical surfaces, then tap to spawn a protal.");
    }
    public void ResetBtn()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
