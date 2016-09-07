﻿// Device Finder|Scripts|0030
namespace VRTK
{
    using UnityEngine;

    /// <summary>
    /// The Device Finder offers a collection of static methods that can be called to find common game devices such as the headset or controllers, or used to determine key information about the connected devices.
    /// </summary>
    public class VRTK_DeviceFinder : MonoBehaviour
    {
        public enum ControllerHand
        {
            None,
            Left,
            Right
        }

        /// <summary>
        /// The TrackedIndexIsController method is used to determine if a given tracked object index belongs to a tracked controller.
        /// </summary>
        /// <param name="index">The index of the tracked object to find.</param>
        /// <returns>Returns true if the given index is a tracked object of type controller.</returns>
        public static bool TrackedIndexIsController(uint index)
        {
            return VRTK_SDK_Bridge.TrackedIndexIsController(index);
        }

        /// <summary>
        /// The GetControllerIndex method is used to find the index of a given controller object.
        /// </summary>
        /// <param name="controller">The controller object to check the index on.</param>
        /// <returns>The index of the given controller.</returns>
        public static uint GetControllerIndex(GameObject controller)
        {
            return VRTK_SDK_Bridge.GetIndexOfTrackedObject(controller);
        }

        /// <summary>
        /// The TrackedObjectByIndex method is used to find the GameObject of a tracked object by its generated index.
        /// </summary>
        /// <param name="index">The index of the tracked object to find.</param>
        /// <returns>The tracked object that matches the given index.</returns>
        public static GameObject TrackedObjectByIndex(uint index)
        {
            return VRTK_SDK_Bridge.GetTrackedObjectByIndex(index);
        }

        /// <summary>
        /// The TrackedObjectOrigin method is used to find the tracked object's origin.
        /// </summary>
        /// <param name="obj">The GameObject to get the origin for.</param>
        /// <returns>The transform of the tracked object's origin or if an origin is not set then the transform parent.</returns>
        public static Transform TrackedObjectOrigin(GameObject obj)
        {
            return VRTK_SDK_Bridge.GetTrackedObjectOrigin(obj);
        }

        /// <summary>
        /// The GetControllerHandType method is used for getting the enum representation of ControllerHand from a given string.
        /// </summary>
        /// <param name="hand">The string representation of the hand to retrieve the type of. `left` or `right`.</param>
        /// <returns>A ControllerHand representing either the Left or Right hand.</returns>
        public static ControllerHand GetControllerHandType(string hand)
        {
            switch (hand.ToLower())
            {
                case "left":
                    return ControllerHand.Left;
                case "right":
                    return ControllerHand.Right;
                default:
                    return ControllerHand.None;
            }
        }

        /// <summary>
        /// The GetControllerHand method is used for getting the enum representation of ControllerHand for the given controller game object.
        /// </summary>
        /// <param name="controller">The controller game object to check the hand of.</param>
        /// <returns>A ControllerHand representing either the Left or Right hand.</returns>
        public static ControllerHand GetControllerHand(GameObject controller)
        {
            if (VRTK_SDK_Bridge.IsControllerLeftHand(controller))
            {
                return ControllerHand.Left;
            }
            else if (VRTK_SDK_Bridge.IsControllerRightHand(controller))
            {
                return ControllerHand.Right;
            }
            else
            {
                return ControllerHand.None;
            }
        }

        /// <summary>
        /// The IsControllerOfHand method is used to check if a given controller game object is of the hand type provided.
        /// </summary>
        /// <param name="checkController">The actual controller object that is being checked.</param>
        /// <param name="hand">The representation of a hand to check if the given controller matches.</param>
        /// <returns>Is true if the given controller matches the given hand.</returns>
        public static bool IsControllerOfHand(GameObject checkController, ControllerHand hand)
        {
            if (hand == ControllerHand.Left && VRTK_SDK_Bridge.IsControllerLeftHand(checkController))
            {
                return true;
            }

            if (hand == ControllerHand.Right && VRTK_SDK_Bridge.IsControllerRightHand(checkController))
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// The HeadsetTransform method is used to retrieve the transform for the VR Headset in the scene. It can be useful to determine the position of the user's head in the game world.
        /// </summary>
        /// <returns>The transform of the VR Headset component.</returns>
        public static Transform HeadsetTransform()
        {
            return VRTK_SDK_Bridge.GetHeadset();
        }

        /// <summary>
        /// The HeadsetCamera method is used to retrieve the transform for the VR Camera in the scene.
        /// </summary>
        /// <returns>The transform of the VR Camera component.</returns>
        public static Transform HeadsetCamera()
        {
            return VRTK_SDK_Bridge.GetHeadsetCamera();
        }

        /// <summary>
        /// The PlayAreaTransform method is used to retrieve the transform for the play area in the scene.
        /// </summary>
        /// <returns>The transform of the VR Play Area component.</returns>
        public static Transform PlayAreaTransform()
        {
            return VRTK_SDK_Bridge.GetPlayArea();
        }
    }
}