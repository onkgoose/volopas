using System;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class NotebookPageData
{
    public Sprite unlockedSprite; 
    public Sprite lockedSprite;   
    public int unlockCost = 5;    
    public bool isUnlocked = false; 
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

    private int _currentPage = 0;

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

        NotebookPageData page = pages[_currentPage];

        if (!page.isUnlocked && thingsOnClick.points >= page.unlockCost)
        {
            thingsOnClick.points -= page.unlockCost;

            page.isUnlocked = true;
            UpdatePageUI();
        }
    }

    private void UpdatePageUI()
    {
        if (pages == null || pages.Length == 0) return;

        NotebookPageData page = pages[_currentPage];

        // 1. Устанавливаем нужный спрайт в зависимости от статуса
        if (pageDisplay != null)
        {
            pageDisplay.sprite = page.isUnlocked ? page.unlockedSprite : page.lockedSprite;
        }

        // 2. Показываем кнопку разблокировки ТОЛЬКО если страница заблокирована
        if (unlockButton != null)
        {
            unlockButton.gameObject.SetActive(!page.isUnlocked);
        }

        // 3. Управление интерактивностью стрелок перелистывания
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