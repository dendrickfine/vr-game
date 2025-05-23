using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

[RequireComponent(typeof(XRGrabInteractable))]
public class QuestItemPickup : MonoBehaviour
{
    private QuestObject questObject;
    private XRGrabInteractable grab;

    void Start()
    {
        grab = GetComponent<XRGrabInteractable>();
        questObject = GetComponent<QuestObject>();

        grab.selectEntered.AddListener(OnGrab);
    }

    void OnGrab(SelectEnterEventArgs args)
    {
        if (questObject != null)
        {
            QuestManager.instance.CompleteQuest(questObject.questID);
        }
    }
}
