using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal.Profiling.Memory.Experimental;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private Transform player;
    private Vector2 targetPosition;//目标位置
    private Rigidbody2D rbody;
    private BoxCollider2D collider;
    private Animator animator;

    public float smooth = 3;
    public int foodloss = 10;
    
    private void Awake()
    {
        rbody = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("player").transform;
        targetPosition = transform.position;
        GamingManager.Instance.enemyList.Add(this);
        collider = GetComponent<BoxCollider2D>();
        animator = GetComponent<Animator>();
    }
    private void Update()
    {
        rbody.MovePosition(Vector2.Lerp(transform.position,targetPosition,smooth *Time.deltaTime));
    }
    public void Move()
    {
        Vector2 selfSet = player.position - transform.position;
        if (selfSet.magnitude < 1.15f)
        {
            animator.SetTrigger("Enemy_Attack");
            player.SendMessage("TakeDamage",foodloss);
            
        }    
        else
        {
            float x = 0, y = 0;
            if (Mathf.Abs(selfSet.y) > Mathf.Abs(selfSet.x))
            {
                if (selfSet.y < 0)
                {
                    y = -1;
                }
                else
                {
                    y = 1;
                }
            }
            else
            {
                if (selfSet.x > 0)
                {
                    x = 1;
                }
                else
                {
                    x = -1;
                }
            }
            collider.enabled = false;
            RaycastHit2D hit= Physics2D.Linecast(targetPosition,targetPosition+new Vector2(x,y));
            collider.enabled = true;
            if (hit.transform == null)
            {
                targetPosition += new Vector2(x, y);
            }
            else
            {
                if (hit.collider.tag == "food" || hit.collider.tag == "suda")
                {
                    targetPosition += new Vector2(x, y);
                }
            }
        }
        
    }



}
 