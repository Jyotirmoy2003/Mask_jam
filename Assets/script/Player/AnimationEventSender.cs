using UnityEngine;
using StarterAssets;

public class AnimationEventSender : MonoBehaviour
{
    private ThirdPersonController thirdPersonController;

    private void Awake()
    {
        thirdPersonController = GetComponentInParent<ThirdPersonController>();

        if (thirdPersonController == null)
        {
            Debug.LogError("AnimationEventSender: ThirdPersonController not found!");
        }
    }

    // Called from animation events
    public void OnFootstep(AnimationEvent animationEvent)
    {
        if (thirdPersonController == null)
            return;

        thirdPersonController.OnFootstep(animationEvent);
    }

    // Called from animation events
    public void OnLand(AnimationEvent animationEvent)
    {
        if (thirdPersonController == null)
            return;

        thirdPersonController.OnLand(animationEvent);
    }
}
