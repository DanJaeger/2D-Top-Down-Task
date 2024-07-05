using UnityEngine;

public class ShopSystem : MonoBehaviour
{
    [SerializeField] private GameObject _shopIntroPanel;
    [SerializeField] private GameObject _shopMenuPanel;
    [SerializeField] private GameObject _inventoryPanel;
                     
    [SerializeField] private GameObject _shopButton;
    [SerializeField] private GameObject _sellButton;
    [SerializeField] private GameObject _sellPriceText;

    private bool _haveIEnterBefore;
    public static bool IsPlayingIntro { get; set; }
    public static bool IsShopOpen {get; private set; }
    private void Start()
    {
        _shopIntroPanel.SetActive(false);
        _shopMenuPanel.SetActive(false);
        _sellPriceText.SetActive(false);

        _haveIEnterBefore = false;
        IsShopOpen = false;
    }
    public void OpenShop()
    {
        if (!_haveIEnterBefore)
        {
            _shopIntroPanel.SetActive(true);
            _haveIEnterBefore = true;
            IsPlayingIntro = true;
            IsShopOpen = true;

            SoundFXManager.instance.PlayInteractionSound();
        }
        else
        {
            if (_shopMenuPanel.activeSelf || _inventoryPanel.activeSelf)
            {
                _shopMenuPanel.SetActive(false);
                _inventoryPanel.SetActive(false);
                _shopButton.SetActive(false);
                _sellButton.SetActive(false);
                _sellPriceText.SetActive(false);
                IsShopOpen = false;
            }
            else
            {
                _shopMenuPanel.SetActive(true); 
                IsShopOpen = true;

                SoundFXManager.instance.PlayInteractionSound();
            }
        }
    }
    public void ChangeToInventory()
    {
        _shopMenuPanel.SetActive(false);
        _inventoryPanel.SetActive(true);
        _shopButton.SetActive(true);
        _sellButton.SetActive(true);
        _sellPriceText.SetActive(true);
    }
    public void ChangeToShop()
    {
        _inventoryPanel.SetActive(false);
        _shopButton.SetActive(false);
        _sellButton.SetActive(false);
        _sellPriceText.SetActive(false);
        _shopMenuPanel.SetActive(true);
    }
    public bool IsPanelActive()
    {
        if (_shopMenuPanel.activeInHierarchy || _inventoryPanel.activeInHierarchy || _shopIntroPanel.activeInHierarchy)
            return true;
        else
            return false;
    }
}
