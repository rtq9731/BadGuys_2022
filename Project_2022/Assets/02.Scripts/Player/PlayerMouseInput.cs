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
    InventoryInput _inventoryInput;
    Inventory _inventory = null;

    Transform _curTouchObj = null;

    private Action<IInteractableItem> _onItemOverMouse = (x) => { };
    private Action<IInteractAndGetItemObj, ItemInfo> _onObjOverMouse = (x, y) => { };
    private Action _onItemExitMouse = () => { };

    private void Awake()
    {
        _playerController = FindObjectOfType<PlayerController>();
        _playerPickUpManager = FindObjectOfType<PlayerPickUpManager>();
        _inventoryInput = FindObjectOfType<InventoryInput>();
        _inventory = FindObjectOfType<Inventory>();

        _onItemOverMouse += _playerPickUpManager.CanPickUpItem;
        _onObjOverMouse += _playerPickUpManager.CanInteractObj;
        _onItemOverMouse += _inventoryInput.RemoveItmeFalse;
        
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

            IInteractAndGetItemObj curObj = hitInfo.transform.GetComponent<IInteractAndGetItemObj>();
            if (curObj != null && _inventory.MainItem != null)
            {
                Debug.Log("obj");
                _onObjOverMouse(curObj, _inventory.MainItem);
                return;
            }

            //
            IInteractableItem curItem = hitInfo.transform.GetComponent<Item>();
            if (curItem != null)
            {
                _onItemOverMouse(curItem);
            }
            else if (hitInfo.transform.GetComponent<IInteractableItem>() != null)
            {
                curItem = hitInfo.transform.GetComponent<IInteractableItem>();
                _onItemOverMouse(curItem);
            }
            else
            {
                _onItemExitMouse();
            }
            //
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
