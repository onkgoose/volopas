using UnityEngine;
using UnityEngine.UI;

public class NotebookController : MonoBehaviour
{
    [Header("UI Elements")]
    [SerializeField] private Image pageDisplay;
    [SerializeField] private Button backButton;
    [SerializeField] private Button nextButton;
    [Header("Pages")]
    [SerializeField] private Sprite[] pageSprites;

    private int _currentPage;

    private void Start()
    {
        _currentPage = 0;
        UpdatePage();
    }

    public void NextPage()
    {
        if (_currentPage < pageSprites.Length - 1)
        {
            _currentPage++;
            UpdatePage();
        }
    }

    public void PrevPage()
    {
        if (_currentPage > 0)
        {
            _currentPage--;
            UpdatePage();
        }
    }

    private void UpdatePage()
    {
        if (pageSprites == null || pageSprites.Length == 0) return;
        pageDisplay.sprite = pageSprites[_currentPage];
        if (backButton != null)
            backButton.interactable = (_currentPage > 0);

        if (nextButton != null)
            nextButton.interactable = (_currentPage < pageSprites.Length - 1);
    }
}