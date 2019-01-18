Vuforia with OpenCV for Unity Example
====================

Demo Video
-----
[![](http://img.youtube.com/vi/TnF90ladrOo/sddefault.jpg)](https://www.youtube.com/watch?v=TnF90ladrOo)

Environment
-----
Windows 10  
Unity 2017.4.14f1  
[OpenCV for Unity](https://assetstore.unity.com/packages/tools/integration/opencv-for-unity-21088?aid=1011l4ehR) 2.3.3

Setup
-----
* Setup Vuforia ([Getting Started with Vuforia Engine in Unity](https://library.vuforia.com/articles/Training/getting-started-with-vuforia-in-unity.html))
* Import OpenCVForUnity2.3.3 from AssetStore
* Import VuforiaWithOpenCVForUnityExample.unitypackage

Examples
-----
**[CameraImageToMatExample.cs](/Assets/VuforiaWithOpenCVForUnityExample/CameraImageToMatExample.cs)**  
Conversion from CameraImage(without augmentation) of "Vuforia" to Mat of "OpenCV for Unity".  
![screenshot1.PNG](screenshot1.PNG) 

**[PostRenderToMatExample.cs](/Assets/VuforiaWithOpenCVForUnityExample/PostRenderToMatExample.cs)**  
Conversion from PostRenderTexture(ARCamera) of "Vuforia" to Mat of "OpenCV for Unity".  
![screenshot2.PNG](screenshot2.PNG) 


![Light_Frame.png](Light_Frame.png)


