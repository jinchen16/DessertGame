using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class PlayerActionController : MonoBehaviour
{
    private UnityAction _onInteractCallback;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void OnInteract(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            _onInteractCallback?.Invoke();
        }        
    }

    public void SetOnInteractCallback(UnityAction onInteractCallback = null) => _onInteractCallback = onInteractCallback;
}
