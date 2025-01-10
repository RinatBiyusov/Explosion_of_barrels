using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Cube))]
public class Spawner : MonoBehaviour
{
    [SerializeField] private Cube _cubeReference;

    private readonly int _minCreatedCubes = 2;
    private readonly int _maxCreatedCubes = 6;
    private float _sizeReductionRatio = 2;

    private void OnEnable()
    {
        _cubeReference.Splitting += Split;
    }

    private void OnDisable()
    {
        _cubeReference.Splitting -= Split;
    }

    private void Split(Cube cube)
    {
        int clones = Random.Range(_minCreatedCubes, _maxCreatedCubes + 1);
        Cube duplicatedObject;
        List<Cube> duplicatedObjects = new List<Cube>();

        for (int i = 0; i < clones; i++)
        {
            duplicatedObject = CreateCube(cube);

            duplicatedObject.Splitting += Split;

            duplicatedObjects.Add(duplicatedObject);
        }

        cube.Exploder.ExplodeWhenSplitted(duplicatedObjects);

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