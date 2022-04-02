using UnityEngine;
using UnityEditor;

public class GenerateNewTile : MonoBehaviour
{
    [SerializeField] GameObject[] tiles;

    [SerializeField] Transform lastTile = null;

    // Start is called before the first frame update
    void Start()
    {
        tiles = Resources.LoadAll<GameObject>("Prefabs/ProceduralMap");

       
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CalculateNewTile()
    {
        float random = Random.Range(0, 100);

        GameObject newObject = null;
        if (random < 30)
        { //Straight
            newObject = Instantiate(tiles[0], transform);
        }
        else if( 30 < random && random < 60)
        { //Turn right
            newObject = Instantiate(tiles[1], transform);
        }
        else
        { //Turn left
            newObject = Instantiate(tiles[2], transform);
        }

        if (newObject == null) Debug.LogError("Upsie");

        Transform child = lastTile.transform.GetChild(0).Find("NewSpawn");

        newObject.transform.position = child.position;

        Vector3 _scale = new Vector3(0.5f, 0.5f, 0.5f);
        newObject.transform.localScale = _scale;
        newObject.transform.rotation = Quaternion.RotateTowards(newObject.transform.rotation, child.rotation, 360);

        if(transform.childCount > 2)
            Destroy(transform.GetChild(0).gameObject);

        lastTile = newObject.transform;
    }
}
