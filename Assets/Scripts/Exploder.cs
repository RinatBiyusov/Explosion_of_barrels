using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Exploder : MonoBehaviour
{
    private float _explosionRadius = 50f;
    private float _explosionForce = 100f;

    public void Explode(Vector3 positon)
    {
        foreach (Cube cube in GetExplodableObjects(positon))
        {
            float force = _explosionForce;

            cube.AddExplosion(force, positon, _explosionRadius);
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