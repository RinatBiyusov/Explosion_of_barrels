using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Cube))]

public class Spawner : MonoBehaviour
{
    [SerializeField] private Exploder _exploder;
    [SerializeField] private Cube _cubeReference;

    private readonly int _minRangeRandom = 2;
    private readonly int _maxRangeRandom = 6;
    private float _sizeReductionRatio = 2;
    private List<Cube> _listActiveCubes = new List<Cube>();

    private void Awake()
    {
        _exploder = GetComponent<Exploder>();
    }

    private void OnEnable()
    {
        _cubeReference.Splitting += Split;
    }

    private void Split(Cube cube)
    {
        int clones = Random.Range(_minRangeRandom, _maxRangeRandom);
        Cube duplicatedObject;
        List<Rigidbody> duplicatedObjects = new List<Rigidbody>();

        cube.Splitting -= Split;

        for (int i = 0; i < clones; i++)
        {
            duplicatedObject = CreateCube(cube);

            duplicatedObject.Splitting += Split;

            duplicatedObjects.Add(duplicatedObject.GetComponent<Rigidbody>());
        }

        _exploder.ExplodeWhenSplitted(duplicatedObjects);

       cube.Splitting -= Split;
    }

    private Cube CreateCube(Cube sourceCube)
    {
        Vector3 newScale = sourceCube.transform.localScale / _sizeReductionRatio;

        Cube newCube = Instantiate(sourceCube, sourceCube.transform.position, Random.rotation);

        newCube.Init(newScale, newCube);

        return newCube;
    }
}