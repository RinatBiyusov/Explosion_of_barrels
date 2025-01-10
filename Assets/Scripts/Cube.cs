using UnityEngine;
using System;

[RequireComponent(typeof(Exploder), typeof(ColorChanger), typeof(Rigidbody))]
public class Cube : MonoBehaviour
{
    [SerializeField] private float _currentSplitChance = 1;
    private ColorChanger _colorChanger;
    private float _probabilityReductionSplit = 2;

    public Exploder Exploder {  get; private set; }
    public Rigidbody Rigidbody { get; private set; }

    public event Action<Cube> Splitting;

    public void Init(Vector3 scale, Cube sourceCube)
    {
        transform.localScale = scale;
        _colorChanger.ChangeColor(sourceCube.GetComponent<MeshRenderer>());
        UpdateSplitChance(sourceCube._currentSplitChance);
    }

    private void Awake()
    {
        _colorChanger = GetComponent<ColorChanger>();
        Exploder = GetComponent<Exploder>();
        Rigidbody = GetComponent<Rigidbody>();
    }

    private void OnMouseDown()
    {
        float random = UnityEngine.Random.value;

        if (_currentSplitChance >= random)
        {
            Splitting?.Invoke(this);
            Debug.Log(_currentSplitChance);
        }
        else
        {
            Exploder.Explode(transform.position);
        }

        Destroy(gameObject);
    }

    private void UpdateSplitChance(float splitChance)
    {
        splitChance /= _probabilityReductionSplit;
        _currentSplitChance = splitChance;
    }
}