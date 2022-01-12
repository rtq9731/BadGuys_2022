using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerMouseInput : MonoBehaviour
{
    [SerializeField] float _playerArmLength = 1f;
    [SerializeField] LayerMask _whatIsTouchable;
    PlayerController _playerController;
    PlayerPickUpManager _playerPickUpManager;

    Transform _curTouchObj = null;

    private Action<IInteractableItem> _onItemOverMouse = (x) => { };
    private Action _onItemExitMouse = () => { };

    private void Awake()
    {
        _playerController = FindObjectOfType<PlayerController>();
        _playerPickUpManager = FindObjectOfType<PlayerPickUpManager>();

        _onItemOverMouse += _playerPickUpManager.CanPickUpItem;
        _onItemExitMouse += () => _playerPickUpManager.ShowPickUpIcon(false);
    }

    private void Update()
    {
        if (_playerController._isPaused)
        {
            return;
        }

        Vector3 mousePos = Vector3.zero;
        Ray ray;
        if (UIStackManager.IsUIStackEmpty())
        {
            ray = new Ray(transform.position, transform.forward);
        }
        else
        {
            ray = Camera.main.ScreenPointToRay(mousePos);
            mousePos = Input.mousePosition;
        }

        if (Physics.Raycast(ray, out RaycastHit hitInfo, _playerArmLength, _whatIsTouchable))
        {
            if(_curTouchObj != hitInfo.transform)
            {
                _curTouchObj?.GetComponents<IPlayerMouseExitHandler>()?.ToList().ForEach(x => x.OnPlayerMouseExit());
                _curTouchObj = hitInfo.transform;
                _curTouchObj?.GetComponents<IPlayerMouseEnterHandler>()?.ToList().ForEach(x => x.OnPlayerMouseEnter());
            }

            IInteractableItem curItem = hitInfo.transform.GetComponent<Item>();
            if (curItem != null)
            {
                _onItemOverMouse(curItem);
            }
            else
            {
                _onItemExitMouse();
            }
        }
        else if(_curTouchObj != null)
        {
            _curTouchObj?.GetComponents<IPlayerMouseExitHandler>()?.ToList().ForEach(x => x.OnPlayerMouseExit());
            _curTouchObj = null;
            _onItemExitMouse();
        }

        if(_curTouchObj != null)
        {
            if (Input.GetMouseButtonDown(0))
            {
                _curTouchObj.GetComponents<IGetPlayerMouseHandler>()?.ToList().ForEach(x => x.OnGetPlayerMouse());
            }
        }

    }
}
