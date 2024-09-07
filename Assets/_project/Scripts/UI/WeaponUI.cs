using TMPro;
using UnityEngine;

public class WeaponUI : MonoBehaviour
{
    [SerializeField] TMP_Text _text;
    private WeaponSystem _system;
    
    private void Start() =>
        _system = PlayerStats.Instance.GetComponent<WeaponSystem>();

    private void Update()
    {
        _text.text = _system.SelectedWeapon.GetInfo();
    }

}
