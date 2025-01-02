using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;

namespace SojaExiles
{
    public class opencloseDoor : MonoBehaviour
    {
        public Animator openandclose;
        public bool open;
        public Transform Player;
        public XRController leftController;
        public XRController rightController;

        private InputAction interactAction;

        void Start()
        {
            open = false;

            // Set up the input action for interacting (assuming "Activate" is mapped to the correct button in the input system)
            interactAction = new InputAction(type: InputActionType.Button, binding: "<XRController>{RightHand}/trigger");
            interactAction.Enable();
        }

        void Update()
        {
            if (Player)
            {
                float dist = Vector3.Distance(Player.position, transform.position);
                if (dist < 15)
                {
                    if (IsInteractionButtonPressed())
                    {
                        if (open == false)
                        {
                            StartCoroutine(opening());
                        }
                        else
                        {
                            StartCoroutine(closing());
                        }
                    }
                }
            }
        }

        private bool IsInteractionButtonPressed()
        {
            // Check if the interaction button is pressed
            return interactAction.WasPressedThisFrame();
        }

        IEnumerator opening()
        {
            print("you are opening the door");
            openandclose.Play("Opening");
            open = true;
            yield return new WaitForSeconds(.5f);
        }

        IEnumerator closing()
        {
            print("you are closing the door");
            openandclose.Play("Closing");
            open = false;
            yield return new WaitForSeconds(.5f);
        }
    }
}
