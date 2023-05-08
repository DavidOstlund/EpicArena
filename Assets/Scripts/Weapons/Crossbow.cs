using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crossbow : Weapon
{
    public Crossbow() {
        this.weaponName = "crossbow";
        this.weaponIndex = 2;
        this.weaponDamage = 15f;
        this.reloadSpeed = 0.5f;
        this.isRanged = true;
    }

    public override void MakeAttack(Vector2 clickPosition, Vector2 spawnPosition) {
        Vector2 attackDirection = (clickPosition - (Vector2)spawnPosition).normalized;
        Transform projectileTransform = WeaponManager.Instance.InstantiateProjectile(spawnPosition);
        projectileTransform.GetComponent<Projectile>().Setup(attackDirection, weaponDamage);
    }
}
