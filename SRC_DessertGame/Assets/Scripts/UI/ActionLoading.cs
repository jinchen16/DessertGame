using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ActionLoading : MonoBehaviour
{
    [SerializeField]
    private Image _barImage;

    public void SetPosition(Vector3 targetPos)
    {
        Vector2 screenPoint = Camera.main.WorldToScreenPoint(targetPos);
        RectTransform rt = GetComponent<RectTransform>();
        rt.position = screenPoint;
        UpdateBar(0);
    }

    public void UpdateBar(float value)
    {
        _barImage.fillAmount = value;
    }

    public void Hide()
    {
        PoolHandler.instance.DespawnElement(this.transform);
    }
}
