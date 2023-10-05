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
    public int CurrentTargetIndex = 1;

    private void Start()
    {
        _moveAction = CoreController.Input.actions["Move"];
        _orbitAction = CoreController.Input.actions["Orbit"];
    }

    private void Update()
    {
        if (_orbitAction.ReadValue<float>() > 0)
        {
            var motion = _moveAction.ReadValue<Vector2>();

            _cameraRoot.Rotate(Vector3.up, motion.x * _orbitRate * Time.deltaTime);
        }
    }

    public void CycleLeft()
    {
        CurrentTargetIndex = (CurrentTargetIndex - 1) % CoreController.JengaStacks.Length;
        CurrentTargetIndex = CurrentTargetIndex < 0 ? CoreController.JengaStacks.Length - 1 : CurrentTargetIndex;

        CoreController.TargetStackIndex = CurrentTargetIndex;
        _cameraRoot.position = CoreController.TargetedStack.transform.position;
    }
    public void CycleRight()
    {
        CurrentTargetIndex = (CurrentTargetIndex + 1) % CoreController.JengaStacks.Length;

        CoreController.TargetStackIndex = CurrentTargetIndex;
        _cameraRoot.position = CoreController.TargetedStack.transform.position;
    }


}
