using System;
using System.Collections;
using System.Collections.Generic;
using OpenCVForUnity.CoreModule;
using OpenCVForUnity.ImgprocModule;
using OpenCVForUnity.ObjdetectModule;
using OpenCVForUnity.UnityUtils;
using UnityEngine;
using UnityEngine.UI;

public class Display : MonoBehaviour
{
    private WebCamTexture webcamTexture;

    private string filename;

    private CascadeClassifier cascade;

    private MatOfRect faces;

    private Texture2D texture;

    private Mat rgbaMat;

    private Mat grayMat;

    public RawImage rawImage;

    [SerializeField]
    public Text debugLog;

    private Dictionary<string, string>
        eventLogs = new Dictionary<string, string>();

    void initializeData()
    {
        filename = "objdetect/lbpcascade_frontalface.xml";

        //initaliaze cascade classifier
        cascade = new CascadeClassifier();

        //load the xml file data
        cascade.load(Utils.getFilePath(filename));

        //initalize faces matofrect
        faces = new MatOfRect();

        //initialize rgb and gray Mats
        rgbaMat =
            new Mat(webcamTexture.height, webcamTexture.width, CvType.CV_8UC4);
        grayMat =
            new Mat(webcamTexture.height, webcamTexture.width, CvType.CV_8UC4);

        //initialize texture2d
        texture =
            new Texture2D(rgbaMat.cols(),
                rgbaMat.rows(),
                TextureFormat.RGBA32,
                false);
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

    void Start()
    {
        //obtain cameras avialable
        WebCamDevice[] cam_devices = WebCamTexture.devices;
        webcamTexture =
            new WebCamTexture(cam_devices[0].name,
                Screen.height / 4,
                Screen.width / 4,
                30);

        webcamTexture.Play();
        initializeData();
    }

    void Update()
    {
        if (webcamTexture.width < 100)
        {
            Debug.Log("Still waiting another frame for correct info...");
            return;
        }

        //convert webcamtexture to rgb mat
        Utils.webCamTextureToMat (webcamTexture, rgbaMat);

        //convert rgbmat to grayscale
        Imgproc.cvtColor(rgbaMat, grayMat, Imgproc.COLOR_RGBA2GRAY);

        //extract faces
        cascade.detectMultiScale(grayMat, faces, 1.1, 4);

        //store faces in array
        OpenCVForUnity.CoreModule.Rect[] rects = faces.toArray();

        UpdateDebugLog("Hit Count", rects.Length.ToString());

        //draw rectangle over all faces
        for (int i = 0; i < rects.Length; i++)
        {
            Imgproc
                .rectangle(rgbaMat,
                new Point(rects[i].x, rects[i].y),
                new Point(rects[i].x + rects[i].width,
                    rects[i].y + rects[i].height),
                new Scalar(255, 0, 0, 255),
                2);
        }
        Utils.matToTexture2D (rgbaMat, texture);
        rawImage.texture = texture;
    }

    public void ToggleRawImage()
    {
        rawImage.enabled = !rawImage.enabled;
    }

    public void ToggleDebugLog()
    {
        debugLog.enabled = !debugLog.enabled;
        debugLog.text =
            debugLog.enabled ? "Debug Log: Enabled" : "Debug Log: Disabled";
    }
}
