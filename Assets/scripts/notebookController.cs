using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class NotebookPageData
{
    public Sprite unlockedSprite;
    public Sprite lockedSprite;
    public int unlockCost = 5;
    [Tooltip("Если true, страница открыта изначально")]
    public bool isUnlockedByDefault = false;
}

public class notebookController : MonoBehaviour
{
    [Header("UI Elements")]
    [SerializeField] private Image pageDisplay;
    [SerializeField] private Button prevButton;
    [SerializeField] private Button nextButton;
    [SerializeField] private Button unlockButton;

    [Header("Pages Configuration")]
    [SerializeField] private NotebookPageData[] pages;

    // Статическое хранилище разблокированных индексов страниц (живет между сценами)
    private static HashSet<int> _unlockedPageIndices = new HashSet<int>();
    private static bool _isInitialized = false;

    private int _currentPage = 0;

    private void Awake()
    {
        if (!_isInitialized)
        {
            for (int i = 0; i < pages.Length; i++)
            {
                if (pages[i].isUnlockedByDefault)
                {
                    _unlockedPageIndices.Add(i);
                }
            }
            _isInitialized = true;
        }
    }

    private void Start()
    {
        if (unlockButton != null)
        {
            unlockButton.onClick.AddListener(TryUnlockCurrentPage);
        }

        UpdatePageUI();
    }

    public void NextPage()
    {
        if (_currentPage < pages.Length - 1)
        {
            _currentPage++;
            UpdatePageUI();
        }
    }

    public void PrevPage()
    {
        if (_currentPage > 0)
        {
            _currentPage--;
            UpdatePageUI();
        }
    }

    public void TryUnlockCurrentPage()
    {
        if (pages == null || pages.Length == 0) return;

        bool isUnlocked = _unlockedPageIndices.Contains(_currentPage);
        NotebookPageData page = pages[_currentPage];

        if (!isUnlocked && thingsOnClick.points >= page.unlockCost)
        {
            thingsOnClick.points -= page.unlockCost;

            _unlockedPageIndices.Add(_currentPage);

            UpdatePageUI();
        }
    }

    private void UpdatePageUI()
    {
        if (pages == null || pages.Length == 0) return;

        bool isUnlocked = _unlockedPageIndices.Contains(_currentPage);
        NotebookPageData page = pages[_currentPage];

        if (pageDisplay != null)
        {
            pageDisplay.sprite = isUnlocked ? page.unlockedSprite : page.lockedSprite;
        }

        if (unlockButton != null)
        {
            unlockButton.gameObject.SetActive(!isUnlocked);
        }

        if (prevButton != null)
            prevButton.interactable = (_currentPage > 0);

        if (nextButton != null)
            nextButton.interactable = (_currentPage < pages.Length - 1);
    }

    private void OnDestroy()
    {
        if (unlockButton != null)
        {
            unlockButton.onClick.RemoveListener(TryUnlockCurrentPage);
        }
    }
}