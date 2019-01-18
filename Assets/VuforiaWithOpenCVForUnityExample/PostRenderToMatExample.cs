using UnityEngine;
using System.Collections;

using OpenCVForUnity.CoreModule;
using OpenCVForUnity.ImgprocModule;
using OpenCVForUnity.UnityUtils;

/// <summary>
/// Post render to mat example.
/// </summary>
public class PostRenderToMatExample : MonoBehaviour
{
	
    public GameObject quad;
    public Camera mainCamera;
    Mat cameraMat;
    Texture2D cameraTexture;
    Texture2D outputTexture;
    Color32[] colors;
    Mat mSepiaKernel;
    private Size mSize0;
    private Mat mIntermediateMat;

    public enum modeType
    {
        original,
        sepia,
        pixelize,
    }

    public modeType mode;

    // Use this for initialization
    void Start ()
    {

        cameraMat = new Mat (Screen.height, Screen.width, CvType.CV_8UC4);
		
        Debug.Log ("imgMat dst ToString " + cameraMat.ToString ());
		
		
        cameraTexture = new Texture2D (cameraMat.cols (), cameraMat.rows (), TextureFormat.ARGB32, false);
        outputTexture = new Texture2D (cameraMat.cols (), cameraMat.rows (), TextureFormat.ARGB32, false);


        colors = new Color32[outputTexture.width * outputTexture.height];


        mainCamera.orthographicSize = cameraTexture.height / 2;

        quad.transform.localScale = new Vector3 (cameraTexture.width, cameraTexture.height, quad.transform.localScale.z);
        quad.GetComponent<Renderer> ().material.mainTexture = outputTexture;



        // sepia
        mSepiaKernel = new Mat (4, 4, CvType.CV_32F);
        mSepiaKernel.put (0, 0, /* R */0.189f, 0.769f, 0.393f, 0f);
        mSepiaKernel.put (1, 0, /* G */0.168f, 0.686f, 0.349f, 0f);
        mSepiaKernel.put (2, 0, /* B */0.131f, 0.534f, 0.272f, 0f);
        mSepiaKernel.put (3, 0, /* A */0.000f, 0.000f, 0.000f, 1f);

	
        // pixelize
        mIntermediateMat = new Mat ();
        mSize0 = new Size ();
    }
	
    // Update is called once per frame
    void Update ()
    {
	
    }

    void OnPostRender ()
    {

        UnityEngine.Rect rect = new UnityEngine.Rect (0, 0, cameraTexture.width, cameraTexture.height);
        cameraTexture.ReadPixels (rect, 0, 0, true);



        Utils.texture2DToMat (cameraTexture, cameraMat);


        if (mode == modeType.original) {
			
            Imgproc.putText (cameraMat, "ORIGINAL MODE " + cameraTexture.width + "x" + cameraTexture.height, new Point (5, cameraTexture.height - 5), Imgproc.FONT_HERSHEY_PLAIN, 1.0, new Scalar (255, 0, 0, 255));	
			
        } else if (mode == modeType.sepia) {

            Core.transform (cameraMat, cameraMat, mSepiaKernel);

            Imgproc.putText (cameraMat, "SEPIA MODE " + cameraTexture.width + "x" + cameraTexture.height, new Point (5, cameraTexture.height - 5), Imgproc.FONT_HERSHEY_PLAIN, 1.0, new Scalar (255, 0, 0, 255));

        } else if (mode == modeType.pixelize) {
            Imgproc.resize (cameraMat, mIntermediateMat, mSize0, 0.2, 0.2, Imgproc.INTER_NEAREST);
            Imgproc.resize (mIntermediateMat, cameraMat, cameraMat.size (), 0.0, 0.0, Imgproc.INTER_NEAREST);

            Imgproc.putText (cameraMat, "PIXELIZE MODE" + cameraTexture.width + "x" + cameraTexture.height, new Point (5, cameraTexture.height - 5), Imgproc.FONT_HERSHEY_PLAIN, 1.0, new Scalar (255, 0, 0, 255));

        }
				
				
        Utils.matToTexture2D (cameraMat, outputTexture, colors);
    }

    void OnGUI ()
    {
        float screenScale = Screen.width / 240.0f;
        Matrix4x4 scaledMatrix = Matrix4x4.Scale (new Vector3 (screenScale, screenScale, screenScale));
        GUI.matrix = scaledMatrix;
		
		
        GUILayout.BeginVertical ();
		
        if (GUILayout.Button ("Original")) {
            mode = modeType.original;
        }
		
        if (GUILayout.Button ("sepia")) {
            mode = modeType.sepia;
        }
		
        if (GUILayout.Button ("pixelize")) {
            mode = modeType.pixelize;
        }
		
		
        GUILayout.EndVertical ();
    }
}
