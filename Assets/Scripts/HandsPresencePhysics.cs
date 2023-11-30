using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class HandsPresencePhysics : MonoBehaviour
{
    [Header("Physics related")]
    public Transform target;
    public Renderer nonPhysicalHands;
    public float showNonPhysicalHandsOffset = 0.05f;

    [Header("Animation related")]
    public InputActionProperty pinchAnimationAction;
    public InputActionProperty gripAnimationAction;
    public Animator handAnimator;
    private HandAnimator animator;

    private Rigidbody rb;
    private Collider[] handColliders;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        handColliders = GetComponentsInChildren<Collider>();
        animator = new HandAnimator(gripAnimationAction, pinchAnimationAction, handAnimator);
    }

    public void EnableHandColliderDelay(float delay)
    {
        Invoke(nameof(EnableHandCollider), delay);
    }

    public void EnableHandCollider()
    {
        foreach (var item in handColliders)
        {
            item.enabled = true;
        }
    }

    public void DisableHandCollider()
    {
        foreach (var item in handColliders)
        {
            item.enabled = false;
        }
    }

    private void Update()
    {
        animator.Animate();
        float distance = Vector3.Distance(transform.position, target.position);

        if (distance > showNonPhysicalHandsOffset)
        {
            nonPhysicalHands.enabled = true;
        }
        else
        {
            nonPhysicalHands.enabled = false;
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        rb.velocity = (target.position - transform.position) / Time.fixedDeltaTime;

        //Quaternion rotationDelta = target.rotation * Quaternion.Inverse(transform.rotation);
        //rotationDelta.ToAngleAxis(out float angleDegree, out Vector3 rotationAxis);
        //Vector3 rotationDeltaDegree = angleDegree * rotationAxis;

        //rb.angularVelocity = (rotationDeltaDegree * Mathf.Deg2Rad / Time.fixedDeltaTime);
        transform.rotation = target.rotation;
    }
}
