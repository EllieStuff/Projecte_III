using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapWarningObstacle : MonoBehaviour
{
    [SerializeField] internal GameObject warningPrefab;
    private Transform UIParent;

    private void Start()
    {
        UIParent = transform;
    }

    public void InstantiateWarning(Vector3 UIPosition, float time) 
    {
        GameObject prefabSpawned = Instantiate(warningPrefab, UIParent);
        prefabSpawned.GetComponent<MapWarningInstance>().time = time;
        prefabSpawned.transform.GetComponent<RectTransform>().localPosition = UIPosition;
    }
}
