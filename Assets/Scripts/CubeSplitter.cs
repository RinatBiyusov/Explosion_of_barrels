using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Cube))]
[RequireComponent(typeof(ColorChanger))]

public class CubeSpliter : MonoBehaviour
{
    [SerializeField] private float _chanceNotToDisappear = 1f;

    private Cube _cube;
    private ColorChanger _colorChanger;
    private readonly int _minRangeRandom = 2;
    private readonly int _maxRangeRandom = 6;
    private float _decreaceChanceNotToDisappear = 2f;

    private void Awake()
    {
        _cube = GetComponent<Cube>();
        _colorChanger = GetComponent<ColorChanger>();
    }

    private void OnEnable()
    {
        _cube.Splitter += TrySplit;
    }

    private void OnDisable()
    {
        _cube.Splitter -= TrySplit;
    }

    public void TrySplit()
    {
        float chanceDisappear = Random.Range(0, 1f);

        if (_chanceNotToDisappear >= chanceDisappear)
        {
            Split(_cube);
        }

        Destroy(gameObject);
    }

    private void Split(Cube cube)
    {
        int clones = Random.Range(_minRangeRandom, _maxRangeRandom);
        Cube duplicatedObject;
        List<Cube> duplicatedObjects = new List<Cube>();

        _chanceNotToDisappear /= _decreaceChanceNotToDisappear;

        for (int i = 0; i < clones; i++)
        {
            duplicatedObject = Instantiate(cube, cube.transform.position, Quaternion.identity);
            duplicatedObject.transform.localScale /= _decreaceChanceNotToDisappear;
            _colorChanger.ChangeColor(duplicatedObject.GetComponent<MeshRenderer>());

            duplicatedObjects.Add(duplicatedObject);
        }

        Exploder.exploder.Explode(cube.gameObject);
    }
}