using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerOne : MonoBehaviour
{
    private Vector2 targetPos = new Vector2(1,1);
    private Rigidbody2D rbody;
    private BoxCollider2D collider;
    private Animator animator;

    public float smooth = 6f;
    public float restTime = 1f;
    public float restIimer = 0;

    // Start is called before the first frame update
    void Awake()
    {
        rbody = GetComponent<Rigidbody2D>();
        collider = GetComponent<BoxCollider2D>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (GamingManager.Instance.food<1)
        {
            this.enabled = false;
            GamingManager.Instance.deadText.enabled = true;
        }
        rbody.MovePosition(Vector2.Lerp(transform.position, targetPos, smooth * Time.deltaTime));
        restIimer += Time.deltaTime;
        if (restIimer < restTime) return;
        float h = Input.GetAxisRaw("Horizontal"); 
        float v = Input.GetAxisRaw("Vertical");
        if (h > 0)
        {
            v = 0;
        }
        
        if (h != 0 || v != 0)
        {
            GamingManager.Instance.foodReduce(1);
            collider.enabled = false;
            RaycastHit2D hit= Physics2D.Linecast(targetPos,targetPos+new Vector2(h,v));
            collider.enabled = true;


            if (hit.transform == null)
            {
                targetPos += new Vector2(h, v);
            }
            else
            {
                switch (hit.collider.tag)
                {
                    case"outwall":break;
                    case "wall":
                        animator.SetTrigger("Player_Attack");
                        hit.collider.SendMessage("TakeDamage");
                        break;
                    case "food":
                        GamingManager.Instance.foodAdd(10);
                        targetPos += new Vector2(h, v);
                        Destroy(hit.transform.gameObject);
                        break;
                    case "suda":
                        GamingManager.Instance.foodAdd(20);
                        targetPos += new Vector2(h, v);
                        Destroy(hit.transform.gameObject);
                        break;
                    case"exit":
                        isEnd();
                        break;
                }
            }
            restIimer = 0;
            GamingManager.Instance.OnPlayerMove();
            
        }
    }

    public void TakeDamage(int foodloss)
    {
        animator.SetTrigger("Player_Hurt");
        GamingManager.Instance.foodReduce(20);
    }

    public void isEnd()
    {
        this.enabled = false;
        GamingManager.Instance.level += 3;
        SceneManager.LoadScene("SampleScene");
    }
}
