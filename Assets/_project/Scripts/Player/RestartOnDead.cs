using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class RestartOnDead : MonoBehaviour
{
    [SerializeField] Image _deadImage;

    private void Start()
    {
        PlayerStats.Instance.DeadEvent.AddListener(Dead);

        Time.timeScale = 1;
    }

    private void Dead()
    {
        _deadImage.gameObject.SetActive(true);
        Time.timeScale = 0;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
