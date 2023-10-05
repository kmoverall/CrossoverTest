using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIController : MonoBehaviour
{
    [SerializeField]
    private Button _cycleLeftButton;
    [SerializeField]
    private Button _cycleRightButton;
    [SerializeField]
    private Button _testButton;
    [SerializeField]
    private GameObject _infoPanel;
    [SerializeField]
    private TMP_Text _infoText;

    private void Start()
    {
        _cycleLeftButton.onClick.AddListener(CoreController.Camera.CycleLeft);
        _cycleRightButton.onClick.AddListener(CoreController.Camera.CycleRight);
        _testButton.onClick.AddListener(TestStack);

        _infoPanel.SetActive(false);
    }

    private void TestStack() => CoreController.TargetedStack.TestStack();


}
