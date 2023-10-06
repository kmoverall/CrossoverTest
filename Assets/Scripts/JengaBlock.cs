using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class JengaBlock : MonoBehaviour, IPointerClickHandler
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

    //Would prefer to do this with the new Input System, but documentation isn't great and time is limited
    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button != PointerEventData.InputButton.Right)
        {
            return;
        }

        var infoString = $"{_model.grade}: {_model.domain}\n\n" +
            $"{_model.cluster}\n\n" +
            $"{_model.standardid}: {_model.standarddescription}";
        CoreController.UI.ShowInfo(infoString);
    }
}
