using BestFit.Model;
using UnityEngine;
using UnityEngine.UI;

public class PhoneTile : MonoBehaviour
{
    [SerializeField]
    private RectTransform _transform;
    [SerializeField]
    private Image _backgroundImage;
    [SerializeField]
    private Color _backgroundColor = Color.green, _alternateColor = Color.red;

    public void Display (FittedRectangle rectangle)
    {
        rectangle.Print(Display);
    }

    public void Display (Rectangle rectangle, double x, double y)
    {
        _transform.anchoredPosition = new Vector3((float)x, (float)y, 0);
        _transform.sizeDelta = new Vector2((float)rectangle.Width, (float)rectangle.Length);
        _backgroundImage.color = rectangle.Width > rectangle.Length ? _backgroundColor : _alternateColor;
    }

    public void Remove ()
    {
        Destroy(gameObject);
    }
}
