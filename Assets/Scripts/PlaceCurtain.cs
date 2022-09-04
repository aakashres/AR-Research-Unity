using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Experimental.XR;
using UnityEngine.UI;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class PlaceCurtain : MonoBehaviour
{
    public GameObject oneSideCurtainPrefab;

    public GameObject threeSideCurtainPrefab;

    public GameObject fourSideCurtainPrefab;

    [SerializeField]
    public Text debugLog;

    private int activeCurtain;

    private GameObject oneSideCurtain;

    private GameObject threeSideCurtain;

    private GameObject fourSideCurtain;

    private ARRaycastManager raycastManager;

    static List<ARRaycastHit> hits = new List<ARRaycastHit>();

    private Dictionary<string, string>
        eventLogs = new Dictionary<string, string>();

    void Start()
    {
        eventLogs.Add("Hit Count", "0");
        eventLogs.Add("Active Curtain", "None");
        eventLogs.Add("Child Count", "None");
        eventLogs.Add("Object Position 3d", "None");
        eventLogs.Add("Object Position 2d", "None");
        debugLog.text = "Debug Log: Enabled";
    }

    // Start is called before the first frame update
    void Awake()
    {
        raycastManager = FindObjectOfType<ARRaycastManager>();
    }

    private void UpdateDebugLog(string key, string logmsg)
    {
        eventLogs[key] = logmsg;
        string log = "Debug Log\n";
        foreach (var item in eventLogs)
        {
            log += item.Key + ": " + item.Value + "\n";
        }
        debugLog.text = log;
    }

    void Update()
    {
        UpdateDebugLog("Hit Count", hits.Count.ToString());
    }

    public void AddOneSideCurtain()
    {
        DestoryAllExcept(1);
        var screenCenter =
            Camera.main.ViewportToScreenPoint(new Vector3(0.5f, 0.5f));
        if (raycastManager.Raycast(screenCenter, hits, TrackableType.Planes))
        {
            var hitPose = hits[0].pose;
            if (oneSideCurtain == null)
            {
                oneSideCurtain =
                    Instantiate(oneSideCurtainPrefab,
                    hitPose.position,
                    hitPose.rotation);
            }
        }
        UpdateDebugLog("Active Curtain", "One Sided");
        activeCurtain = 1;
    }

    public void AddThreeSideCurtain()
    {
        DestoryAllExcept(3);
        var screenCenter =
            Camera.main.ViewportToScreenPoint(new Vector3(0.5f, 0.5f));
        if (raycastManager.Raycast(screenCenter, hits, TrackableType.Planes))
        {
            var hitPose = hits[0].pose;
            if (threeSideCurtain == null)
            {
                threeSideCurtain =
                    Instantiate(threeSideCurtainPrefab,
                    hitPose.position,
                    hitPose.rotation);
            }
        }
        UpdateDebugLog("Active Curtain", "Three Sided");
        activeCurtain = 3;
    }

    public void AddFourSideCurtain()
    {
        DestoryAllExcept(4);
        var screenCenter =
            Camera.main.ViewportToScreenPoint(new Vector3(0.5f, 0.5f));
        if (raycastManager.Raycast(screenCenter, hits, TrackableType.Planes))
        {
            var hitPose = hits[0].pose;
            if (fourSideCurtain == null)
            {
                fourSideCurtain =
                    Instantiate(fourSideCurtainPrefab,
                    hitPose.position,
                    hitPose.rotation);
            }
        }
        UpdateDebugLog("Active Curtain", "Four Sided");
        activeCurtain = 4;
    }

    private void DestoryAllExcept(int curtainType)
    {
        if (curtainType == 1)
        {
            DestroyIfNotNull (threeSideCurtain);
            DestroyIfNotNull (fourSideCurtain);
        }
        else if (curtainType == 3)
        {
            DestroyIfNotNull (oneSideCurtain);
            DestroyIfNotNull (fourSideCurtain);
        }
        else if (curtainType == 4)
        {
            DestroyIfNotNull (oneSideCurtain);
            DestroyIfNotNull (threeSideCurtain);
        }
    }

    public void DestoryAllCurtains()
    {
        DestroyIfNotNull (oneSideCurtain);
        DestroyIfNotNull (threeSideCurtain);
        DestroyIfNotNull (fourSideCurtain);
        UpdateDebugLog("Active Curtain", "None");
    }

    private void DestroyIfNotNull(GameObject gameObject)
    {
        if (gameObject != null)
        {
            Destroy (gameObject);
        }
    }

    public void ToggleDebugLog()
    {
        debugLog.enabled = !debugLog.enabled;
        debugLog.text =
            debugLog.enabled ? "Debug Log: Enabled" : "Debug Log: Disabled";
    }

    public void AlphaChange(Slider slider)
    {
        if (activeCurtain == 1)
        {
            foreach (Transform child in oneSideCurtain.transform)
            {
                Renderer renderer = child.gameObject.GetComponent<Renderer>();
                if (renderer != null)
                {
                    Color color = renderer.material.color;
                    color.a = slider.value;
                    renderer.material.color = color;
                }
            }
            UpdateDebugLog("Child Count",
            oneSideCurtain.transform.childCount.ToString());
        }
        else if (activeCurtain == 3)
        {
            foreach (Transform child in threeSideCurtain.transform)
            {
                Renderer renderer = child.gameObject.GetComponent<Renderer>();
                if (renderer != null)
                {
                    Color color = renderer.material.color;
                    color.a = slider.value;
                    renderer.material.color = color;
                }
            }
            UpdateDebugLog("Child Count",
            threeSideCurtain.transform.childCount.ToString());
        }
        else if (activeCurtain == 4)
        {
            foreach (Transform child in fourSideCurtain.transform)
            {
                Renderer renderer = child.gameObject.GetComponent<Renderer>();
                if (renderer != null)
                {
                    Color color = renderer.material.color;
                    color.a = slider.value;
                    renderer.material.color = color;
                }
            }
            UpdateDebugLog("Child Count",
            fourSideCurtain.transform.childCount.ToString());
        }
    }

    public void FindObjectFeature()
    {
        Vector3 position = Vector3.zero;
        if (activeCurtain == 1)
        {
            position = oneSideCurtain.transform.position;
        }
        else if (activeCurtain == 3)
        {
            position = threeSideCurtain.transform.position;
        }
        else if (activeCurtain == 4)
        {
            position = fourSideCurtain.transform.position;
        }
        UpdateDebugLog("Object Position 3d", position.ToString());
        UpdateDebugLog("Object Position 2d",
        Camera.main.WorldToScreenPoint(position).ToString());
    }

    public void ToggleAlertText()
    {
        if (activeCurtain == 1)
        {
            foreach (Transform child in oneSideCurtain.transform)
            {
                if (child.gameObject.name == "AlertText")
                {
                    child.gameObject.SetActive(!child.gameObject.activeSelf);
                }
            }
        }
    }
}
