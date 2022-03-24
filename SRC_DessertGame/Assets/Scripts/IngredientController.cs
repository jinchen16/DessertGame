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

    private bool _isChopping;
    private float _choppingTime;
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
    private float _chopTime;


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
        _choppingTime = _chopTime;
    }

    // Update is called once per frame
    void Update()
    {
        if (_isChopping)
        {
            _currentTime -= Time.deltaTime;

            if (_actionLoading != null)
            {
                float value = (_choppingTime - _currentTime) / _choppingTime;
                _actionLoading.UpdateBar(value);
            }

            if (_currentTime <= 0)
            {
                _isChopping = false;
                _actionLoading.Hide();
                SetCurrentState(State.PROCESSED);
                //added line to play sfx
                AudioManager.getInsta().Stop(AudioManager.SoundName.chopping);
            }
        }
    }

    public void OnIngredientInteracted()
    {
        if (_currentState == State.NORMAL)
        {
            Transform actionLoadingT = PoolHandler.instance.SpawnActionLoading();
            _actionLoading = actionLoadingT.GetComponent<ActionLoading>();
            _actionLoading.SetPosition(_target.position);

            _currentTime = _choppingTime;
            _isChopping = true;
            SetCurrentState(State.PROCESSING);
            //added line to play sfx
            AudioManager.getInsta().Play(AudioManager.SoundName.chopping);
        }
        else if (_currentState == State.PROCESSING)
        {
            _isChopping = true;
        }
        else if (_currentState == State.PROCESSED)
        {
            if (_playerHolder != null)
            {
                _rigidBody.isKinematic = true;
                _collider.isTrigger = true;
                transform.parent = _playerHolder;
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
        _isChopping = false;
        //added line to play sfx
        AudioManager.getInsta().Stop(AudioManager.SoundName.chopping);
    }

    public void OnIngredientReleased()
    {
        _rigidBody.isKinematic = false;
        _collider.isTrigger = false;
        transform.parent = null;
    }
}
