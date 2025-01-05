using System.Collections.Generic;
using UnityEngine;

public class Exploder : MonoBehaviour 
{
    public static Exploder exploder { get; private set; }

    [SerializeField] private  float _explosionRadius = 100f;
    [SerializeField] private  float _explosionForce = 10f;

    private void Awake()
    {
        if (exploder != null && exploder != this)
        {
            Destroy(gameObject);
            return;
        }

        exploder = this;

        DontDestroyOnLoad(gameObject);
    }

    public void Explode(GameObject gameObject)
    {
        foreach (Rigidbody explodableObject in GetExplodableObjects(gameObject))
            explodableObject.AddExplosionForce(_explosionForce, explodableObject.transform.position, _explosionRadius);
    }

    private List<Rigidbody> GetExplodableObjects(GameObject gameObject)
    {
        Collider[] hits = Physics.OverlapSphere(gameObject.transform.position, _explosionRadius);

        List<Rigidbody> cubes = new();

        foreach (Collider hit in hits)
            if (hit.attachedRigidbody != null)
                cubes.Add(hit.attachedRigidbody);

        return cubes;
    }
}
