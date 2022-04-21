using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EZ_Pooling;

public class IngredientSpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject _ingredientPrefab;

    [SerializeField]
    private State _startState;

    public void SpawnIngredient(Transform playerHolder, PlayerActionController playerActionController)
    {
        Transform element = EZ_PoolManager.Spawn(_ingredientPrefab.transform, Vector3.zero, Quaternion.identity);
        IngredientController ingredientController = element.GetComponent<IngredientController>();
        ingredientController.SetCurrentState(_startState);
        ingredientController.CarryIngredient(playerHolder, playerActionController);
    }
}
