using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Recipe : MonoBehaviour
{//user story cam
    [SerializeField] private Transform cam;
    [SerializeField] private Transform player;
    public Vector3 displacement = new Vector3(1, 14, -10);
    public Vector3 orientation = new Vector3(50, 0, 0);
    //user story recipe
    [SerializeField] private GameObject recipe;//pidentify by using unity interface
    private bool showRecipe = true;
    // Start is called before the first frame update
    void Start()
    {
        cam = GameObject.FindGameObjectWithTag("MainCamera").transform;
        player = GameObject.FindGameObjectWithTag("Player").transform;
        cam.position = player.position + displacement;
        //cam.rotation = Quaternion.Euler(orientation);// 
        cam.eulerAngles = orientation;
        recipe.SetActive(showRecipe);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.P))
        {
            showRecipe = !showRecipe;
            recipe.SetActive(showRecipe);
        }
    }
}
