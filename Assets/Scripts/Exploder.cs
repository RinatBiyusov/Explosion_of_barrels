using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Exploder : MonoBehaviour
{
    [SerializeField] private float _explosionRadius = 100f;
    [SerializeField] private float _explosionForce = 10f;

    public void ExplodeWhenNotSplitted(GameObject cube)
    {
        foreach (Rigidbody explodableObject in GetExplodableObjects(gameObject))
            explodableObject.AddExplosionForce(_explosionForce, explodableObject.transform.position, _explosionRadius);
    }

    public void ExplodeWhenSplitted(List<Rigidbody> cubes)
    {
        foreach (Rigidbody explodableObject in cubes)
            explodableObject.AddExplosionForce(_explosionForce, explodableObject.transform.position, _explosionRadius);
    }

    private List<Rigidbody> GetExplodableObjects(GameObject cube)
    {
        Collider[] hits = Physics.OverlapSphere(gameObject.transform.position, _explosionRadius);

        List<Rigidbody> cubes = new List<Rigidbody>();

        foreach (Collider hit in hits)
            if (hit.attachedRigidbody != null)
                cubes.Add(hit.attachedRigidbody);

        return cubes;
    }
}