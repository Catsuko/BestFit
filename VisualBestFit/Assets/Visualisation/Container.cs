using BestFit.Model;
using UnityEngine;

public class Container : MonoBehaviour
{
    [SerializeField]
    private RectTransform _transform;

    public void Display (Rectangle rectangle)
    {
        _transform.sizeDelta = new Vector2((float)rectangle.Width, (float)rectangle.Length);
    }
}
