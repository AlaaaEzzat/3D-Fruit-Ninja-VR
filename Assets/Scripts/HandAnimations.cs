using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class HandAnimations : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private InputActionProperty inputGrip;
    [SerializeField] private InputActionProperty inputTrigger;

    private void Start()
    {
        animator = GetComponent<Animator>();

        inputGrip.action.performed += PlayerIsGripping;
        inputGrip.action.canceled += PlayerIsGripping;

        inputTrigger.action.performed += PlayerIsDelecting;
        inputTrigger.action.canceled += PlayerIsDelecting;
    }

    public void PlayerIsGripping(InputAction.CallbackContext inputActionCallback)
    {
        float inputValue = inputGrip.action.ReadValue<float>();
        animator.SetFloat("Grip", inputValue);
    }

    public void PlayerIsDelecting(InputAction.CallbackContext inputActionCallback)
    {
        float inputValue = inputTrigger.action.ReadValue<float>();
        animator.SetFloat("Trigger", inputValue);
    }

}
