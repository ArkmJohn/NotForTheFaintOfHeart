using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using Valve.VR.InteractionSystem;

public class PlayerHelp : MonoBehaviour
{
    Coroutine textHintCoroutine;

    void Start()
    {
        StartCoroutine(RightTextHelp(Player.instance.rightHand));
        StartCoroutine(LeftTextHelp(Player.instance.leftHand));
    }

    public void ShowControls(Hand hand)
    {
        if (textHintCoroutine != null)
        {
            StopCoroutine(textHintCoroutine);

        }

        
    }

    public void DisableHelp()
    {
        if (textHintCoroutine != null)
        {
            StopCoroutine(textHintCoroutine);
            textHintCoroutine = null;
        }

        foreach (Hand hand in Player.instance.hands)
        {
            ControllerButtonHints.HideAllButtonHints(hand);
            ControllerButtonHints.HideAllTextHints(hand);
        }
    }

    private IEnumerator RightTextHelp(Hand hand)
    {
        ControllerButtonHints.HideAllTextHints(hand);

        while (true)
        {
            ControllerButtonHints.ShowTextHint(hand, EVRButtonId.k_EButton_SteamVR_Trigger, "Interact with Objects");
            yield return new WaitForSeconds(5.0f);
            ControllerButtonHints.ShowTextHint(hand, EVRButtonId.k_EButton_Axis0, "Open/Close Flashlight");
            yield return new WaitForSeconds(5.0f);

            ControllerButtonHints.HideAllTextHints(hand);
            yield return new WaitForSeconds(5.0f);
        }
    }

    private IEnumerator LeftTextHelp(Hand hand)
    {
        ControllerButtonHints.HideAllTextHints(hand);

        while (true)
        {
            ControllerButtonHints.ShowTextHint(hand, EVRButtonId.k_EButton_SteamVR_Trigger, "Interact with Objects");
            yield return new WaitForSeconds(5.0f);
            ControllerButtonHints.ShowTextHint(hand, EVRButtonId.k_EButton_SteamVR_Touchpad, "Touch to move Around");
            yield return new WaitForSeconds(5.0f);

            ControllerButtonHints.HideAllTextHints(hand);
            yield return new WaitForSeconds(5.0f);
        }
    }
}

