using UnityEngine;

namespace Microsoft.Azure.SpatialAnchors.Unity.Examples
{
    public abstract class InputInteractionBase : MonoBehaviour
    {
        /// Destroying the attached Behaviour will result in the game or Scene receiving OnDestroy.
        /// OnDestroy will only be called on game objects that have previously been active.
        public virtual void OnDestroy()
        {
            UnityEngine.XR.WSA.Input.InteractionManager.InteractionSourcePressed -= InteractionManager_InteractionSourcePressed;
        }

        public virtual void Start()
        {
            Debug.Log("starting InputInteractionBase");
            UnityEngine.XR.WSA.Input.InteractionManager.InteractionSourcePressed += InteractionManager_InteractionSourcePressed;
        }

        /// Handles the HoloLens interaction event.
        private void InteractionManager_InteractionSourcePressed(UnityEngine.XR.WSA.Input.InteractionSourcePressedEventArgs obj)
        {
            if (obj.pressType == UnityEngine.XR.WSA.Input.InteractionSourcePressType.Select)
            {
                OnSelectInteraction();
            }
        }

        /// Called when a select interaction occurs. // this is what i want
        /// <remarks>Currently only called for HoloLens.</remarks>
        protected virtual void OnSelectInteraction()
        {
            RaycastHit hit;
            if (TryGazeHitTest(out hit))
            {
                OnSelectObjectInteraction(hit.point, hit);
            }
        }

        protected virtual void OnSelectObjectInteraction(Vector3 hitPoint, object target) { /* To be overridden */ }

        private bool TryGazeHitTest(out RaycastHit target)
        {
            Transform mainCameraT = Camera.main.transform;
            return Physics.Raycast(mainCameraT.position, mainCameraT.forward, out target);
        }
    }
}
