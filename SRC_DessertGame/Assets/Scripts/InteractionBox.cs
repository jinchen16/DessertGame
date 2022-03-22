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
            ingredientController.SetPlayerCarry(_playerHolder);
            _playerActionController.SetOnInteractCallback(ingredientController.OnIngredientInteracted);
            Debug.Log(">>>Touching ingredient");
        }
        else if (other.CompareTag("Deliver"))
        {
            _playerActionController.SetOnInteractCallback(() =>
            {
                DeliverZone deliverZone = other.GetComponent<DeliverZone>();
                deliverZone.DeliverRecipe(100);
            });
        }
        else if (other.CompareTag("Mixer"))
        {
            Debug.Log(">>>Mixer");
        }
        else if (other.CompareTag("Oven"))
        {
            Debug.Log(">>>Oven");
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
