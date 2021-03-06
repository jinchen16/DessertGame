using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionBox : MonoBehaviour
{
    [SerializeField]
    private PlayerActionController _playerActionController;

    [SerializeField]
    private Transform _playerHolder;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Ingredient"))
        {
            IngredientController ingredientController = other.GetComponent<IngredientController>();
            //ingredientController.SetPlayerCarry(_playerHolder);
            _playerActionController.SetOnInteractCallback(() =>
            {
                if (_playerActionController.HasItemOnHands)
                {
                    IngredientController tmp = _playerHolder.GetChild(0).GetComponent<IngredientController>();
                    if (tmp != null)
                    {
                        tmp.OnIngredientReleased();
                        _playerActionController.SetHasItemOnHands(false);
                    }
                }

                ingredientController.OnIngredientInteracted(_playerHolder, _playerActionController);
            });
        }
        else if (other.CompareTag("Deliver"))
        {
            // TODO::Check that the baked product is the only one to be served
            _playerActionController.SetOnInteractCallback(() =>
            {
                if (_playerActionController.HasItemOnHands)
                {
                    PoolHandler.instance.DespawnElement(_playerHolder.GetChild(0));
                    _playerActionController.SetHasItemOnHands(false);
                    DeliverZone deliverZone = other.GetComponent<DeliverZone>();
                    deliverZone.DeliverRecipe(100);
                }
            });
        }
        else if (other.CompareTag("Mixer"))
        {
            if (_playerHolder.childCount > 0)
            {
                _playerActionController.SetOnInteractCallback(() =>
                {
                    if (_playerActionController.HasItemOnHands)
                    {
                        MixerController mixer = other.GetComponent<MixerController>();
                        BowlController bowl = _playerHolder.GetChild(0).GetComponent<BowlController>();
                        if (bowl)
                        {
                            _playerHolder.GetChild(0).transform.parent = mixer.transform;
                            bowl.ResetPosition();
                            _playerActionController.SetHasItemOnHands(false);
                        }
                        else
                        {
                            if (_playerHolder.GetChild(0).GetComponent<IngredientController>().CurrentState == State.PROCESSED)
                            {
                                mixer.PlaceIngredient();
                                _playerActionController.SetHasItemOnHands(false);
                                PoolHandler.instance.DespawnElement(_playerHolder.GetChild(0).gameObject.transform);
                            }
                        }
                    }
                });
                //Debug.Log(">>>Mixer");
            }
            else
            {
                _playerActionController.SetOnInteractCallback(() =>
                {
                    MixerController mixer = other.GetComponent<MixerController>();

                    if (mixer.IsMixReady())
                    {
                        mixer.OnMixerInteracted(_playerHolder, _playerActionController);
                        //Debug.Log(">>>Pick up the things");
                    }
                });
            }
        }
        else if (other.CompareTag("Oven"))
        {
            _playerActionController.SetOnInteractCallback(() =>
            {
                OvenController oven = other.GetComponent<OvenController>();

                if (!oven.IsDone())
                {
                    if (_playerHolder.childCount > 0)
                    {
                        BowlController bowl = _playerHolder.GetChild(0).GetComponent<BowlController>();
                        if (bowl != null)
                        {
                            //bowl.transform.parent = null;
                            oven.PlaceBowl();
                        }
                    }
                }
                else
                {
                    if (_playerActionController.HasItemOnHands)
                    {
                        _playerHolder.GetChild(0).transform.parent = null;
                    }

                    oven.TakeCake(_playerHolder, _playerActionController);
                }
            });
        }
        else if (other.CompareTag("IngredientSpawner"))
        {
            _playerActionController.SetOnInteractCallback(() =>
            {
                if (_playerActionController.HasItemOnHands)
                {
                    IngredientController tmp = _playerHolder.GetChild(0).GetComponent<IngredientController>();
                    if (tmp != null)
                    {
                        tmp.OnIngredientReleased();
                        _playerActionController.SetHasItemOnHands(false);
                    }
                }
                other.GetComponent<IngredientSpawner>().SpawnIngredient(_playerHolder, _playerActionController);
            });
        }
        else if (other.CompareTag("CutBoard"))
        {
            _playerActionController.SetOnInteractCallback(() =>
            {
                if (_playerActionController.HasItemOnHands)
                {
                    IngredientController tmp = _playerHolder.GetChild(0).GetComponent<IngredientController>();
                    if (tmp != null && tmp.CurrentState == State.NORMAL)
                    {
                        tmp.OnIngredientReleased();
                        tmp.transform.parent = other.transform;
                        tmp.transform.localPosition = other.transform.Find("Positioner").localPosition;
                        tmp.transform.rotation = Quaternion.identity;
                        _playerActionController.SetHasItemOnHands(false);
                    }
                }
            });
        }

        if (other.CompareTag("Bowl"))
        {
            _playerActionController.SetOnInteractCallback(() =>
            {
                if (!_playerActionController.HasItemOnHands)
                {
                    _playerActionController.SetHasItemOnHands(true);

                    other.transform.parent = _playerHolder;
                    other.transform.localPosition = Vector3.zero;
                    other.transform.localRotation = Quaternion.identity;
                    other.GetComponent<BowlController>().SetColliderStatus(true);
                }
            });
        }
    }

    private void OnTriggerExit(Collider other)
    {
        _playerActionController.SetOnInteractCallback();
        if (other.CompareTag("Ingredient"))
        {
            IngredientController ingredientController = other.GetComponent<IngredientController>();
            ingredientController.OnIngredientCanceled();
        }
    }
}
