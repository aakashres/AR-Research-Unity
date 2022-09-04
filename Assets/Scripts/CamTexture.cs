using System;
using System.Collections;
using System.Collections.Generic;
using OpenCVForUnity.CoreModule;
using OpenCVForUnity.ImgprocModule;
using OpenCVForUnity.ObjdetectModule;
using OpenCVForUnity.UnityUtils;
using UnityEngine;
using UnityEngine.UI;

public class CamTexture : MonoBehaviour
{
    private WebCamTexture webcamTexture;

    private Texture2D texture;

    private Mat rgbaMat;

    public RawImage rawImage;

    private bool hasInit = false;

    // Start is called before the first frame update
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

        //initialize rgb and gray Mats
        rgbaMat =
            new Mat(webcamTexture.height, webcamTexture.width, CvType.CV_8UC4);

        //initialize texture2d
        texture =
            new Texture2D(rgbaMat.cols(),
                rgbaMat.rows(),
                TextureFormat.RGBA32,
                false);
        hasInit = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (hasInit)
        {
            if (webcamTexture.didUpdateThisFrame)
            {
                Utils.webCamTextureToMat(webcamTexture, rgbaMat, true);
                Imgproc.cvtColor(rgbaMat, rgbaMat, Imgproc.COLOR_RGBA2RGB);
                Utils.matToTexture2D(rgbaMat, texture, true);
                rawImage.texture = texture;
            }
        }
    }

    public void ToggleRawImage()
    {
        rawImage.enabled = !rawImage.enabled;
    }
}
