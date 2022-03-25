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
            //Debug.Log(">>>Touching ingredient");
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
                        mixer.PlaceIngredient();
                        _playerActionController.SetHasItemOnHands(false);
                        Destroy(_playerHolder.GetChild(0).gameObject);
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
                            bowl.transform.parent = null;
                            oven.PlaceBowl();
                        }
                    }
                }
                else
                {
                    oven.TakeCake(_playerHolder, _playerActionController);
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
