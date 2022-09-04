using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

[RequireComponent(typeof (ARPlaneManager))]
public class TogglePlaneDetection : MonoBehaviour
{
    private ARPlaneManager planeManager;

    [SerializeField]
    private Text toggleButtonText;

    // Start is called before the first frame update
    void Awake()
    {
        planeManager = GetComponent<ARPlaneManager>();
        toggleButtonText.text = "Plane Detection: Enabled";
    }

    public void PlaneDetectionToggle()
    {
        planeManager.enabled = !planeManager.enabled;
        toggleButtonText.text =
            planeManager.enabled
                ? "Plane Detection: Enabled"
                : "Plane Detection: Disabled";
        ToggleAllPlane(planeManager.enabled);
    }

    private void ToggleAllPlane(bool value)
    {
        foreach (var plane in planeManager.trackables)
        {
            plane.gameObject.SetActive (value);
        }
    }
}
