using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum HazardType
{
    Car,
    Dinosaur,
    Ghost,
    Hatchet,
    Jet,
    Shotgun,
    Snake,
    Squid,
    Train,
    Witch
}

public class Hazard : MonoBehaviour
{
    [SerializeField] private HazardType _hazardType;

    public HazardType HazardType => _hazardType;
}
