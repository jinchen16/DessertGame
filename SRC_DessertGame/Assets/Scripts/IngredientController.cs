using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum State
{
    NORMAL,
    PROCESSING,
    PROCESSED
}

public class IngredientController : MonoBehaviour
{
    [SerializeField]
    private State _currentState;
    private State _previousState;

    private bool _isProcessing;
    private float _processingTime;
    private float _currentTime;

    private ActionLoading _actionLoading;

    [SerializeField]
    private Transform _target;

    [SerializeField]
    private GameObject _rawContainer;

    [SerializeField]
    private GameObject _processedContainer;

    private Transform _playerHolder;

    [SerializeField]
    private Rigidbody _rigidBody;
    [SerializeField]
    private Collider _collider;

    [SerializeField]
    private float _processTimeDeclared;


    public void SetCurrentState(State state)
    {
        _previousState = _currentState;
        _currentState = state;

        Debug.Log(">>>State " + state);
        switch (state)
        {
            case State.NORMAL:
                _rawContainer.SetActive(true);
                _processedContainer.SetActive(false);
                _rigidBody.isKinematic = false;
                _collider.isTrigger = false;
                break;
            case State.PROCESSING:
                break;
            case State.PROCESSED:
                _rawContainer.SetActive(false);
                _processedContainer.SetActive(true);
                break;
            default:
                break;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        _processingTime = _processTimeDeclared;
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
                SetCurrentState(State.PROCESSED);
                //added line to play sfx
                if (AudioManager.getInsta() != null)
                {
                    AudioManager.getInsta().Stop(AudioManager.SoundName.chopping);
                }
            }
        }
    }

    public void OnIngredientInteracted(Transform playerHolder = null, PlayerActionController playerActionController = null)
    {
        if (_currentState == State.NORMAL)
        {
            Transform actionLoadingT = PoolHandler.instance.SpawnActionLoading();
            _actionLoading = actionLoadingT.GetComponent<ActionLoading>();
            _actionLoading.SetPosition(_target.position);

            _currentTime = _processingTime;
            _isProcessing = true;
            SetCurrentState(State.PROCESSING);
            //added line to play sfx
            if (AudioManager.getInsta() != null)
            {
                AudioManager.getInsta().Play(AudioManager.SoundName.chopping);
            }
        }
        else if (_currentState == State.PROCESSING)
        {
            _isProcessing = true;
        }
        else if (_currentState == State.PROCESSED)
        {
            if (playerHolder != null)
            {
                if (playerActionController != null)
                {
                    playerActionController.SetHasItemOnHands(true);
                }
                _rigidBody.isKinematic = true;
                _collider.isTrigger = true;
                transform.parent = playerHolder;
                transform.localPosition = Vector3.zero;
                transform.localRotation = Quaternion.identity;
            }
        }
    }

    public void SetPlayerCarry(Transform parent)
    {
        _playerHolder = parent;
    }

    public void OnIngredientCanceled()
    {
        //added line to play sfx
        if (AudioManager.getInsta() != null)
        {
            AudioManager.getInsta().Stop(AudioManager.SoundName.chopping);
        }
        _isProcessing = false;
    }

    public void OnIngredientReleased()
    {
        _rigidBody.isKinematic = false;
        _collider.isTrigger = false;
        transform.parent = null;
    }
}
