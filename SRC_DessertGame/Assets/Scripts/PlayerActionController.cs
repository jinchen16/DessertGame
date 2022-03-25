using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class PlayerActionController : MonoBehaviour
{
    private UnityAction _onInteractCallback;

    public bool HasItemOnHands { get; private set; }    

    public void OnInteract(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            _onInteractCallback?.Invoke();
        }        
    }

    public void SetHasItemOnHands(bool hasItemOnHands) => HasItemOnHands = hasItemOnHands;

    public void SetOnInteractCallback(UnityAction onInteractCallback = null) => _onInteractCallback = onInteractCallback;
}
