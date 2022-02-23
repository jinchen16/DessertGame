using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionBox : MonoBehaviour
{
    [SerializeField]
    private PlayerActionController _playerActionController;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Ingredient"))
        {
            IngredientController ingredientController = other.GetComponent<IngredientController>();
            _playerActionController.SetOnInteractCallback(ingredientController.OnIngredientInteracted);
            Debug.Log(">>>Touching ingredient");
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
