using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bow : Weapon
{
    public Bow() {
        this.weaponName = "bow";
        this.weaponIndex = 1;
        this.weaponDamage = 11f;
        this.reloadSpeed = 0.3f;
        this.isRanged = true;
    }

    public override void MakeAttack(Vector2 clickPosition, Vector2 spawnPosition) {
        Vector2 attackDirection = (clickPosition - (Vector2) spawnPosition).normalized;
        Transform projectileTransform = WeaponManager.Instance.InstantiateProjectile(spawnPosition);
        projectileTransform.GetComponent<Projectile>().Setup(attackDirection, weaponDamage);
    }

}
