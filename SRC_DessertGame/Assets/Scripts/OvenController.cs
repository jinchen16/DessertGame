using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OvenController : MonoBehaviour
{
    private bool _isProcessing;
    private float _processingTime;
    private float _currentTime;
    private bool _isDone;

    private ActionLoading _actionLoading;

    [SerializeField]
    private Transform _target;

    [SerializeField]
    private float _processTimeDeclared;

    // Start is called before the first frame update
    void Start()
    {
        _processingTime = _processTimeDeclared;
        _isDone = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (_isProcessing)
        {
            _currentTime -= Time.deltaTime;

            if (_actionLoading != null)
            {
                float value = (_processingTime - _currentTime) / _processingTime;
                _actionLoading.UpdateBar(value);
            }

            if (_currentTime <= 0)
            {
                _isProcessing = false;
                _actionLoading.Hide();
                _actionLoading = null;
                _isDone = true;
            }
        }
    }

    public void OnOvenInteracted(Transform playerHolder = null, PlayerActionController playerActionController = null)
    {
        if (_isDone)
        {
            Transform cake = PoolHandler.instance.SpawnCake();
            cake.parent = playerHolder;
            cake.localPosition = Vector3.zero;
            cake.localRotation = Quaternion.identity;
            playerActionController.SetHasItemOnHands(true);
        }
        else
        {
            if (!_isProcessing && playerHolder.GetChild(0).GetComponent<BowlController>() != null)
            {
                Transform actionLoadingT = PoolHandler.instance.SpawnActionLoading();
                _actionLoading = actionLoadingT.GetComponent<ActionLoading>();
                _actionLoading.SetPosition(_target.position);

                _currentTime = _processingTime;
                _isProcessing = true;
                _isDone = false;
            }
        }
    }

    public void PlaceBowl()
    {
        if (!_isProcessing)
        {
            Transform actionLoadingT = PoolHandler.instance.SpawnActionLoading();
            _actionLoading = actionLoadingT.GetComponent<ActionLoading>();
            _actionLoading.SetPosition(_target.position);

            _currentTime = _processingTime;
            _isProcessing = true;
            _isDone = false;
        }
    }

    public void TakeCake(Transform playerHolder, PlayerActionController playerActionController)
    {
        Transform cake = PoolHandler.instance.SpawnCake();
        cake.parent = playerHolder;
        cake.localPosition = Vector3.zero;
        cake.localRotation = Quaternion.identity;
        playerActionController.SetHasItemOnHands(true);
    }

    public bool IsDone()
    {
        return _isDone;
    }
}
