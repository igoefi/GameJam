
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DashUI : MonoBehaviour
{
    //[SerializeField] RectTransform _position;
    //[SerializeField] Vector2 _offset;
    //[SerializeField] Image _imagePrefab;
    //private Image[] _images;

    [SerializeField] TMP_Text _text;

    private PlayerStats _stats;

    private void Start() =>
        _stats = PlayerStats.Instance;

    private void Update()
    {
        var maxDash = _stats.DashCount;
        var haveDash = _stats.HaveDashCount;
        _text.text = $"{haveDash} / {maxDash}";
    }

    //private void SetDashCount()
    //{
    //    for(int i = 0; i < _maxDash; i++)
    //    {
    //        if (_images.Length <= i)
    //        {

    //        }
    //        else
    //        {

    //        }
    //    }
    //}
    
    //private void AddImage()
    //{
    //    var position = _po
    //    GameObject.Instantiate
    //}
}
