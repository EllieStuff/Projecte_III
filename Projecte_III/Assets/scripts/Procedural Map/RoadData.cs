using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadData : MonoBehaviour
{
    public enum Type { STRAIGHT = 0, LEFT, RIGHT, COUNT };
    [SerializeField] Type roadType = Type.COUNT;

    [SerializeField] int baseSpawnRate = 60;

    [System.Serializable]
    public class SpawnRateSet
    {
        public float straight = 0, left = 0, right = 0;
        public SpawnRateSet() { }
        public SpawnRateSet(float _initValue) { straight = left = right = _initValue; }
        public SpawnRateSet(float _straight, float _left, float _right) { straight = _straight; left = _left; right = _right; }
    }
    public SpawnRateSet spawnRates = new SpawnRateSet();

    public Type RoadType { get { return roadType; } }
    public int SpawnRate { get { return baseSpawnRate; } }


    // Start is called before the first frame update
    void Start()
    {
        float maxSpawnRate = spawnRates.straight + spawnRates.left + spawnRates.right;
        if(maxSpawnRate != 100)
        {
            float spawnDiff = 100 / maxSpawnRate;
            spawnRates.straight *= spawnDiff;
            spawnRates.left *= spawnDiff;
            spawnRates.right *= spawnDiff;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public Type GetRoadType(float _percentage)
    {
        if (_percentage < 0 || _percentage > 100) Debug.LogError("Percentage was out of range: " + _percentage);

        if (_percentage < spawnRates.straight)
        {
            return Type.STRAIGHT;
        }
        else if (_percentage >= spawnRates.straight && _percentage < spawnRates.straight + spawnRates.left)
        {
            return Type.LEFT;
        }
        else
        {
            return Type.RIGHT;
        }

    }

}
