using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMouseInput : MonoBehaviour
{
    [SerializeField] float _playerArmLength = 1f;
    [SerializeField] LayerMask _whatIsTouchable;
    PlayerController _playerController;

    Transform _curTouchObj = null;

    private void Awake()
    {
        _playerController = GetComponentInParent<PlayerController>();
    }

    private void Update()
    {
        if (_playerController._isPaused)
        {
            return;
        }

        Ray ray = new Ray(transform.position, transform.forward);
        if (Physics.Raycast(ray, out RaycastHit hitInfo, _playerArmLength, _whatIsTouchable))
        {
            if(_curTouchObj != hitInfo.transform)
            {
                _curTouchObj?.GetComponent<IPlayerMouseExitHandler>()?.OnPlayerMouseExit();
                _curTouchObj = hitInfo.transform;
                _curTouchObj?.GetComponent<IPlayerMouseEnterHandler>()?.OnPlayerMouseEnter();
                Debug.Log(_curTouchObj.gameObject.name);
            }
        }
        else
        {
            _curTouchObj?.GetComponent<IPlayerMouseExitHandler>()?.OnPlayerMouseExit();
            _curTouchObj = null;
        }

        if(_curTouchObj != null)
        {
            if (Input.GetMouseButtonDown(0))
            {
                _curTouchObj.GetComponent<IGetPlayerMouseHandler>()?.OnGetPlayerMouse();
            }
        }
    }
}
