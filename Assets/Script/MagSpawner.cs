using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class MagSpawner : MonoBehaviour
{
    [SerializeField] XRBaseInteractable simpleInteractable;
    [SerializeField] XRInteractionManager interactionManager;
    private void Awake()
    {
        simpleInteractable.selectEntered.AddListener(OnActivate);
    }
    private void OnActivate(SelectEnterEventArgs args)
    {
        XRGrabInteractable objectToGrab = PoolManager.instance.Create(PoolEnum.Magazine, transform).GetComponent<XRGrabInteractable>();
        interactionManager.SelectEnter(args.interactorObject, objectToGrab);
    }
}
