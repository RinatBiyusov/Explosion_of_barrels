using System.Collections.Generic;
using System;
using UnityEngine;

public class CubeSpliter : MonoBehaviour
{
    [SerializeField] private float _chanceNotToDisappear = 1f;

    private Cube _cube;
    private Exploder _exploder;
    private ColorChanger _colorChanger;
    private readonly int _minRangeRandom = 2;
    private readonly int _maxRangeRandom = 6;
    private float _decreaceChanceNotToDisappear = 2f;

    private void Awake()
    {
        _cube = GetComponent<Cube>();
        _exploder = GetComponent<Exploder>();
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
        if (gameObject != null)
        {
            float chanceDisappear = UnityEngine.Random.Range(0, 1f);

            Debug.Log(chanceDisappear + " " + _chanceNotToDisappear);

            if (_chanceNotToDisappear >= chanceDisappear)
            {
                Split(gameObject);
                Destroy(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }
    }

    private void Split(GameObject cube)
    {
        int clones = UnityEngine.Random.Range(_minRangeRandom, _maxRangeRandom);
        GameObject duplicatedObject;
        List<GameObject> duplicatedObjects = new List<GameObject>();

        _chanceNotToDisappear /= _decreaceChanceNotToDisappear;

        for (int i = 0; i < clones; i++)
        {
            duplicatedObject = Instantiate(cube, cube.transform.position, Quaternion.identity);
            duplicatedObject.transform.localScale = duplicatedObject.transform.localScale / 2;
            _colorChanger.ChangeColor(duplicatedObject.GetComponent<MeshRenderer>()); 

            duplicatedObjects.Add(duplicatedObject);
        }

        _exploder.Explode(duplicatedObjects);
    }
}