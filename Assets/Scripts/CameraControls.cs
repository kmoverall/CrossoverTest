using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraControls : MonoBehaviour
{
    [SerializeField]
    private Transform _cameraRoot;

    [SerializeField]
    private float _orbitRate = 1;

    private InputAction _moveAction;
    private InputAction _orbitAction;

    [SerializeField]
    private int _currentTargetIndex;

    private void Start()
    {
        _moveAction = CoreController.Input.actions["Move"];
        _orbitAction = CoreController.Input.actions["Orbit"];
    }

    private void Update()
    {
        Cursor.visible = _orbitAction.ReadValue<float>() == 0;
        if (_orbitAction.ReadValue<float>() > 0)
        {
            var motion = _moveAction.ReadValue<Vector2>();

            _cameraRoot.Rotate(Vector3.up, motion.x * _orbitRate * Time.deltaTime);
        }
    }

    public void CycleLeft()
    {
        _currentTargetIndex = (_currentTargetIndex - 1) % CoreController.JengaStacks.Length;
        _currentTargetIndex = _currentTargetIndex < 0 ? CoreController.JengaStacks.Length - 1 : _currentTargetIndex;

        _cameraRoot.position = CoreController.JengaStacks[_currentTargetIndex].transform.position;
    }
    public void CycleRight()
    {
        _currentTargetIndex = (_currentTargetIndex + 1) % CoreController.JengaStacks.Length;

        _cameraRoot.position = CoreController.JengaStacks[_currentTargetIndex].transform.position;
    }


}
