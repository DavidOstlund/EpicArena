using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : Character {
    // Variables regarding physics
    private Rigidbody2D body;
    private SpriteRenderer sprite;
    private Animator animator;

    // Variables regarding movement
    private float moveLimiter = 0.7f;
    [SerializeField] private float runSpeed;
    [SerializeField] private string playerName;

    // Variables regarding weapons and items
    [SerializeField] private List<GameObject> weaponsList;
    private List<bool> weaponsBoolList;
    private Weapon equippedWeapon = null;

    private float timestampForNextAction;

    private GameObject itemOnFloor;

    // UI elements
    [SerializeField] private HealthBar overworldHealthBar;
    [SerializeField] private AvailableWeapons availableWeapons;
    [SerializeField] private Text nameTextBox;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();

        nameTextBox.text = playerName;
        body = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();

        weaponsBoolList = new List<bool>();
        // Setting true for "startingWeapon" position
        weaponsBoolList.Add(true);
        // Setting false for all other weapon positions
        for (int i = 1; i < weaponsList.Count; i++) {
            weaponsBoolList.Add(false);
        }

        overworldHealthBar = GameObject.Find("OverworldHealthBar").GetComponent<HealthBar>();
        availableWeapons = GameObject.Find("AvailableWeapons").GetComponent<AvailableWeapons>();
        overworldHealthBar.SetMaxHealth(base.GetMaxHealth());

        //Setting "StartingWeapon" as first weapon
        SwitchWeapon(0);

        Debug.Log("Player max currenthealth:" + base.GetCurrentHealth());
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void Move(float horizontal, float vertical) {
        // Check for diagonal movement
        if (horizontal != 0 && vertical != 0) {
            // limit movement speed diagonally, so you move at 70% speed
            horizontal *= moveLimiter;
            vertical *= moveLimiter;
        }

        if (horizontal > 0) {
            sprite.flipX = false;
        }
        if (horizontal < 0) {
            sprite.flipX = true;
        }

        animator.SetFloat("xInput", horizontal);
        animator.SetFloat("yInput", vertical);
        
        animator.SetBool("isMoving", true);

        body.velocity = new Vector2(horizontal * runSpeed, vertical * runSpeed);
    }

    public void StopMoving() {
        animator.SetBool("isMoving", false);
        body.velocity = new Vector2(0, 0);
    }

    public void GrabObject() {
        if (itemOnFloor == null) {
            Debug.Log("Trying to grab an item when noone exist");
            return;
        }
        if (itemOnFloor.tag == "Weapon") {
            Weapon weaponOnFloor = itemOnFloor.GetComponent<Weapon>();

            if (weaponsBoolList[weaponOnFloor.weaponIndex] == false) {
                weaponsBoolList[weaponOnFloor.weaponIndex] = true;
                SwitchWeapon(weaponOnFloor.weaponIndex);
                availableWeapons.SetActive(weaponOnFloor.weaponIndex);
                Destroy(itemOnFloor);
                itemOnFloor = null;
            } else {
                // Already had the weapon which we tried to pick up.
            }
        }
    }

    public void CycleEquippedWeapon(int cycleDirection) {
        int newWeaponIndex = equippedWeapon.weaponIndex + cycleDirection;

        if (newWeaponIndex < 0) {
            newWeaponIndex += weaponsList.Count;
        } else if (newWeaponIndex == weaponsList.Count) {
            newWeaponIndex = 0;
        }

        if (weaponsBoolList[newWeaponIndex] == false) {
            if (cycleDirection < 0) {
                CycleEquippedWeapon(cycleDirection - 1);
            } else {
                CycleEquippedWeapon(cycleDirection + 1);
            }
        } else {
            SwitchWeapon(newWeaponIndex);
        }
    }

    private void SwitchWeapon(int weaponIndex) {
        equippedWeapon = weaponsList[weaponIndex].GetComponent<Weapon>();
        availableWeapons.ChooseWeapon(weaponIndex);
        if (equippedWeapon.isRanged == true) {
            animator.SetBool("isRanged", true);
        } else {
            animator.SetBool("isRanged", false);
        }
    }

    public void TryToAttack(Vector2 targetPosition) {
        if (Time.time >= timestampForNextAction) {
            animator.SetTrigger("Attack");
            this.equippedWeapon.MakeAttack(targetPosition, transform.position);
            timestampForNextAction = Time.time + equippedWeapon.reloadSpeed;
           
        } else {
            Debug.Log("Trying to fire too fast");
        }
    }

    public override void TakeDamage(float damage) {
        Debug.Log("Player took " + damage + " damage!");
        animator.SetTrigger("Hit");
        base.ReduceCurrentHealth(damage);
        overworldHealthBar.SetHealth(base.GetCurrentHealth());
        if (base.GetCurrentHealth() <= 0) {
            GameManager.instance.HandleKilledPlayer();
            GameManager.instance.GameOver();
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision) {

        if (collision.gameObject.tag == "Weapon") {
            itemOnFloor = collision.gameObject;
            Debug.Log("Inside Weapon OnTriggerEnter2D for player");
        }        
    }

    private void OnTriggerExit2D(Collider2D collision) {

        if (collision.gameObject.tag == "Weapon") {
            itemOnFloor = null;
            Debug.Log("Exiting Weapon trigger for player");
        }
    }
}
