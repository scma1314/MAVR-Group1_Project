using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;

public class teleportation_manager : MonoBehaviour
{
    [SerializeField] private InputActionAsset actionAsset;
    [SerializeField] private XRRayInteractor rightrayInteractor;
    [SerializeField] private XRRayInteractor leftrayInteractor;
    [SerializeField] private TeleportationProvider provider;
    [SerializeField] private string leftActionname = "Teleport Select";
    [SerializeField] private string rightActionname = "Teleport Select";



    // Start is called before the first frame update
    void Start()
    {
        rightrayInteractor.enabled = false;
        leftrayInteractor.enabled = false;

        var activate = actionAsset.FindActionMap("XRI LeftHand Locomotion").FindAction(leftActionname);
        activate.Enable();
        activate.performed += OnTeleportActivate;
        activate.canceled += OnTelportCancel;

        var activate2 = actionAsset.FindActionMap("XRI RightHand Locomotion").FindAction(rightActionname);
        activate2.Enable();
        activate2.performed += OnUIActivate;
        activate2.canceled += OnUICancel;
    }

    private void OnTeleportActivate(InputAction.CallbackContext context)
    {

        leftrayInteractor.enabled = true;

    }

    private void OnTelportCancel(InputAction.CallbackContext context)
    {
        leftrayInteractor.enabled = false;
    }

    private void OnUIActivate(InputAction.CallbackContext context)
    {

        rightrayInteractor.enabled = true;

    }

    private void OnUICancel(InputAction.CallbackContext context)
    {
        rightrayInteractor.enabled = false;
    }
}
