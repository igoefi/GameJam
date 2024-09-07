using System.Collections;
using UnityEngine;

public class Movement : KDTimer
{
    private Inputs _input;
    private Rigidbody _body;
    private PlayerStats _stats;

    private Coroutine _addDashCoroutine;

    private void Start()
    {
        _input = Inputs.Instance;
        _stats = PlayerStats.Instance;
        _body = GetComponent<Rigidbody>();
        _input.Dash.AddListener(Dash);
    }

    private void FixedUpdate()
    {
        if (!_stats.CanMove) return;
        if (!_isReady) return;

        var moveSpeed = _stats.Speed * _stats.WeaponSystem.MoveCoef;
        var y = _body.velocity.y > 1 ? 1 : _body.velocity.y;
        _body.velocity = new Vector3(_input.Move.x * moveSpeed, y, _input.Move.y * moveSpeed);
    }

    private void Dash(bool isDash)
    {
        if (!isDash) return;
        if (!_isReady) return;
        if (!_stats.CanMove) return;
        if (_stats.HaveDashCount <= 0) return;

        _stats.HaveDashCount--;

        var dashForce = _stats.DashForce;
        var impulse = new Vector3(_input.Move.x * dashForce, _body.velocity.y, _input.Move.y * dashForce);
        _body.AddForce(impulse, ForceMode.Impulse);

        StartCoroutine(CheckCD(_stats.DashKD));

        if (_addDashCoroutine != null)
            StopCoroutine(_addDashCoroutine);
        _addDashCoroutine = StartCoroutine(AddOneDash());
    }

    private IEnumerator AddOneDash()
    {
        yield return new WaitForSeconds(_stats.DashAddKD);
        _stats.HaveDashCount++;

        if (_stats.HaveDashCount < _stats.DashCount)
            _addDashCoroutine = StartCoroutine(AddOneDash());
        else
            _addDashCoroutine = null;
    }

    protected override IEnumerator CheckCD(float timeKD)
    {
        _isReady = false;
        _stats.IsDashing = true;
        yield return new WaitForSeconds(timeKD);
        _stats.IsDashing = false;
        _isReady = true;
    }
}
