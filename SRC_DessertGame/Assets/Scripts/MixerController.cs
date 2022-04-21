using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MixerController : MonoBehaviour
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

    [SerializeField]
    private int _ingredientsQuantity;

    [SerializeField]
    private int _maxIngredients;

    [SerializeField]
    private GameObject _bowlContainer;    

    // Start is called before the first frame update
    void Start()
    {
        _processingTime = _processTimeDeclared;
        _isDone = false;

        _bowlContainer.GetComponent<BowlController>().SetColliderStatus(false);
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

    public void PlaceIngredient()
    {
        //added line to play sfx
        if (AudioManager.getInsta() != null)
        {
            AudioManager.getInsta().Play(AudioManager.SoundName.mixing);
        }
        if (_ingredientsQuantity >= _maxIngredients)
        {
            return;
        }

        if (_actionLoading == null)
        {
            Transform actionLoadingT = PoolHandler.instance.SpawnActionLoading();
            _actionLoading = actionLoadingT.GetComponent<ActionLoading>();
            _actionLoading.SetPosition(_target.position);
        }
        else
        {
            _actionLoading.UpdateBar(0);
        }

        _currentTime = _processingTime;
        _isProcessing = true;
        _ingredientsQuantity++;
        _isDone = false;
    }

    public bool IsMixReady()
    {
        return _isDone;
    }

    public void OnMixerInteracted(Transform playerHolder = null, PlayerActionController playerActionController = null)
    {
        if (playerActionController != null)
        {
            playerActionController.SetHasItemOnHands(true);
        }
        _bowlContainer.transform.parent = playerHolder;
        _bowlContainer.transform.localPosition = Vector3.zero;
        _bowlContainer.transform.localRotation = Quaternion.identity;
        _bowlContainer.GetComponent<BowlController>().SetColliderStatus(true);
    }

    public void ResetMixer()
    {
        _isDone = false;
        _ingredientsQuantity = 0;
    }
}
