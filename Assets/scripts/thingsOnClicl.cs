using System.Collections.Generic;
using UnityEngine;
using TMPro; // Если используется TextMeshPro

public class ResearchDialogSystem : MonoBehaviour
{
    [Header("points")]
    [SerializeField] private int pointsPerClick = 10;
    public static int _researchPoints = 0;

    [Header("panel")]
    [SerializeField] private GameObject dialogPanel;
    [SerializeField] private TextMeshProUGUI dialogText;

    [Header("messages qe")]
    [TextArea(2, 4)]
    [SerializeField] private List<string> messagesToShow = new List<string>();

    private int _currentMessageIndex = -1;
    private bool _isDialogOpen = false;

    private void Start()
    {

        if (dialogPanel != null)
            dialogPanel.SetActive(false);
    }

    private void Update()
    {
        if (_isDialogOpen && Input.GetKeyDown(KeyCode.Space))
        {
            ShowNextMessage();
        }
    }

    public void OnResearchButtonClick()
    {
        _researchPoints += pointsPerClick;

        if (!_isDialogOpen && messagesToShow != null && messagesToShow.Count > 0)
        {
            _isDialogOpen = true;
            _currentMessageIndex = -1;

            if (dialogPanel != null)
                dialogPanel.SetActive(true);

            ShowNextMessage();
        }
    }

    private void ShowNextMessage()
    {
        _currentMessageIndex++;
        if (_currentMessageIndex >= messagesToShow.Count)
        {
            CloseDialog();
            return;
        }

        if (dialogText != null)
            dialogText.text = messagesToShow[_currentMessageIndex];
    }

    private void CloseDialog()
    {
        _isDialogOpen = false;
        _currentMessageIndex = -1;

        if (dialogPanel != null)
            dialogPanel.SetActive(false);
    }
}