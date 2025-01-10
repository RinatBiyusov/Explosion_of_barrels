using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Exploder : MonoBehaviour
{
    private float _explosionRadius = 50f;
    private float _explosionForce = 100f;

    public void Explode(Vector3 positon)
    {
        float upwardsModifier = 0.1f;
        int forceMultiplier = 3;
        float force = _explosionForce;

        foreach (Cube cube in GetExplodableObjects(positon))
        {
            float sizeFactor = 1f / transform.localScale.magnitude;
            float adjustedForce = _explosionForce * sizeFactor * forceMultiplier;
            float adjustedRadius = _explosionRadius * sizeFactor * forceMultiplier;

            cube.Rigidbody.AddExplosionForce(adjustedForce, positon, adjustedRadius, upwardsModifier, ForceMode.Impulse);
        }
    }

    public void ExplodeWhenSplitted(List<Cube> cubes)
    {
        foreach (Cube explodableObject in cubes)
            explodableObject.Rigidbody.AddExplosionForce(_explosionForce, explodableObject.transform.position, _explosionRadius);
    }

    private List<Cube> GetExplodableObjects(Vector3 position)
    {
        Collider[] hits = Physics.OverlapSphere(position, _explosionRadius);

        List<Cube> cubes = new List<Cube>();

        foreach (Collider collider in hits)
            if (collider.TryGetComponent<Cube>(out Cube cube))
                cubes.Add(cube);

        return cubes;
    }
}