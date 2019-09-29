using BestFit.Model;
using UnityEngine;

public class StartRunButton : MonoBehaviour
{
    [SerializeField]
    private DimensionField _phoneWidth, _phoneHeight, _boxWidth, _boxHeight;
    [SerializeField]
    private BoxPackingView _packingView;

    public void Submit ()
    {
        var phone = new Rectangle(_phoneWidth.Value(), _phoneHeight.Value());
        var box = new Rectangle(_boxWidth.Value(), _boxHeight.Value());
        if (phone.Width * phone.Length * box.Width * box.Length > 0)
        {
            _packingView.StartPacking(phone, box);
        }
    }
}
