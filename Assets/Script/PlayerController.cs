using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator),typeof(Rigidbody2D),typeof(BoxCollider2D))]

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D m_rigidbody;
    private Animator m_animator;
    private BoxCollider2D m_boxCollider;
    private bool m_isGround = false;
    private Vector3 m_initialPosition;

    public float moveSpeed = 5.0f;
    public float jumpPower = 400.0f;
    public LayerMask whatIsGround;

    private void Awake(){
        this.m_rigidbody = this.GetComponent<Rigidbody2D>();
        this.m_animator = this.GetComponent<Animator>();
        this.m_boxCollider = this.GetComponent<BoxCollider2D>();
        this.m_initialPosition = this.transform.position; 
    }
  
    private void Update(){
        float x = Input.GetAxis("Horizontal");
        if(Input.GetKey(KeyCode.P)){
            x = x * 2;
        }

        bool isJump = Input.GetButtonDown("Jump");

        float currentJumpPower = this.jumpPower;
            if(Input.GetKey(KeyCode.J)){
                currentJumpPower = currentJumpPower * 2;
            }
        
        this.Move(x,isJump,currentJumpPower);
    }

    void Move(float speedX,bool isJump,float currentJumpPower){
        if(Mathf.Abs(speedX) > 0){
            Vector3 tmpEuler = this.transform.eulerAngles;

// このif文はキャラクターの向きを進行方向に変える処理
            if(Mathf.Sign(speedX) > 0){
                tmpEuler.y = 0;
            }

            else{
                tmpEuler.y = 180;
            }
                
            this.transform.eulerAngles = tmpEuler;
        }

            this.m_rigidbody.velocity = new Vector2(speedX * this.moveSpeed,this.m_rigidbody.velocity.y);

            this.m_animator.SetFloat("InputX", Mathf.Abs(speedX));

            if(this.m_isGround && isJump){
                this.m_rigidbody.AddForce(Vector2.up * currentJumpPower);
            }
            this.m_animator.SetFloat("Vertical",this.m_rigidbody.velocity.y);
    }

    private void FixedUpdate(){
        Vector2 pos = this.transform.position;

        float characterBottom = pos.y +
        (this.m_boxCollider.offset.y -(this.m_boxCollider.size.y*0.5f))*this.transform.lossyScale.y;

        Vector2 groundCheck = new Vector2(pos.x,characterBottom);

        Vector2 groundArea = new Vector2(this.m_boxCollider.size.x*0.49f,0.05f);

        this.m_isGround = Physics2D.OverlapArea(groundCheck + groundArea,groundCheck - groundArea,this.whatIsGround);

        this.m_animator.SetBool("IsGround",this.m_isGround);
    }

    public void enemyJumpForce(float power){
        Vector3 tmpVelocity = this.m_rigidbody.velocity;
        tmpVelocity.y = 0;
        this.m_rigidbody.velocity = tmpVelocity;

        if(Input.GetButton("Jump")){
            this.m_rigidbody.AddForce(new Vector2(0,this.jumpPower));
        }

        else{
            this.m_rigidbody.AddForce(new Vector2(0,this.jumpPower * power));
        }
    }

    public void Initialize(){
        this.transform.position = this.m_initialPosition;
    }
}

