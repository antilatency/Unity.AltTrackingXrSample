[![Antilatency SDK - 3.5.2](https://img.shields.io/badge/Antilatency_SDK-3.5.2-acc435?style=for-the-badge)](https://)

[![Unity - 2021.3.5f1](https://img.shields.io/badge/Unity-2021.3.5f1-787777?style=for-the-badge&logo=unity)](https://)

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
* For Vive Focus 3 headsets, add Vive [Wave XR plugin](https://developer.vive.com/resources/vive-wave/sdk/411/vive-wave-xr-plugin/)
* For Pico Neo 2/3 headsets, add Pico [Unity XR Platform SDK](https://developer.pico-interactive.com/sdk)