using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Stats : MonoBehaviour
{
    [Serializable]
    private struct Data
    {
        public float weight;
        public float torque;
        public float acceleration;
        public float maxVelocity;
        public float friction;
    }
    [SerializeField] Data stats;

    public static Stats operator +(Stats a, Stats b)
    {
        Stats tmp = new Stats();

        tmp.stats.weight = a.stats.weight + b.stats.weight;
        tmp.stats.torque = a.stats.torque + b.stats.torque;
        tmp.stats.acceleration = a.stats.acceleration + b.stats.acceleration;
        tmp.stats.maxVelocity = a.stats.maxVelocity + b.stats.maxVelocity;
        tmp.stats.friction = a.stats.friction + b.stats.friction;

        return tmp;
    }

    public static Stats operator -(Stats a, Stats b)
    {
        Stats tmp = new Stats();

        tmp.stats.weight = a.stats.weight - b.stats.weight;
        tmp.stats.torque = a.stats.torque - b.stats.torque;
        tmp.stats.acceleration = a.stats.acceleration - b.stats.acceleration;
        tmp.stats.maxVelocity = a.stats.maxVelocity - b.stats.maxVelocity;
        tmp.stats.friction = a.stats.friction - b.stats.friction;

        return tmp;
    }
};
