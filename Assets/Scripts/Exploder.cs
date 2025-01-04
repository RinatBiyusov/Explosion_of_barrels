using System;
using System.Collections.Generic;
using UnityEngine;

public class Exploder : MonoBehaviour
{
    [SerializeField] private float _explosionRadius = 100f;
    [SerializeField] private float _explosionForce = 10f;

    public void Explode(List<GameObject> cubes)
    {
        foreach (GameObject explodableObject in cubes)
            explodableObject.GetComponent<Rigidbody>().AddExplosionForce(_explosionForce, transform.position, _explosionRadius);
    }
}
