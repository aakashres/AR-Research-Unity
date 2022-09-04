using System;
using System.Collections;
using System.Collections.Generic;
using OpenCVForUnity.CoreModule;
using OpenCVForUnity.FaceModule;
using OpenCVForUnity.ImgprocModule;
using OpenCVForUnity.ObjdetectModule;
using OpenCVForUnity.UnityUtils;
using OpenCVForUnity.UnityUtils.Helper;
using OpenCVForUnity.UtilsModule;
using UnityEngine;
using UnityEngine.UI;

using Rect = OpenCVForUnity.CoreModule.Rect;

[RequireComponent(typeof (WebCamTextureToMatHelper))]
public class EmotionDetection : MonoBehaviour
{
    public RawImage imgDisplay;

    Mat grayMat;

    Texture2D texture;

    CascadeClassifier cascade;

    MatOfRect faces;

    WebCamTextureToMatHelper webCamTextureToMatHelper;

    FpsMonitor fpsMonitor;

    protected static readonly string
        FACEMARK_CASCADE_FILENAME = "objdetect/lbpcascade_frontalface.xml";

    string facemark_cascade_filepath;

    protected static readonly string
        EMOTION_DETECTION_MODEL = "emotion/emotion_model.tflite";

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }
}
