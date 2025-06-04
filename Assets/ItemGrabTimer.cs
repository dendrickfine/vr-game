using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class ItemGrabTimer : MonoBehaviour
{
    public int itemIndex;
    public float holdDuration = 1f;

    private XRGrabInteractable grabInteractable;
    private float timer = 0f;
    private bool isHeld = false;
    private bool alreadyMarked = false;
    private QuestTracker questTracker;

    private void Awake()
    {
        grabInteractable = GetComponent<XRGrabInteractable>();
        questTracker = FindObjectOfType<QuestTracker>();
    }

    private void OnEnable()
    {
        grabInteractable.selectEntered.AddListener(OnGrabStart);
        grabInteractable.selectExited.AddListener(OnGrabEnd);
    }

    private void OnDisable()
    {
        grabInteractable.selectEntered.RemoveListener(OnGrabStart);
        grabInteractable.selectExited.RemoveListener(OnGrabEnd);
    }

    private void OnGrabStart(SelectEnterEventArgs args)
    {
        isHeld = true;
        timer = 0f;
    }

    private void OnGrabEnd(SelectExitEventArgs args)
    {
        isHeld = false;
        timer = 0f;
    }

    private void Update()
    {
        if (isHeld && !alreadyMarked)
        {
            timer += Time.deltaTime;
            if (timer >= holdDuration)
            {
                questTracker?.MarkItemFound(itemIndex);
                alreadyMarked = true;
            }
        }
    }
}
