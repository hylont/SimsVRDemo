using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;

public class Hand : MonoBehaviour, IGameListener
{
    public const float OFFSET = 0.10f;


    [Header("Controller related")]
    public InputActionProperty actionButton;
    public Transform playerTransform;
    //public Hand otherHand;
    public EHand controllerHand;
    public EHand dominantHand;
    private CellContent inHandContent;
    private ActionBasedController xr;

    [Header("Animation related")]
    public InputActionProperty activateAction;
    public InputActionProperty selectAction;
    public Animator handAnimator;
    private HandAnimator animator;

    private float previousActivateValue;
    private float previousSelectValue;
    private Vector3 initialPosition;

    [Header("Tablet related")]
    public Renderer handRenderer;
    public Transform contentHintPosition;
    private GameObject createdHint;

    private GameObject gameManager;

    // Start is called before the first frame update
    void Start()
    {
        previousActivateValue = 0;
        previousSelectValue = 0;
        animator = new HandAnimator(selectAction, activateAction, handAnimator);
        xr = GetComponent<ActionBasedController>();
        gameManager = GameObject.FindGameObjectWithTag("GAME");
        gameManager.GetComponent<Game>().AddListener(this);
    }

    // Update is called once per frame
    void Update()
    {
        AnimationUpdate();
        LocomotionUpdate();
        TabletUpdate();
    }


    private void LocomotionUpdate()
    {
        if (controllerHand == dominantHand)
        {
            float actionValue = actionButton.action.ReadValue<float>();
            //Debug.Log("actionvalue : " + actionValue+".\nPreviousValue : "+previousActivateValue);
            if (previousActivateValue <= OFFSET)
            {
                if (actionValue >= OFFSET) OnActivate();
            }
            else if (actionValue <= OFFSET)
            {
                OnActivateRelease();
            }
            else
            {
                OnActivateMaintain();
            }
            previousActivateValue = actionValue;
        }
    }

    private void OnActivate()
    {
        if (dominantHand == controllerHand)
        {
            initialPosition = transform.localToWorldMatrix.GetPosition();
        }
    }

    private void OnActivateMaintain()
    {
        if (dominantHand == controllerHand)
        {
            Vector3 moveVector = initialPosition - transform.position;
            playerTransform.position = new Vector3(playerTransform.position.x + moveVector.x, playerTransform.position.y, playerTransform.position.z + moveVector.z);
        }
    }

    private void OnActivateRelease()
    {
        //Debug.Log("Release");
    }

    private void AnimationUpdate()
    {
        animator.Animate();
    }

    private void TabletUpdate()
    {
        RotateHint();
        if (controllerHand == dominantHand)
        {
            float actionValue = selectAction.action.ReadValue<float>();
            if (previousSelectValue <= OFFSET)
            {
                if (actionValue >= OFFSET) OnSelect();
            }
            else if (actionValue <= OFFSET)
            {
                OnSelectRelease();
            }
            else
            {
                OnSelectMaintain();
            }
            previousSelectValue = actionValue;
        }
    }
    private void RotateHint()
    {
        if (createdHint) createdHint.transform.Rotate(40 * Time.deltaTime * Vector3.right, Space.Self);
    }

    private void OnSelect()
    {
        //ToggleHandVisibility(false);
    }

    private void OnSelectMaintain()
    {

    }

    private void OnSelectRelease()
    {
        ToggleHandVisibility(true);
    }

    public void SetInHandContent(CellContent newCellContent)
    {
        const float SCALE = 0.35f;
        Vibrate();
        Destroy(createdHint);
        inHandContent = newCellContent;
        createdHint = Instantiate(inHandContent.prefab, contentHintPosition);
        Collider contentCollider = createdHint.GetComponent<Collider>();
        if (contentCollider) contentCollider.enabled = false;
        Vector3 baseScale = createdHint.transform.localScale;
        createdHint.transform.localScale = new Vector3(baseScale.x * SCALE, baseScale.y * SCALE, baseScale.z * SCALE);
        //toggleHandVisibility(false);
    }

    private void ToggleHandVisibility(bool visibility)
    {
        handRenderer.enabled = visibility;
        if (createdHint)
        {
            //createdHint.GetComponentInChildren<Renderer>().enabled = visibility;
            GameObject.FindGameObjectWithTag(dominantHand + " PHAND").GetComponentInChildren<SkinnedMeshRenderer>().enabled = visibility;
        }
    }

    public void EmptyHandContent()
    {
        Vibrate();
        inHandContent = null;
        Destroy(createdHint);
        //toggleHandVisibility(true);
    }

    public CellContent GetInHandContent()
    {
        return inHandContent;
    }

    public void Vibrate()
    {
        xr.SendHapticImpulse(0.7f, 0.3f);
    }

    public void OnDominantHandChange(EHand newDominantHand)
    {
        dominantHand = newDominantHand;
    }
}
