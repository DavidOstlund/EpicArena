using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public class Enemy : Character
{
    private Rigidbody2D body;
    private Animator animator;

    [SerializeField] private float runSpeed = 8f;
    [SerializeField] private float minDistance = 2f;
    [SerializeField] private float enemyReach = 2f;
    [SerializeField] private float enemyDamage = 2f;
    [SerializeField] private float enemyAttackSpeed = 0.5f;

    private Vector2 oldPosition;
    private int stuckCount;

    private float timestampForNextAttack;


    [SerializeField] private int currentPathIndex;
    private List<Vector3> pathVectorList;

    private Transform target;

    [SerializeField] private HealthBar healthBar;

    protected override void Start()
    {
        base.Start();

        body = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        pathVectorList = null;
        currentPathIndex = 0;
        oldPosition = transform.position;

        timestampForNextAttack = Time.time;

        target = GameManager.instance.thePlayer.transform;

        // Set the initial path
        UpdatePath();
    }

    void Update()
    {
        if (target != null) {
            // If we have a target we check if we are close enough to attack it
            if (Vector3.Distance(transform.position, target.position) < enemyReach) {
                // If we are close enough we try to attack it
                if (Time.time >= timestampForNextAttack) {
                    animator.SetTrigger("enemyAttack");
                    target.GetComponent<Character>().TakeDamage(enemyDamage);
                    timestampForNextAttack = Time.time + enemyAttackSpeed;
                    body.velocity = new Vector3(0, 0, 0);
                    pathVectorList = null;
                }
            }
            else if (pathVectorList != null) {
                // If we are not close enough to attack we move towards it
                HandleMovement();
            }
            else {
                // If we have no path we update path
                UpdatePath();
            }
        }
        else {
            // If we have no target we update path
            UpdatePath();
        }
    }

    public void HandleMovement() {
        Vector3 currentPathTarget = pathVectorList[currentPathIndex];

        // Solves enemy getting stuck in corners by lerping it past it
        if (Vector2.Distance((Vector2) transform.position, oldPosition) < 0.0001) {
            stuckCount += 1;
        } else {
            stuckCount = 0;
        }
        if (stuckCount > 3) {
            StartCoroutine(LerpPosition((Vector2)currentPathTarget, Vector3.Distance(transform.position, currentPathTarget) /runSpeed));
                
            stuckCount = 0;
        }

        Debug.DrawLine(transform.position, currentPathTarget, Color.green);

        // If we are far enough from the tile we are moving towards, then we continue to move towards it.
        if (Vector3.Distance(transform.position, currentPathTarget) > minDistance) {
            Vector3 moveDir = (currentPathTarget - transform.position).normalized;

            body.velocity = new Vector2(moveDir.x * runSpeed, moveDir.y * runSpeed);

        } else {
            currentPathIndex++;
            // If the enemy is far away from the target it moves 7 tiles before retargeting.
            if (currentPathIndex >= pathVectorList.Count || currentPathIndex > 7) {
                UpdatePath();
            }
        }

        oldPosition = transform.position;
    }

    public Vector3 GetPosition() {
        return transform.position;
    }

    public override void TakeDamage(float damage) {
        Debug.Log("Enemy took " + damage + " damage!");
        animator.SetTrigger("enemyHit");
        base.ReduceCurrentHealth(damage);
        healthBar.SetHealth(base.GetCurrentHealth());

        if (base.GetCurrentHealth() <= 0) {
            GameManager.instance.HandleKilledEnemy(base.gameObject);
        }
    }

    private void UpdatePath() {

        // If we don't have a target
        if (target == null) {
            body.velocity = new Vector2(0, 0);
            return;
        }

        currentPathIndex = 0;
        pathVectorList = Pathfinding.Instance.FindPath(GetPosition(), target.position);
    }

    IEnumerator LerpPosition(Vector2 targetPosition, float duration) {
        float time = 0;
        Vector2 startPosition = transform.position;

        while (time < duration) {
            transform.position = Vector2.Lerp(startPosition, targetPosition, time / duration);
            time += Time.deltaTime;
            yield return null;
        }
        transform.position = targetPosition;
    }



    public void RemovePlayerFromTarget() {
        this.target = null;
    }


    private void OnCollisionEnter2D(Collision2D other) {
        if (other.gameObject.GetComponent<Enemy>()) {
            if (pathVectorList != null) {
                Vector3 currentPathTarget = pathVectorList[currentPathIndex];
                StartCoroutine(LerpPosition((Vector2)currentPathTarget, Vector3.Distance(transform.position, currentPathTarget) / runSpeed));
            }
        }
    }


    /*
    private void SelectTarget() {
        float closestDistance = float.MaxValue;
        
        GameObject[] PlayerGameObjectList = GameObject.FindGameObjectsWithTag("Player");
        targetList = new List<Transform>();
        foreach (GameObject OtherPlayerGameObject in PlayerGameObjectList) {
            targetList.Add(OtherPlayerGameObject.transform);
        }

        foreach (Transform target in targetList) {
            float targetDistance = Vector2.Distance(transform.position, target.position);
            if (targetDistance < closestDistance) {
                closestDistance = targetDistance;
                currentTarget = target;
            }
        }
    }
    */

}
