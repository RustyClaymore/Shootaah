using UnityEngine;
using UnityEngine.UI;
using Pixelplacement;
using TMPro;
using UnityEngine.SceneManagement;

public class UIMainMenuManager : MonoBehaviour
{
    enum MenuScreens
    {
        Main,
        LevelSelection,
        UpgradesShop
    }

    public PlayerDataSO playerData;

    public GameObject rightButton;
    public GameObject leftButton;

    public RectTransform logo;

    public TextMeshProUGUI rupeeAmount;

    [Header("Max Health Upgrades")]
    public TextMeshProUGUI currentMaxHealth;
    public TextMeshProUGUI nextMaxHealth;
    public TextMeshProUGUI maxHealthUpgradeCost;
    public Button maxHealthUpgradeButton;

    [Header("Regen Rate Upgrades")]
    public TextMeshProUGUI currentRegenRate;
    public TextMeshProUGUI nextRegenRate;
    public TextMeshProUGUI regenRateUpgradeCost;
    public Button regenRateUpgradeButton;

    [Header("Regen Cooldown Upgrades")]
    public TextMeshProUGUI currentRegenCooldown;
    public TextMeshProUGUI nextRegenCooldown;
    public TextMeshProUGUI regenCooldownUpgradeCost;
    public Button regenCooldownUpgradeButton;

    [Header("Speed Upgrades")]
    public TextMeshProUGUI currentSpeed;
    public TextMeshProUGUI nextSpeed;
    public TextMeshProUGUI speedUpgradeCost;
    public Button speedUpgradeButton;

    [Header("Aim Distance Upgrades")]
    public TextMeshProUGUI currentAimDistance;
    public TextMeshProUGUI nextAimDistance;
    public TextMeshProUGUI aimDistanceUpgradeCost;
    public Button aimDistanceUpgradeButton;

    private MenuScreens currentState;

    void Start()
    {
        currentState = MenuScreens.Main;
        leftButton.SetActive(false);
        rightButton.SetActive(false);
        
        UpdateUpgradeValues();
    }

    // Update is called once per frame
    void Update()
    {
        logo.Rotate(Vector3.forward * 20 * Time.deltaTime);

        rupeeAmount.text = Diamond.GetCurrentDiamondAmount().ToString();

        // For debug only
        if (Input.GetKeyDown(KeyCode.B))
        {
            PlayerPrefs.SetInt(Diamond.DiamondType, PlayerPrefs.GetInt(Diamond.DiamondType) + 500);
        }
        if (Input.GetKeyDown(KeyCode.U))
        {
            PlayerUpgrades.ResetPlayerUpgrades();
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            Diamond.ResetDiamondAmount();
        }

        UpdateUpgradeButtonStates(PlayerUpgrades.MaxHealth, playerData.maxHealth, maxHealthUpgradeButton);
        UpdateUpgradeButtonStates(PlayerUpgrades.HealthRegen, playerData.healthRegen, regenRateUpgradeButton);
        UpdateUpgradeButtonStates(PlayerUpgrades.RegenCooldown, playerData.regenCooldown, regenCooldownUpgradeButton);
        UpdateUpgradeButtonStates(PlayerUpgrades.Speed, playerData.speed, speedUpgradeButton);
        UpdateUpgradeButtonStates(PlayerUpgrades.AimDistance, playerData.aimDistance, aimDistanceUpgradeButton);
    }

    #region camera rotation
    public void RotateCameraToLevelSelection()
    {
        currentState = MenuScreens.LevelSelection;
        Tween.Rotation(Camera.main.transform, new Vector3(0, 90, 0), 1, 0, Tween.EaseInOut, Tween.LoopType.None, () => ActivateLeftButton());
    }

    public void RotateCameraToUpgradeScreen()
    {
        currentState = MenuScreens.UpgradesShop;
        Tween.Rotation(Camera.main.transform, new Vector3(0, -90, 0), 1, 0, Tween.EaseInOut, Tween.LoopType.None, () => ActivateRightButton());
    }

    public void RotateCameraToMainMenu()
    {
        currentState = MenuScreens.Main;
        Tween.Rotation(Camera.main.transform, new Vector3(0, 0, 0), 1, 0, Tween.EaseInOut, Tween.LoopType.None, () => DeactivateRightLeftButton());
    }
    #endregion

    #region upgrades
    public void UpgradeMaxHealth()
    {
        if (CanUpgrade(PlayerUpgrades.MaxHealth, playerData.maxHealth))
        {
            PlayerUpgrades.Upgrade(PlayerUpgrades.MaxHealth, playerData.maxHealth);
            UpdateUpgrade(PlayerUpgrades.MaxHealth, playerData.maxHealth, currentMaxHealth, nextMaxHealth, maxHealthUpgradeCost, maxHealthUpgradeButton);
        }
    }

