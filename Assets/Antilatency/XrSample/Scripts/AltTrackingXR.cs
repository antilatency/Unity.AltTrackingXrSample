using System.Collections;
using System.Collections.Generic;
using System.Linq;

using UnityEngine;

using Antilatency;
using Antilatency.Integration;
using Antilatency.DeviceNetwork;

public class AltTrackingXR : AltTracking {
    public Camera XRCamera;
    public UnityEngine.SpatialTracking.TrackedPoseDriver HmdPoseDriver;

    private bool _lerpPosition;
    private bool _lerpRotation;

    private Antilatency.TrackingAlignment.ILibrary _alignmentLibrary;
    private Antilatency.TrackingAlignment.ITrackingAlignment _alignment;

    private bool _altInitialPositionApplied = false;
    private const float _bQuality = 0.15f;

    protected override NodeHandle GetAvailableTrackingNode() {
        return GetUsbConnectedFirstIdleTrackerNode();
    }

    protected override Pose GetPlacement() {
        var result = Pose.identity;

        using (var localStorage = Antilatency.Integration.StorageClient.GetLocalStorage()) {

            if (localStorage == null) {
                return result;
            }

            var placementCode = localStorage.read("placement", "default");

            if (string.IsNullOrEmpty(placementCode)) {
                Debug.LogError("Failed to get placement code");
                result = Pose.identity;
            } else {
                result = _trackingLibrary.createPlacement(placementCode);
            }

            return result;
        }
    }

    protected override void Awake() {
        base.Awake();

        _alignmentLibrary = Antilatency.TrackingAlignment.Library.load();

        var placement = GetPlacement();
        _alignment = _alignmentLibrary.createTrackingAlignment(Antilatency.Math.doubleQ.FromQuaternion(placement.rotation), ExtrapolationTime);

        if (XRCamera == null) {
            XRCamera = GetComponentInChildren<Camera>();
            if (XRCamera == null) {
                Debug.LogError("XR Camera is not setted and no cameras has been found in children gameobjects");
                enabled = false;
                return;
            } else {
                Debug.LogWarning("XR Camera: " + XRCamera.gameObject.name);
            }
        }

        _lerpPosition = HmdPoseDriver.trackingType == UnityEngine.SpatialTracking.TrackedPoseDriver.TrackingType.PositionOnly ||
                        HmdPoseDriver.trackingType == UnityEngine.SpatialTracking.TrackedPoseDriver.TrackingType.RotationAndPosition;

        _lerpRotation = HmdPoseDriver.trackingType == UnityEngine.SpatialTracking.TrackedPoseDriver.TrackingType.RotationOnly ||
                        HmdPoseDriver.trackingType == UnityEngine.SpatialTracking.TrackedPoseDriver.TrackingType.RotationAndPosition;
    }

