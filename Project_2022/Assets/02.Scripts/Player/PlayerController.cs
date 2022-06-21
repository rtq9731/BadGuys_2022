using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] public Transform camTrm = null;
    [Header("Control Settings")]
    public float _mouseSensitivity = 100.0f;
    public float _playerSpeed = 5.0f;
    public float _runningSpeed = 7.0f;
    public float _gravityScale = 1f;

    float _horizontalAngle = 0f;
    float _verticalAngle = 0f;
    float _walkSoundDelay = 0f;

    public bool _isPaused = false;
    public bool _isMove = false;

    CharacterController _characterController = null;
    PlayerFootstepSound _footStepSound = null;

    private void Awake()
    {
        _footStepSound = GetComponent<PlayerFootstepSound>();
        _characterController = GetComponent<CharacterController>();
    }

    private void Start()
    {
        UIManager.Instance.DisplayCursor(false);

        _mouseSensitivity = SettingManager.Instance.Setting.GetValue(SettingManager.SettingInfo.SettingType.MOUSESENSITIVITY);

        SettingManager.onChangeSetting += (setting) =>
        {
            _mouseSensitivity = setting.GetValue(SettingManager.SettingInfo.SettingType.MOUSESENSITIVITY);
        };
    }

    private void Update()
    {
        if(GameManager.Instance.IsPause)
        {
            return;
        }

        Vector3 move = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxisRaw("Vertical"));

        if (move != Vector3.zero)
        {
            _isMove = true;
            _walkSoundDelay += Time.deltaTime;
            if(_walkSoundDelay >= 0)
            {
                if (_walkSoundDelay >= 0.2f)
                {
                    _footStepSound.PlaySound();
                }
            }
        }
        else
        {
            _isMove = false;
            _walkSoundDelay = 0f;
            _footStepSound.PauseSound();
        }

        if (move.sqrMagnitude > 1.0f)
        {
            move.Normalize();
        }

        move = move * (Input.GetButton("Run") ? _runningSpeed : _playerSpeed) * Time.deltaTime;

        _footStepSound.SetPitch(Input.GetButton("Run") ? 1.5f : 1.2f);

        move = new Vector3(move.x, -9.8f * _gravityScale * Time.deltaTime, move.z);

        float turnPlayer = Input.GetAxis("Mouse X") * (_mouseSensitivity / 5f);
        _horizontalAngle = _horizontalAngle + turnPlayer;

        if (_horizontalAngle > 360) _horizontalAngle -= 360.0f;
        if (_horizontalAngle < 0) _horizontalAngle += 360.0f;

        Vector3 currentAngles = transform.localEulerAngles;
        currentAngles.y = _horizontalAngle;
        transform.localEulerAngles = currentAngles;

        var turnCam = -Input.GetAxis("Mouse Y");
        turnCam = turnCam * (_mouseSensitivity / 5f);
        _verticalAngle = Mathf.Clamp(turnCam + _verticalAngle, -89.0f, 89.0f);
        currentAngles = camTrm.localEulerAngles;
        currentAngles.x = _verticalAngle;
        camTrm.localEulerAngles = currentAngles;

        move = transform.TransformDirection(move);
        _characterController.Move(move);
    }
}
