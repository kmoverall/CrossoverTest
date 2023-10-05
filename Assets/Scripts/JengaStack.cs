using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Linq;

public class JengaStack : MonoBehaviour
{
    [SerializeField]
    private string _grade;

    [SerializeField]
    private JengaBlock _blockPrefab;
    [SerializeField]
    private Vector3 _blockDimensions;

    [SerializeField]
    private TMP_Text _label;

    private List<JengaBlock> _blocks;

    public void Initialize(List<BlockModel> data)
    {
        _blocks = new List<JengaBlock>();
        _label.text = _grade;

        var blockData = data.Where(b => b.grade == _grade);

        blockData = blockData.OrderBy(b => b.domain)
                             .ThenBy(b => b.cluster)
                             .ThenBy(b => b.standardid);

        int layer = 0;
        int posInLayer = -1;
        foreach (var block in blockData)
        {
            var newBlock = Instantiate(_blockPrefab, transform);
            _blocks.Add(newBlock);

            var vOffset = layer * _blockDimensions.y + 0.5f * _blockDimensions.y;
            var hOffset = posInLayer * _blockDimensions.x;

            newBlock.transform.localPosition = new Vector3(
                layer % 2 == 0 ? hOffset : 0,
                vOffset,
                layer % 2 == 0 ? 0 : hOffset);

            newBlock.transform.localEulerAngles = new Vector3(
                0,
                layer % 2 == 0 ? 0 : 90,
                0);

            newBlock.Initialize(block);

            posInLayer++;
            if (posInLayer > 1)
            {
                posInLayer = -1;
                layer++;
            }
        }
    }

    public void TestStack()
    {
        StartCoroutine(TestStackCoroutine(5));
    }

    private IEnumerator TestStackCoroutine(float time)
    {
        foreach (var block in _blocks)
        {
            block.ActivatePhysics();
        }

        yield return new WaitForSeconds(time);

        foreach (var block in _blocks)
        {
            block.Reset();
        }
    }
}
