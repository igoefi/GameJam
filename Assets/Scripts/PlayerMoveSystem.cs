using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMoveSystem : MonoBehaviour
{
    [SerializeField] private int _speed;
    [SerializeField] private Animator _animator;
    [SerializeField] private SpriteRenderer _playerSprite;
    private PlayerController _playerController;
    private Rigidbody _rigidbody;
    private Vector3 _movement;

    private const string Is_RUN_PARAM = "IsRun";

    private void  Awake()
    {
        _playerController = new PlayerController();
    }

    private void OnEnable()
    {
        _playerController.Enable();
    }

    void Start()
    {
        _rigidbody = gameObject.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
   

        float x = _playerController.Player.Newaction.ReadValue<Vector2>().x;
        float z = _playerController.Player.Newaction.ReadValue<Vector2>().y;

        _movement = new Vector3(x,0, z).normalized;

        _animator.SetBool(Is_RUN_PARAM, _movement!=Vector3.zero);

        if(x!=0&&x<0){
            _playerSprite.flipX = true;
        }
        
        if(x!=0&&x>0){
            _playerSprite.flipX = false;
        }   
    }
    void FixedUpdate()
    {
        _rigidbody.MovePosition(transform.position + _movement * _speed * Time.fixedDeltaTime);
    }
}
