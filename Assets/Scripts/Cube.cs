using System;
using UnityEngine;

public class Cube : MonoBehaviour
{
    public event Action Splitter;

    private void OnMouseDown()
    {
        Splitter?.Invoke();
    }
}
