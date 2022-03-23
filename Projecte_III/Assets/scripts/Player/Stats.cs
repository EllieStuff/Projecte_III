using UnityEngine;
using System;

public class Stats : MonoBehaviour
{
    [Serializable]
    public struct Data
    {
        public float weight;
        public float torque;
        public float acceleration;
        public float maxVelocity;
        public float friction;

        public Data(float value)
        {
            weight = value;
            torque = value;
            acceleration = value;
            maxVelocity = value;
            friction = value;
        }

        public static Data operator +(Data a, Data b)
        {
            Data tmp;

            tmp.weight = a.weight + b.weight;
            tmp.torque = a.torque + b.torque;
            tmp.acceleration = a.acceleration + b.acceleration;
            tmp.maxVelocity = a.maxVelocity + b.maxVelocity;
            tmp.friction = a.friction + b.friction;

            return tmp;
        }

        public static Data operator -(Data a, Data b)
        {
            Data tmp;

            tmp.weight = a.weight - b.weight;
            tmp.torque = a.torque - b.torque;
            tmp.acceleration = a.acceleration - b.acceleration;
            tmp.maxVelocity = a.maxVelocity - b.maxVelocity;
            tmp.friction = a.friction - b.friction;

            return tmp;
        }

        public static bool operator==(Data a, Data b)
        {
            return (a.weight == b.weight) && (a.torque == b.torque) && (a.acceleration == b.acceleration) && (a.maxVelocity == b.maxVelocity) && (a.friction == b.friction);
        }

        public static bool operator !=(Data a, Data b)
        {
            return !(a == b);
        }
    }
    [SerializeField] private Data stats;

    public static Data operator +(Stats a, Stats b)
    {
        return a.stats + b.stats;
    }

    public static Data operator -(Stats a, Stats b)
    {
        return a.stats - b.stats;
    }

    public static bool operator ==(Stats a, Stats b)
    {
        return a.stats == b.stats;
    }

    public static bool operator !=(Stats a, Stats b)
    {
        return !(a.stats == b.stats);
    }

    public Data GetStats() { return stats; }

    public void SetStats(Data s) { stats = s; }

    public void ResetStats()
    {
        stats.weight = 0;
        stats.torque = 0;
        stats.acceleration = 0;
        stats.maxVelocity = 0;
        stats.friction = 0;
    }
};
