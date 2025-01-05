using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class CubeSpliter : MonoBehaviour
{
    [SerializeField] private float _chanceNotToDisappear = 1f;

    private Cube _cube;
    private ColorChanger _colorChanger;
    private readonly int _minRangeRandom = 2;
    private readonly int _maxRangeRandom = 6;
    private float _decreaceChanceNotToDisappear = 2f;

    public void TrySplit()
    {
        float chanceDisappear = Random.Range(0, 1f);

        if (_chanceNotToDisappear >= chanceDisappear)
        {
            Split(gameObject);
        }

        Destroy(gameObject);
    }

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

    private void Split(GameObject cube)
    {
        int clones = Random.Range(_minRangeRandom, _maxRangeRandom);
        GameObject duplicatedObject;
        List<GameObject> duplicatedObjects = new List<GameObject>();

        _chanceNotToDisappear /= _decreaceChanceNotToDisappear;

        for (int i = 0; i < clones; i++)
        {
            duplicatedObject = Instantiate(cube, cube.transform.position, Quaternion.identity);
            duplicatedObject.transform.localScale /= 2;
            _colorChanger.ChangeColor(duplicatedObject.GetComponent<MeshRenderer>());

            duplicatedObjects.Add(duplicatedObject);
        }

        Exploder.exploder.Explode(gameObject);
    }
}