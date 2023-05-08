using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponManager : MonoBehaviour
{
    public static WeaponManager Instance { get; private set; }
    [SerializeField] private Sprite[] weaponSprites;
    private Dictionary<string, Weapon> allWeapons = new Dictionary<string, Weapon>();
    public Weapon currentWeapon { get; private set; }
    [SerializeField] private GameObject currentWeaponUI;
    [SerializeField] private Transform projectile;

    // Start is called before the first frame update
    void Start()
    {
        if (Instance != null)
        {
            Debug.LogWarning("Found more than one Weapon Manager in the scene");
        }
        Instance = this;
        allWeapons.Add("Dagger", new Dagger());
        allWeapons.Add("Bow", new Bow());
    }

    public void ChooseWeapon(string weaponKey)
    {
        currentWeapon = allWeapons[weaponKey];

        Image currentWeaponImage = currentWeaponUI.GetComponent<Image>();
        currentWeaponImage.sprite = weaponSprites[currentWeapon.weaponIndex];

        GameManager.Instance.thePlayer.equippedWeapon = currentWeapon;
    }    
    
    public Transform InstantiateProjectile(Vector2 spawnPosition)
    {
        return Instantiate(projectile, spawnPosition, Quaternion.identity);
    }

}
