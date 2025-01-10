using UnityEngine;
using System;

[RequireComponent(typeof(Exploder), typeof(ColorChanger), typeof(Rigidbody))]
public class Cube : MonoBehaviour
{
    [SerializeField] private float _currentSplitChance = 1;
    private Exploder _exploder;
    private ColorChanger _colorChanger;
    private float _probabilityReductionSplit = 2;

    public Rigidbody Rigidbody { get; private set; }

    public event Action<Cube> Splitting;

    private void Awake()
    {
        _exploder = GetComponent<Exploder>();
        _colorChanger = GetComponent<ColorChanger>();
        Rigidbody = GetComponent<Rigidbody>();
    }

    private void OnMouseDown()
    {
        float random = UnityEngine.Random.value;

        if (_currentSplitChance >= random)
        {
            Splitting?.Invoke(this);
        }
        else
        {
            _exploder.Explode(transform.position);
        }

        Destroy(gameObject);
    }

    public void AddExplosion(float baseForce, Vector3 center, float baseRadius)
    {
        float upwardsModifier = 0.1f;

        int forceMultiplier = 3;

        float sizeFactor = 1f / transform.localScale.magnitude;
        float adjustedForce = baseForce * sizeFactor * forceMultiplier;
        float adjustedRadius = baseRadius * sizeFactor * forceMultiplier;

        Rigidbody.AddExplosionForce(adjustedForce, center, adjustedRadius, upwardsModifier, ForceMode.Impulse);
    }

    public void Init(Vector3 scale, Cube sourceCube)
    {
        ChangeScale(scale);
        _colorChanger.ChangeColor(sourceCube.GetComponent<MeshRenderer>());
        UpdateSplitChance(sourceCube._currentSplitChance);
    }

    private void ChangeScale(Vector3 scale)
    {
        transform.localScale = scale;
    }

    private void UpdateSplitChance(float splitChance)
    {
        splitChance /= _probabilityReductionSplit;
        _currentSplitChance = splitChance;
    }
}