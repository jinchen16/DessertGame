using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeliverZone : MonoBehaviour
{
    public void DeliverRecipe(int amount)
    {
        ScoreManager.instance.IncreaseScore(amount);
    }
}