    protected override void Update() {
        base.Update();

        var centerEye = UnityEngine.XR.InputDevices.GetDeviceAtXRNode(UnityEngine.XR.XRNode.CenterEye);

        if (!centerEye.isValid) {
            Debug.LogWarning("Center eye device is not valid");
            return;
        }

        // Do nothing if HMD is not putted on because some devices send invalid tracking data in such state.
        if (!centerEye.TryGetFeatureValue(UnityEngine.XR.CommonUsages.userPresence, out var userPresence) || !userPresence) {
            return;
        }

        if (!centerEye.TryGetFeatureValue(UnityEngine.XR.CommonUsages.trackingState, out var state)) {
            return;
        }

        var bPositionRecieved = centerEye.TryGetFeatureValue(UnityEngine.XR.CommonUsages.centerEyePosition, out var bPosition) && state.HasFlag(UnityEngine.XR.InputTrackingState.Position);
        var bRotationRecieved = centerEye.TryGetFeatureValue(UnityEngine.XR.CommonUsages.centerEyeRotation, out var bRotation) && state.HasFlag(UnityEngine.XR.InputTrackingState.Rotation);

        Debug.Log("bPositionRecieved: " + bPositionRecieved + ", bRotationRecieved: " + bRotationRecieved);

        bool altTrackingActive;
        Antilatency.Alt.Tracking.State trackingState;

        //bool bRotationRecieved = false;
        //bool bPositionRecieved = false;

        //var bPosition = Vector3.zero;
        //var bRotation = Quaternion.identity;

        //var states = new List<UnityEngine.XR.XRNodeState>();
		//UnityEngine.XR.InputTracking.GetNodeStates(states);

		//if (states.Exists(v => v.nodeType == UnityEngine.XR.XRNode.CenterEye)) {
		//	var centerEyeState = states.First(v => v.nodeType == UnityEngine.XR.XRNode.CenterEye);
		//	if (centerEyeState.tracked) {
		//		bRotationRecieved = centerEyeState.TryGetRotation(out bRotation);
		//		bPositionRecieved = centerEyeState.TryGetPosition(out bPosition);
		//	}
  //      } else {
  //          Debug.Log("No center eye");
  //      }

		//if (!bRotationRecieved) {
		//	if (states.Exists(v => v.nodeType == UnityEngine.XR.XRNode.LeftEye) && states.Exists(v => v.nodeType == UnityEngine.XR.XRNode.RightEye)) {
		//		var leftEyeState = states.First(v => v.nodeType == UnityEngine.XR.XRNode.LeftEye);
		//		var rightEyeState = states.First(v => v.nodeType == UnityEngine.XR.XRNode.RightEye);

		//		var leftEyePos = Vector3.zero;
		//		var rightEyePos = Vector3.zero;
		//		var leftEyeRot = Quaternion.identity;
		//		var rightEyeRot = Quaternion.identity;

		//		var leftEyePosRecieved = leftEyeState.TryGetPosition(out leftEyePos);
		//		var rightEyePosRecieved = rightEyeState.TryGetPosition(out rightEyePos);
		//		var leftEyeRotRecieved = leftEyeState.TryGetRotation(out leftEyeRot);
		//		var rightEyeRotRecieved = rightEyeState.TryGetRotation(out rightEyeRot);

		//		bRotationRecieved = leftEyeRotRecieved && rightEyeRotRecieved;
		//		bPositionRecieved = leftEyePosRecieved && rightEyePosRecieved;

		//		if (bPositionRecieved) {
		//			bPosition = Vector3.Lerp(leftEyePos, rightEyePos, 0.5f);
  //              } else {
  //                  Debug.Log("No position");
  //              }

		//		if (bRotationRecieved) {
		//			bRotation = Quaternion.Lerp(leftEyeRot, rightEyeRot, 0.5f);
  //              } else {
  //                  Debug.Log("No rotation");
  //              }
  //          }
		//}

		altTrackingActive = GetRawTrackingState(out trackingState);

        // If Alt is disconnected, we have nothing to do.
        if (!altTrackingActive) {
            return;
        }

		if (_lerpRotation && trackingState.stability.stage == Antilatency.Alt.Tracking.Stage.Tracking6Dof && bRotationRecieved) {
			var result = _alignment.update(
				Antilatency.Math.doubleQ.FromQuaternion(trackingState.pose.rotation), 
				Antilatency.Math.doubleQ.FromQuaternion(bRotation), 
				Time.realtimeSinceStartup);

			ExtrapolationTime = (float)result.timeBAheadOfA;
			_placement.rotation = result.rotationARelativeToB.ToQuaternion();
			transform.localRotation = result.rotationBSpace.ToQuaternion();
		}

        altTrackingActive = GetTrackingState(out trackingState);
        if (!altTrackingActive || trackingState.stability.stage == Antilatency.Alt.Tracking.Stage.InertialDataInitialization) {
            return;
        }

        if (!_lerpRotation) {
            transform.localRotation = trackingState.pose.rotation;
            XRCamera.transform.localRotation = Quaternion.identity;
        }
            
        if (_lerpPosition) {
            if (trackingState.stability.stage == Antilatency.Alt.Tracking.Stage.Tracking6Dof && bPositionRecieved) {
                var aWorldSpace = transform.parent.TransformPoint(trackingState.pose.position);
                var a = transform.parent.InverseTransformPoint(aWorldSpace);
                var bSpace = transform.localPosition;
                var b = transform.parent.InverseTransformPoint(transform.TransformPoint(bPosition));

                Vector3 averagePositionInASpace;

                if (!_altInitialPositionApplied) {
                    averagePositionInASpace = (b * 0.0f + a * 100.0f) / (100.0f + 0.0f);
                    _altInitialPositionApplied = true;
                } else {
                    averagePositionInASpace = (b * _bQuality + a * trackingState.stability.value) / (trackingState.stability.value + _bQuality);
                }

                transform.localPosition += averagePositionInASpace - b;
            }
        } else {
            transform.localPosition = trackingState.pose.position;
            XRCamera.transform.localPosition = Vector3.zero;
        }
    }

    private float Fx(float x, float k) {
        var xDk = x / k;
        return 1.0f / (xDk * xDk + 1);
    }
}
