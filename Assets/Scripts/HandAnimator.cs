using UnityEngine;
using UnityEngine.InputSystem;

public class HandAnimator
{
    InputActionProperty gripAnimationAction;
    InputActionProperty pinchAnimationAction;
    Animator handAnimator;
    public HandAnimator(InputActionProperty gripAnimationAction, InputActionProperty pinchAnimationAction, Animator handAnimator)
    {
        this.pinchAnimationAction = pinchAnimationAction;
        this.gripAnimationAction = gripAnimationAction;
        this.handAnimator = handAnimator;
    }

    public void Animate()
    {
        float triggerValue = pinchAnimationAction.action.ReadValue<float>();
        float gripValue = gripAnimationAction.action.ReadValue<float>();
        handAnimator.SetFloat("Trigger", triggerValue);
        handAnimator.SetFloat("Grip", gripValue);
    }
}