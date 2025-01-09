using UnityEngine;
using System;

public class Cube : MonoBehaviour
{
    [SerializeField] private float _chanceNotToDisappear = 1f;

    private float _decreaceChanceNotToDisappear = 2f;
    private Exploder _exploder;
    private ColorChanger _colorChanger;
    private MeshRenderer _meshRenderer;

    public event Action<Cube, Exploder> Splitting;

    private void Awake()
    {
        _exploder = GetComponent<Exploder>();
        _colorChanger = GetComponent<ColorChanger>();
        _meshRenderer = GetComponent<MeshRenderer>();
    }

    private void OnMouseDown()
    {
        float random = UnityEngine.Random.value;

        if (_chanceNotToDisappear >= random)
        {
            _chanceNotToDisappear /= _decreaceChanceNotToDisappear;
            Splitting?.Invoke(this, _exploder);
        }
        else
        {
            _exploder.ExplodeWhenNotSplitted(gameObject);
        }

        Destroy(gameObject);
    }

    public void Init(Vector3 scale)
    {
        transform.localScale = scale;
        _colorChanger.ChangeColor(_meshRenderer);
    }
}