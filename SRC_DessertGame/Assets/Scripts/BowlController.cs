using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BowlController : MonoBehaviour
{
    [SerializeField]
    private BoxCollider _collider;

    [SerializeField]
    private Vector3 _startPos;

    private void Start()
    {
        _startPos = transform.localPosition;
    }

    public void SetColliderStatus(bool status)
    {
        _collider.enabled = status;
    }

    public void ResetPosition()
    {
        transform.localPosition = _startPos;
    }
}