    public void UpgradeHealthRegenRate()
    {
        if (CanUpgrade(PlayerUpgrades.HealthRegen, playerData.healthRegen))
        {
            PlayerUpgrades.Upgrade(PlayerUpgrades.HealthRegen, playerData.healthRegen);
            UpdateUpgrade(PlayerUpgrades.HealthRegen, playerData.healthRegen, currentRegenRate, nextRegenRate, regenRateUpgradeCost, regenRateUpgradeButton);
        }
    }

    public void UpgradeHealthRegenCooldown()
    {
        if (CanUpgrade(PlayerUpgrades.RegenCooldown, playerData.regenCooldown))
        {
            PlayerUpgrades.Upgrade(PlayerUpgrades.RegenCooldown, playerData.regenCooldown);
            UpdateUpgrade(PlayerUpgrades.RegenCooldown, playerData.regenCooldown, currentRegenCooldown, nextRegenCooldown, regenCooldownUpgradeCost, regenCooldownUpgradeButton);
        }
    }

    public void UpgradeSpeed()
    {
        if (CanUpgrade(PlayerUpgrades.Speed, playerData.speed))
        {
            PlayerUpgrades.Upgrade(PlayerUpgrades.Speed, playerData.speed);
            UpdateUpgrade(PlayerUpgrades.Speed, playerData.speed, currentMaxHealth, nextSpeed, speedUpgradeCost, speedUpgradeButton);
        }
    }

    public void UpgradeAimDistance()
    {
        if (CanUpgrade(PlayerUpgrades.AimDistance, playerData.aimDistance))
        {
            PlayerUpgrades.Upgrade(PlayerUpgrades.AimDistance, playerData.aimDistance);
            UpdateUpgrade(PlayerUpgrades.AimDistance, playerData.aimDistance, currentAimDistance, nextAimDistance, aimDistanceUpgradeCost, aimDistanceUpgradeButton);
        }
    }
    #endregion

    #region level selection
    public void StartLevel(int level)
    {
        SceneManager.LoadScene(level);
    }
    #endregion

    private void UpdateUpgradeValues()
    {
        UpdateUpgrade(PlayerUpgrades.MaxHealth, playerData.maxHealth, currentMaxHealth, nextMaxHealth, maxHealthUpgradeCost, maxHealthUpgradeButton);
        UpdateUpgrade(PlayerUpgrades.HealthRegen, playerData.healthRegen, currentRegenRate, nextRegenRate, regenRateUpgradeCost, regenRateUpgradeButton);
        UpdateUpgrade(PlayerUpgrades.RegenCooldown, playerData.regenCooldown, currentRegenCooldown, nextRegenCooldown, regenCooldownUpgradeCost, regenCooldownUpgradeButton);
        UpdateUpgrade(PlayerUpgrades.Speed, playerData.speed, currentSpeed, nextSpeed, speedUpgradeCost, speedUpgradeButton);
        UpdateUpgrade(PlayerUpgrades.AimDistance, playerData.aimDistance, currentAimDistance, nextAimDistance, aimDistanceUpgradeCost, aimDistanceUpgradeButton);
    }

    private void DeactivateRightLeftButton()
    {
        leftButton.SetActive(false);
        rightButton.SetActive(false);
    }

    private void ActivateRightButton()
    {
        leftButton.SetActive(false);
        rightButton.SetActive(true);
    }

    private void ActivateLeftButton()
    {
        leftButton.SetActive(true);
        rightButton.SetActive(false);
    }

    private bool CanUpgrade(string upgrade, Upgrade upgradeData)
    {
        int currentUpgradeLevel = PlayerUpgrades.GetUpgradeLevel(upgrade);
        if (currentUpgradeLevel == upgradeData.levelUpgradeCosts.Length)
        {
            return false;
        }
        int upgradeCost = upgradeData.levelUpgradeCosts[currentUpgradeLevel];

        return Diamond.GetCurrentDiamondAmount() >= upgradeCost;
    }

    private void UpdateUpgradeButtonStates(string upgrade, Upgrade upgradeData, Button upgradeButton)
    {
        if (!CanUpgrade(upgrade, upgradeData))
        {
            upgradeButton.interactable = false;
            return;
        }
        else
        {
            upgradeButton.interactable = true;
        }
    }

    private void UpdateUpgrade(
        string upgrade,
        Upgrade upgradeData,
        TextMeshProUGUI currentUpgradeText,
        TextMeshProUGUI nextUpgradeText,
        TextMeshProUGUI upgradeCost,
        Button upgradeButton
    )
    {
        int currentUpgradeLevel = PlayerUpgrades.GetUpgradeLevel(upgrade);
        float[] upgradeLevels = upgradeData.levelValues;

        currentUpgradeText.text = upgradeLevels[currentUpgradeLevel].ToString();

        if (currentUpgradeLevel < upgradeLevels.Length - 1)
        {
            nextUpgradeText.text = upgradeLevels[currentUpgradeLevel + 1].ToString();
            upgradeCost.text = upgradeData.levelUpgradeCosts[currentUpgradeLevel].ToString();
        }
        else
        {
            nextUpgradeText.text = "Max";
            upgradeCost.gameObject.SetActive(false);
            upgradeButton.interactable = false;
        }
    }
}
