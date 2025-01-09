using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ColorChanger), typeof(Cube))]

public class Spawner : MonoBehaviour
{
    [SerializeField] private Cube _cubePrefab;

    private Cube _cubeReference;
    private List<Cube> _activeCubes = new List<Cube>();
    private readonly int _minRangeRandom = 2;
    private readonly int _maxRangeRandom = 6;
    private float _sizeReductionRatio = 2;

    private void RegisterCube(Cube cube)
    {
        cube.Splitting += Split;
        _activeCubes.Add(cube);
    }

    private void UnregisterCube(Cube cube)
    {
        cube.Splitting -= Split;
        _activeCubes.Remove(cube);
    }

    private void Split(Cube cube, Exploder exploder)
    {
        int clones = Random.Range(_minRangeRandom, _maxRangeRandom);
        Cube duplicatedObject = new Cube();
        List<Rigidbody> duplicatedObjects = new List<Rigidbody>();

        for (int i = 0; i < clones; i++)
        {
            duplicatedObject = CreateCube(cube);

            //duplicatedObject = Instantiate(_cubePrefab, cube.transform.position, Quaternion.identity);
            //duplicatedObject.transform.localScale /= _sizeReductionRatio;
            //_colorChanger.ChangeColor(duplicatedObject.GetComponent<MeshRenderer>());

            //RegisterCube(duplicatedObject);

            duplicatedObjects.Add(duplicatedObject.GetComponent<Rigidbody>());
        }

        exploder.ExplodeWhenSplitted(duplicatedObjects);

        UnregisterCube(cube);
    }

    private Cube CreateCube(Cube sourceCube)
    {
        Vector3 newScale = sourceCube.transform.localScale / _sizeReductionRatio;

        Cube newCube = Instantiate(_cubePrefab, sourceCube.transform.position, Random.rotation);

        newCube.Init(newScale);

        RegisterCube(newCube);
        return newCube;
    }

}

// ѕередать спавнеру exploder, чтобы он сделал взрыв, и получал взрыватель только тогда когда нужно [+]
// —оздать корзину дл€ всех кубов внутри спавнера (»зучить как сделано у чувак с инопланет€неном)
// –азобратьс€ почему тер€етс€ ссылка, и имеет ли gameobject все компоненты, которые у него есть (можно, взависимости от их модификатора допуска)
