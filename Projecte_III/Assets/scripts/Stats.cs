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
    }
    [SerializeField] private Data stats;

    public static Data operator +(Stats a, Stats b)
    {
        Data tmp;

        tmp.weight = a.stats.weight + b.stats.weight;
        tmp.torque = a.stats.torque + b.stats.torque;
        tmp.acceleration = a.stats.acceleration + b.stats.acceleration;
        tmp.maxVelocity = a.stats.maxVelocity + b.stats.maxVelocity;
        tmp.friction = a.stats.friction + b.stats.friction;

        return tmp;
    }

    public static Data operator -(Stats a, Stats b)
    {
        Data tmp;

        tmp.weight = a.stats.weight - b.stats.weight;
        tmp.torque = a.stats.torque - b.stats.torque;
        tmp.acceleration = a.stats.acceleration - b.stats.acceleration;
        tmp.maxVelocity = a.stats.maxVelocity - b.stats.maxVelocity;
        tmp.friction = a.stats.friction - b.stats.friction;

        return tmp;
    }

    public Data GetStats() { return stats; }

    public void SetStats(Data s) { stats = s; }
};
