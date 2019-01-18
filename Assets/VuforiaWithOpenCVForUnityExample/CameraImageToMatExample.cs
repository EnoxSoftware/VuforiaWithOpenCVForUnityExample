using UnityEngine;
using System.Collections;

using Vuforia;
using OpenCVForUnity.CoreModule;
using OpenCVForUnity.ImgprocModule;
using OpenCVForUnity.UnityUtils;

/// <summary>
/// Camera image to mat sample.
/// https://library.vuforia.com/content/vuforia-library/en/articles/Solution/Working-with-the-Camera.html#How-To-Access-the-Camera-Image-in-Unity
/// </summary>
public class CameraImageToMatExample : MonoBehaviour
{

    private Image.PIXEL_FORMAT mPixelFormat = Image.PIXEL_FORMAT.UNKNOWN_FORMAT;

    private bool mAccessCameraImage = true;
    private bool mFormatRegistered = false;

    public GameObject quad;
    public Camera mainCamera;
    Mat inputMat;
    Texture2D outputTexture;


    void Start ()
    {

        #if UNITY_EDITOR
        mPixelFormat = Image.PIXEL_FORMAT.GRAYSCALE; // Need Grayscale for Editor
        #else
        mPixelFormat = Image.PIXEL_FORMAT.RGB888; // Use RGB888 for mobile
        #endif

        // Register Vuforia life-cycle callbacks:
        VuforiaARController.Instance.RegisterVuforiaStartedCallback (OnVuforiaStarted);
        VuforiaARController.Instance.RegisterTrackablesUpdatedCallback (OnTrackablesUpdated);
        VuforiaARController.Instance.RegisterOnPauseCallback (OnPause);

    }

    void OnVuforiaStarted ()
    {

        // Try register camera image format
        if (CameraDevice.Instance.SetFrameFormat (mPixelFormat, true)) {
            Debug.Log ("Successfully registered pixel format " + mPixelFormat.ToString ());

            mFormatRegistered = true;
        } else {
            Debug.LogError (
                "\nFailed to register pixel format: " + mPixelFormat.ToString () +
                "\nThe format may be unsupported by your device." +
                "\nConsider using a different pixel format.\n");

            mFormatRegistered = false;
        }

    }

    /// <summary>
    /// Called each time the Vuforia state is updated
    /// </summary>
    void OnTrackablesUpdated ()
    {
        if (mFormatRegistered) {
            if (mAccessCameraImage) {
                Vuforia.Image image = CameraDevice.Instance.GetCameraImage (mPixelFormat);

                if (image != null) {
//                    Debug.Log (
//                        "\nImage Format: " + image.PixelFormat +
//                        "\nImage Size:   " + image.Width + "x" + image.Height +
//                        "\nBuffer Size:  " + image.BufferWidth + "x" + image.BufferHeight +
//                        "\nImage Stride: " + image.Stride + "\n"
//                    );

//                    byte[] pixels = image.Pixels;
//
//                    if (pixels != null && pixels.Length > 0)
//                    {
//                        Debug.Log(
//                            "\nImage pixels: " +
//                            pixels[0] + ", " +
//                            pixels[1] + ", " +
//                            pixels[2] + ", ...\n"
//                        );
//                    }

                    if (inputMat == null) {
                        if (mPixelFormat == Image.PIXEL_FORMAT.GRAYSCALE) {
                            inputMat = new Mat (image.Height, image.Width, CvType.CV_8UC1);
                        } else if (mPixelFormat == Image.PIXEL_FORMAT.RGB888) {
                            inputMat = new Mat (image.Height, image.Width, CvType.CV_8UC3);
                        }
                        //Debug.Log ("inputMat dst ToString " + inputMat.ToString ());
                    }
                    
                    
                    inputMat.put (0, 0, image.Pixels);
                    
                    Imgproc.putText (inputMat, "CameraImageToMatSample " + inputMat.cols () + "x" + inputMat.rows (), new Point (5, inputMat.rows () - 5), Imgproc.FONT_HERSHEY_PLAIN, 1.0, new Scalar (255, 0, 0, 255));
                    
                    
                    if (outputTexture == null) {
                        outputTexture = new Texture2D (inputMat.cols (), inputMat.rows (), TextureFormat.RGBA32, false);
                    }
                    
                    Utils.matToTexture2D (inputMat, outputTexture);
                    
                    
                    quad.transform.localScale = new Vector3 ((float)image.Width, (float)image.Height, 1.0f);
                    quad.GetComponent<Renderer> ().material.mainTexture = outputTexture;
                    
                    mainCamera.orthographicSize = image.Height / 2;
                }
            }
        }
    }

    /// <summary>
    /// Called when app is paused / resumed
    /// </summary>
    void OnPause (bool paused)
    {
        if (paused) {
            Debug.Log ("App was paused");
            UnregisterFormat ();
        } else {
            Debug.Log ("App was resumed");
            RegisterFormat ();
        }
    }

    /// <summary>
    /// Register the camera pixel format
    /// </summary>
    void RegisterFormat ()
    {
        if (CameraDevice.Instance.SetFrameFormat (mPixelFormat, true)) {
            Debug.Log ("Successfully registered camera pixel format " + mPixelFormat.ToString ());
            mFormatRegistered = true;
        } else {
            Debug.LogError ("Failed to register camera pixel format " + mPixelFormat.ToString ());
            mFormatRegistered = false;
        }
    }

    /// <summary>
    /// Unregister the camera pixel format (e.g. call this when app is paused)
    /// </summary>
    void UnregisterFormat ()
    {
        Debug.Log ("Unregistering camera pixel format " + mPixelFormat.ToString ());
        CameraDevice.Instance.SetFrameFormat (mPixelFormat, false);
        mFormatRegistered = false;
    }
        
}
