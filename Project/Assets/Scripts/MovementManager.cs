using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementManager : MonoBehaviour
{
	public enum type {platformer, topDown};
	public type moveType;
	public float moveSpeed, jumpHeight;
	public bool onGround = false;
    private Animator animator;
    private Vector2 direction;


    // Start is called before the first frame update
    void Start()
    {

        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        /*if (!GameManager.S.DisabledMovement()) {
            if (Input.GetAxisRaw("Horizontal") > 0)
            {
                transform.Translate(Vector2.right * Time.deltaTime * moveSpeed);
                animator.SetFloat("x", 1);
                animator.SetFloat("y", 1);

            }
            else if (Input.GetAxisRaw("Horizontal") < 0)
            {
                transform.Translate(Vector2.left * Time.deltaTime * moveSpeed);

            }
            if (moveType == type.platformer && Input.GetAxisRaw("Vertical") > 0 && onGround)
            {
                GetComponent<Rigidbody2D>().AddForce(Vector2.up * jumpHeight, ForceMode2D.Impulse);
                
            }
            if (moveType == type.topDown && Input.GetAxisRaw("Vertical") > 0)
            {
                transform.Translate(Vector2.up * Time.deltaTime * moveSpeed);
             
            }
            else if (moveType == type.topDown && Input.GetAxisRaw("Vertical") < 0)
            {
                transform.Translate(Vector2.down * Time.deltaTime * moveSpeed);
           
            }
        }*/
        GetInput();
        Move();
    }
    public void Move()
    {
        transform.Translate(direction*moveSpeed*Time.deltaTime);
        if (this.GetComponent<PlayerLivingRoomBehavior>().vactime == true && (direction.x != 0 || direction.y != 0))
        {
            animator.SetLayerWeight(1,1);
            AnimateMovement(direction);
        } else {
            //animator.SetLayerWeight(1,0);
            AnimateMovement(direction);
        }
        if (this.GetComponent<PlayerLivingRoomBehavior>().vactime == false && (direction.x != 0 || direction.y != 0))
        {
            animator.SetLayerWeight(2,1);
            AnimateMovement(direction);
        } else {
            animator.SetLayerWeight(2,0);
            AnimateMovement(direction);
        }
    }
    private void GetInput()
    {
        direction = Vector2.zero;

        if (Input.GetKey(KeyCode.W))
        {
            direction += Vector2.up;
        }
        if (Input.GetKey(KeyCode.A))
        {
            direction += Vector2.left;
        }
        if (Input.GetKey(KeyCode.S))
        {
            direction += Vector2.down;
        }
        if (Input.GetKey(KeyCode.D))
        {
            direction += Vector2.right;
        }
    }
    public void AnimateMovement(Vector2 direction)
    {
        animator.SetFloat("x",direction.x);
        animator.SetFloat("y",direction.y);
    }
    void OnCollisionEnter2D (Collision2D obj)
    {
    	if (obj.gameObject.tag == "ground")
    	{
    		onGround = true;
    	}
    }
    void OnCollisionExit2D (Collision2D obj)
    {
    	if (obj.gameObject.tag == "ground")
    	{
    		onGround = false;
    	}
    }
}
