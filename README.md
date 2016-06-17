Vuforia with OpenCV for Unity Sample
====================

Demo Video
-----
[![](http://img.youtube.com/vi/TnF90ladrOo/2.jpg)](https://www.youtube.com/watch?v=TnF90ladrOo)

Environment
-----
OSX Mavericks  
Unity 5.0.0f4

In Unity 4.6, It seems the black screen problem occurs while using the combination of Vuforia and OpenCVForUnity.
I am investigating the cause.Unfortunately, please use the Unity5 at the moment.


Setup
-----
* Import vuforia-unity-mobile-android-ios-4-0-105.unitypackage  
* Import OpenCVForUnity2.0.0 from AssetStore  
* Setup Tutorial(Image Targets in Unity <https://developer.vuforia.com/library/articles/Training/Image-Targets-in-Unity>)


Samples
-----
**[CameraImageToMatSample.cs](CameraImageToMatSample.cs)**  
Conversion from CameraImage(without augmentation) of "Vuforia" to Mat of "OpenCV for Unity".  

**[PostRenderToMatSample.cs](PostRenderToMatSample.cs)**  
Conversion from PostRenderTexture(ARCamera) of "Vuforia" to Mat of "OpenCV for Unity".  
Attach "PostRenderToMatSample.cs" to "ARCamera/Camera".  



