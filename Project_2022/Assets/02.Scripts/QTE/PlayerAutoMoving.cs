using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAutoMoving : MonoBehaviour
{
    public float _gravityScale = 1f;
    public float moveSpeed = 7f;

    public Vector3 moveDir;

    CharacterController _characterController = null;
    void Start()
    {
        _characterController = GetComponent<CharacterController>();
        transform.rotation = Quaternion.Euler(new Vector3(0f, 180, 0f));
    }

    void Update()
    {
        if (GameManager.Instance.IsPause) return;

        Vector3 move = Vector3.forward;

        move = move * moveSpeed * Time.deltaTime;

        move = new Vector3(move.x, -9.8f * _gravityScale * Time.deltaTime, move.z);

        move = transform.TransformDirection(move);
        _characterController.Move(move);
    }
}
