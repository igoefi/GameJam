using System.Collections;
using UnityEngine;

public class KDTimer : MonoBehaviour
{
    protected bool _isReady = true;
    protected virtual IEnumerator CheckCD(float timeKD)
    {
        _isReady = false;
        yield return new WaitForSeconds(timeKD);
        _isReady = true;
    }
}
