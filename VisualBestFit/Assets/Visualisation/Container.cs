using BestFit.Model;
using UnityEngine;

public class Container : MonoBehaviour
{
    [SerializeField]
    private float _maxWidth = 30, _maxHeight = 15;
    [SerializeField]
    private Transform _transform;

    public void Display (Rectangle rectangle)
    {
        if (rectangle.Width > rectangle.Length)
            rectangle = rectangle.Rotate();

        _transform.localScale = new Vector3(_maxWidth / (float)rectangle.Width, _maxHeight / (float)rectangle.Length, 1f);
    }
}
