using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EZ_Pooling;

public class PoolHandler : Singleton<PoolHandler>
{
    [SerializeField]
    private Transform _poolContainer;
    
    [SerializeField]
    private GameObject _actionLoadingPrefab;

    [SerializeField]
    private GameObject _cakePrefab;

    public Transform SpawnActionLoading()
    {
        Transform element = EZ_PoolManager.Spawn(_actionLoadingPrefab.transform, Vector3.zero, Quaternion.identity);
        element.parent = _poolContainer;
        element.localScale = Vector3.one;
        return element;
    }

    public Transform SpawnCake()
    {
        Transform element = EZ_PoolManager.Spawn(_cakePrefab.transform, Vector3.zero, Quaternion.identity);
        element.parent = _poolContainer;        
        return element;
    }

    public void DespawnElement(Transform target)
    {
        EZ_PoolManager.Despawn(target);
    }
}
