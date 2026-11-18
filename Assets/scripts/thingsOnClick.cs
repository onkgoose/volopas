using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.EventSystems.EventTrigger;

public class thingsOnClick : MonoBehaviour
{
    [System.Serializable]
    public class ClickDialogue
    {
        [TextArea(2, 4)]
        public List<string> messages = new List<string>();
    }

    [Header("points")]
    [SerializeField] private int pointsPerClick = 1;
    [SerializeField] private int lostEnergy = 1;
    public static int points = 0;

    [Header("panel")]
    [SerializeField] private GameObject dialogPanel;
    [SerializeField] private TextMeshProUGUI dialogText;

    [Header("messages per click")]
    [SerializeField] private List<ClickDialogue> dialoguesPerClick = new List<ClickDialogue>();

    private int _clickCount = 0;
    private int _activeDialogueIndex = -1;
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
        if (_isDialogOpen) return;

        if (_clickCount >= dialoguesPerClick.Count) return;

        if (energy.current < lostEnergy) return;

        var currentDialogue = dialoguesPerClick[_clickCount];

        if (currentDialogue == null || currentDialogue.messages == null || currentDialogue.messages.Count == 0)
        {
            Debug.LogWarning($"[thingsOnClick] Диалог для клика #{_clickCount} пуст!");
            return;
        }

        points += pointsPerClick;
        energy.current -= lostEnergy;

        _activeDialogueIndex = _clickCount;
        _clickCount++;

        _isDialogOpen = true;
        _currentMessageIndex = -1;

        if (dialogPanel != null)
            dialogPanel.SetActive(true);

        ShowNextMessage();

        if (_clickCount >= dialoguesPerClick.Count)
        {
            if (TryGetComponent<Button>(out var button))
                button.interactable = false;
        }
    }

    private void ShowNextMessage()
    {
        if (_activeDialogueIndex < 0 || _activeDialogueIndex >= dialoguesPerClick.Count)
        {
            CloseDialog();
            return;
        }

        var messages = dialoguesPerClick[_activeDialogueIndex].messages;

        _currentMessageIndex++;

        if (_currentMessageIndex >= messages.Count)
        {
            CloseDialog();
            return;
        }

        if (dialogText != null)
            dialogText.text = messages[_currentMessageIndex];
    }

    private void CloseDialog()
    {
        _isDialogOpen = false;
        _activeDialogueIndex = -1;
        _currentMessageIndex = -1;

        if (dialogPanel != null)
            dialogPanel.SetActive(false);
    }
}