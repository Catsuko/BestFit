using BestFit.Model;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class BoxPackingView : MonoBehaviour
{
    [SerializeField]
    private Container _container;
    [SerializeField]
    private PhoneTile _tilePrefab;
    [SerializeField]
    private float _delayBetweenTiles = 0.25f;
    [SerializeField]
    private Text _resultLabel;
    [SerializeField]
    private CanvasScaler _canvasScaler;
    [SerializeField]
    private GameObject _detailsForm;
    [SerializeField]
    private GameObject _retryButton;

    private const float BASE_SCALE = 300, BASE_WIDTH = 3, BASE_HEIGHT = 8;

    public void StartPacking (Rectangle phone, Rectangle box)
    {
        _detailsForm.SetActive(false);
        _container.Display(box);
        _resultLabel.text = "";
        AdjustViewportScale(new Vector2((float)box.Width, (float)box.Length));
        StartCoroutine(DisplayTiles(phone, box));
    }

    public void Clear ()
    {
        _container.Display(new Rectangle(0, 0));
        _resultLabel.text = string.Empty;
        _retryButton.SetActive(false);
        foreach (Transform child in _container.transform)
            Destroy(child.gameObject);
        _detailsForm.SetActive(true);
    }

    private void AdjustViewportScale(Vector2 box)
    {
        _canvasScaler.scaleFactor = BASE_SCALE * Mathf.Min(BASE_WIDTH / box.x, BASE_HEIGHT / box.y);
    }

    private IEnumerator DisplayTiles(Rectangle phone, Rectangle box)
    {
        var delay = new WaitForSeconds(Mathf.Min(_delayBetweenTiles / ((float)box.Width * (float)box.Length), 0.25f));
        var count = 0;
        foreach (var tile in phone.Fill(box))
        {
            yield return delay;
            count++;
            var view = Instantiate(_tilePrefab, _container.transform);
            view.Display(tile);
        }
        _resultLabel.text = $"Packed {count} items.";
        _retryButton.SetActive(true);
    }
}
