[![Antilatency SDK - 4.0.0](https://img.shields.io/badge/Antilatency_SDK-4.0.0-acc435?style=for-the-badge)](https://developers.antilatency.com/Sdk/Configurator_ru.html#{%22Release%22:%224.0.0%22,%22Target%22:%22Unity%22,%22TargetSettings%22:{%22MathTypes%22:%22UnityEngine.Math%22,%22UnityVersion%22:%222019.x%22,%22UnityComponents%22:true,%22Components%22:{%22AltTrackingComponents%22:true,%22AltEnvironmentComponents%22:true,%22BracerComponents%22:true,%22DeviceNetworkComponents%22:true,%22StorageClientComponents%22:true}},%22Libraries%22:{%22AltEnvironmentSelector%22:true,%22AltEnvironmentArbitrary2D%22:true,%22AltEnvironmentHorizontalGrid%22:true,%22AltEnvironmentPillars%22:true,%22AltEnvironmentAdditionalMarkers%22:true,%22DeviceNetwork%22:true,%22AltTracking%22:true,%22Bracer%22:true,%22HardwareExtensionInterface%22:true,%22RadioMetrics%22:true,%22TrackingAlignment%22:true,%22StorageClient%22:true,%22StereoGlasses%22:false,%22IllumetryDisplay%22:false},%22OS%22:{%22WindowsDesktop%22:{%22x86%22:true,%22x64%22:true},%22WindowsUWP%22:{%22x64%22:true,%22armeabi-v7a%22:true,%22arm64-v8a%22:true},%22Android%22:{%22aar%22:true},%22Linux%22:{%22x86_64%22:true}}})

[![Unity - 2021.3.5f1](https://img.shields.io/badge/Unity-2021.3.5f1-787777?style=for-the-badge&logo=unity)](https://unity3d.com/ru/unity/whats-new/2021.3.5)

## Summary

This project contains the integration of the [Unity.AltTrackingXrPackage](https://github.com/antilatency/Unity.AltTrackingXrPackage). The main scene [`AltXrSample.unity`](./Assets/Antilatency/XrSample/Scenes/AltXrSample.unity) includes an example implementation of `AltTrackingXrRig.prefab`. 

You can use the XR controllers and interact with objects on the scene (orange cubes) using the grip buttons:

https://user-images.githubusercontent.com/69207595/200628930-58a11cd7-6ed9-4858-8f7c-9a42feb73bef.mp4

## Sample structure
    .
    └── Assets
        └── Antilatency
            └── XrSample
                └── Scenes        # contains example scene AltXrSample.unity

## Adding new Unity XR plugins
This project includes standard Unity XR plugins (Windows MR, Oculus and etc.)

You can add support for other headsets using external XR plugins:
* For Vive Focus 3 headsets, add the [Vive Wave XR plugin](https://hub.vive.com/storage/docs/en-us/UnityXR/UnityXRSdk.html)
* For Pico headsets, add the [Pico Unity Integration SDK](https://developer.pico-interactive.com/sdk)
