using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JengaBlock : MonoBehaviour
{
    [SerializeField]
    private Material[] _masteryMaterials;

    [SerializeField]
    private MeshRenderer _blockRenderer;
    [SerializeField]
    private Rigidbody _blockPhysics;

    private BlockModel _model;

    public void Initialize(BlockModel data)
    {
        _model = data;
        _blockRenderer.material = _masteryMaterials[data.mastery];
    }

    public void ActivatePhysics()
    {
        if (_model.mastery == 0)
        {
            gameObject.SetActive(false);
            return;
        }

        _blockPhysics.isKinematic = false;
    }

    public void Reset()
    {
        _blockPhysics.isKinematic = true;
        gameObject.SetActive(true);
        // Parent stays at original location while block is affected by physics
        _blockRenderer.transform.localPosition = Vector3.zero;
        _blockRenderer.transform.localRotation = Quaternion.identity;
    }
}
