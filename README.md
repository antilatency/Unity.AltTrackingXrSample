[![Antilatency SDK - 3.5.2](https://img.shields.io/badge/Antilatency_SDK-3.5.2-acc435?style=for-the-badge)](https://developers.antilatency.com/Sdk/Configurator_en.html#{%22Release%22:%223.5.2%22,%22Target%22:%22Unity%22,%22TargetSettings%22:{%22MathTypes%22:%22UnityEngine.Math%22,%22UnityVersion%22:%222019.x%22,%22UnityComponents%22:true,%22Components%22:{%22AltTrackingComponents%22:true,%22AltEnvironmentComponents%22:true,%22BracerComponents%22:true,%22DeviceNetworkComponents%22:true,%22StorageClientComponents%22:true}},%22Libraries%22:{%22AltEnvironmentSelector%22:true,%22AltEnvironmentArbitrary2D%22:true,%22AltEnvironmentHorizontalGrid%22:true,%22AltEnvironmentPillars%22:true,%22DeviceNetwork%22:true,%22AltTracking%22:true,%22Bracer%22:true,%22HardwareExtensionInterface%22:true,%22RadioMetrics%22:true,%22TrackingAlignment%22:true,%22StorageClient%22:true},%22OS%22:{%22WindowsDesktop%22:{%22x86%22:true,%22x64%22:true},%22WindowsUWP%22:{%22x64%22:true,%22armeabi-v7a%22:true,%22arm64-v8a%22:true},%22Android%22:{%22aar%22:true}}})

[![Unity - 2021.3.5f1](https://img.shields.io/badge/Unity-2021.3.5f1-787777?style=for-the-badge&logo=unity)](https://unity3d.com/ru/unity/whats-new/2021.3.5)

## Summary

This project includes example implementation of headset tracking with Antilatency tracking system, [Tracking Alignment library](https://developers.antilatency.com/Software/Libraries/Antilatency_Tracking_Alignment_Library_en.html) and Unity XR platform.

You can use the XR controllers and interact with objects on the scene (orange cubes) using the grip buttons.

    .
    └── Assets
        └── Antilatency
            └── XrSample
                ├── Scenes        # Contains example scene AltXrSample.unity
                └── Scripts       # Contains example script AltTrackingXR.cs

## Adding new Unity XR plugins
This project includes standard Unity XR plugins (Windows MR, Oculus and etc.)

You can add support for other headsets using external XR plugins:
* For Vive Focus 3 headsets, add Vive [Wave XR plugin](https://hub.vive.com/storage/docs/en-us/UnityXR/UnityXRSdk.html)
* For Pico Neo 2/3 headsets, add Pico [Unity XR Platform SDK](https://developer.pico-interactive.com/sdk)