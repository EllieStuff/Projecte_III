using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnTileWithCollider : MonoBehaviour
{
    private BoxCollider _coll;
    [SerializeField] private Transform prefab;

    // Start is called before the first frame update
    void Start()
    {
        _coll = GetComponent<BoxCollider>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Collider");
        if(other.gameObject.tag.Contains("Main"))
        {
            prefab.parent.GetComponent<GenerateNewTile>().CalculateNewTile();
            _coll.enabled = false;
        }
    }
}
