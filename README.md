Vuforia with OpenCV for Unity Example
====================

Demo Video
-----
[![](http://img.youtube.com/vi/TnF90ladrOo/sddefault.jpg)](https://www.youtube.com/watch?v=TnF90ladrOo)

Environment
-----
Windows 8.1  
Unity 5.5.1f1  
vuforia-unity-6-2-6.unitypackage  
SD_UnityChan-1.unitypackage  
[OpenCV for Unity](https://assetstore.unity.com/packages/tools/integration/opencv-for-unity-21088?aid=1011l4ehR) 2.1.3

In Unity 4.6, It seems the black screen problem occurs while using the combination of Vuforia and OpenCVForUnity.
I am investigating the cause.Unfortunately, please use the Unity5 at the moment.


Setup
-----
* Import vuforia-unity-6-2-6.unitypackage
* Import SD_UnityChan-1.unitypackage
* Setup Vuforia ([How To Setup a Simple Unity Project](https://library.vuforia.com/articles/Solution/Compiling-a-Simple-Unity-Project))
* Import OpenCVForUnity2.1.3 from AssetStore
* Import VuforiaWithOpenCVForUnityExample.unitypackage

![screenshot.png](screenshot.png) 

Examples
-----
**[CameraImageToMatExample.cs](/Assets/VuforiaWithOpenCVForUnityExample/CameraImageToMatExample.cs)**  
Conversion from CameraImage(without augmentation) of "Vuforia" to Mat of "OpenCV for Unity".  

**[PostRenderToMatExample.cs](/Assets/VuforiaWithOpenCVForUnityExample/PostRenderToMatExample.cs)**  
Conversion from PostRenderTexture(ARCamera) of "Vuforia" to Mat of "OpenCV for Unity".  
Attach "PostRenderToMatExample.cs" to "ARCamera/Camera".  


![Light_Frame.png](Light_Frame.png)


