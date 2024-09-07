
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HPUI : MonoBehaviour
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
        var HP = _stats.HP;
        var maxHP = _stats.MaxHP;
        _text.text = $"{HP} / {maxHP}";
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
