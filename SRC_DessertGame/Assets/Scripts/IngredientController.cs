using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum State
{
    NORMAL,
    CHOPPING,
    CHOPPED
}

public class IngredientController : MonoBehaviour
{
    private State _currentState;
    private State _previousState;

    private bool _isChopping;
    private float _choppingTime;
    private float _currentTime;

    private ActionLoading _actionLoading;

    [SerializeField]
    private Transform _target;

    public void SetCurrentState(State state)
    {
        _previousState = _currentState;
        _currentState = state;

        Debug.Log(">>>State " + state);
    }

    // Start is called before the first frame update
    void Start()
    {
        _choppingTime = 3f;
    }

    // Update is called once per frame
    void Update()
    {
        if (_isChopping)
        {
            _currentTime -= Time.deltaTime;            

            if(_actionLoading != null )
            {
                float value = (_choppingTime - _currentTime) / _choppingTime;
                _actionLoading.UpdateBar(value);
            }

            if (_currentTime <= 0)
            {
                _isChopping = false;
                _actionLoading.Hide();
                SetCurrentState(State.CHOPPED);
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
            SetCurrentState(State.CHOPPING);
        }
        else if (_currentState == State.CHOPPING)
        {
            _isChopping = true;
        }
    }

    public void OnIngredientCanceled()
    {
        _isChopping = false;
    }
}
