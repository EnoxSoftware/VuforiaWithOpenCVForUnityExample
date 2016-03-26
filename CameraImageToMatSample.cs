using UnityEngine;
using System.Collections;

using Vuforia;
using OpenCVForUnity;

/// <summary>
/// Camera image to mat sample.
/// https://developer.vuforia.com/library/articles/Solution/How-To-Access-the-Camera-Image-in-Unity
/// </summary>
public class CameraImageToMatSample : MonoBehaviour
{


	//    private Image.PIXEL_FORMAT m_PixelFormat = Image.PIXEL_FORMAT.RGB888;
	private Image.PIXEL_FORMAT m_PixelFormat = Image.PIXEL_FORMAT.GRAYSCALE;
	private bool m_RegisteredFormat = false;
	private bool m_LogInfo = true;
	public GameObject quad;
	public Camera mainCamera;
	Mat inputMat;
	Texture2D outputTexture;
	
	void Start ()
	{
		VuforiaBehaviour qcarBehaviour = (VuforiaBehaviour)FindObjectOfType (typeof(VuforiaBehaviour));
		if (qcarBehaviour) {
			qcarBehaviour.RegisterTrackablesUpdatedCallback (OnTrackablesUpdated);
		}
	}
	
	public void OnTrackablesUpdated ()
	{
		if (!m_RegisteredFormat) {
			CameraDevice.Instance.SetFrameFormat (m_PixelFormat, true);
			m_RegisteredFormat = true;
		}
		
		CameraDevice cam = CameraDevice.Instance;
		Image image = cam.GetCameraImage (m_PixelFormat);
		if (image == null) {
			Debug.Log (m_PixelFormat + " image is not available yet");
		} else {
			
			if (inputMat == null) {
				inputMat = new Mat (image.Height, image.Width, CvType.CV_8UC1);
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
