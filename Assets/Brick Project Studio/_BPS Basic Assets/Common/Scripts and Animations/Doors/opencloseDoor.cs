using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;

namespace SojaExiles
{
    public class OpenCloseDoor : MonoBehaviour
    {
        public Animator doorAnimator;                 // Animator for door animations
        public InputActionReference triggerAction;   // Reference to trigger button input
        public float interactionDistance = 15f;      // Maximum distance to allow interaction

        private bool isHovered = false;              // Is the door being hovered?
        private bool isOpen = false;                 // Is the door open?

        void Start()
        {
            // Enable the trigger action
            if (triggerAction != null)
            {
                triggerAction.action.Enable();
            }
        }

        void Update()
        {
            CheckHover();

            // Check trigger input and only interact with the hovered door
            if (isHovered && IsTriggerPressed())
            {
                if (!isOpen)
                {
                    StartCoroutine(OpenDoor());
                }
                else
                {
                    StartCoroutine(CloseDoor());
                }
            }
        }

        private void CheckHover()
        {
            // Perform a raycast to detect if the player is looking at this door
            Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);
            Debug.DrawRay(ray.origin, ray.direction * interactionDistance, Color.green); // Debug ray for visualization

            if (Physics.Raycast(ray, out RaycastHit hit, interactionDistance))
            {
                if (hit.collider != null && hit.collider.gameObject == gameObject)
                {
                    if (!isHovered)
                    {
                        isHovered = true;
                        Debug.Log($"Hover detected on: {gameObject.name}");
                    }
                    return;
                }
            }

            if (isHovered)
            {
                isHovered = false;
                Debug.Log($"Hover exited from: {gameObject.name}");
            }
        }

        private bool IsTriggerPressed()
        {
            // Check if the trigger button is pressed
            return triggerAction != null && triggerAction.action.triggered;
        }

        IEnumerator OpenDoor()
        {
            Debug.Log($"Opening {gameObject.name}...");
            doorAnimator.Play("Opening");
            isOpen = true;
            yield return new WaitForSeconds(0.5f); // Wait for animation
        }

        IEnumerator CloseDoor()
        {
            Debug.Log($"Closing {gameObject.name}...");
            doorAnimator.Play("Closing");
            isOpen = false;
            yield return new WaitForSeconds(0.5f); // Wait for animation
        }
    }
}
