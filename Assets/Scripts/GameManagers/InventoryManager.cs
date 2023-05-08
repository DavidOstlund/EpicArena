using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{        
    public static InventoryManager Instance { get; private set; }
    [SerializeField] private GameObject inventory;
    [SerializeField] private GameObject[] weapons;
    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogWarning("Found more than one Dialogue Manager in the scene");
        }
        Instance = this;
    }

    public void OpenInventory()
    {
        inventory.gameObject.SetActive(true);
        StartCoroutine(InputManager.Instance.SelectButton(weapons[0]));
        InputManager.Instance.SwitchToActionMap("Dialogue");
    }

    public void CloseInventory(string weaponKey)
    {
        WeaponManager.Instance.ChooseWeapon(weaponKey);

        inventory.gameObject.SetActive(false);
        InputManager.Instance.SwitchToActionMap("Player");
    }
}
