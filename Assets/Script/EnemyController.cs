using System.Collections;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private BoxCollider2D m_boxCollider;
    private Vector3 m_initialPosition;
    public float jumpPower = 0.5f;
    public LayerMask whatIsPlayer;

    private void Awake(){
        this.m_boxCollider = this.GetComponent<BoxCollider2D>();
        this.m_initialPosition = this.transform.position;
    }

    private void OnCollisionEnter2D(Collision2D collision){
        if(collision.gameObject.tag == "Player"){
            Vector2 pos = this.transform.position;

            float enemyCenterX = pos.x + m_boxCollider.offset.x * this.transform.lossyScale.x;

            float enemyTopY = pos.y + (this.m_boxCollider.offset.y + this.m_boxCollider.size.y) * this.transform.lossyScale.y;

            Vector2 enemyTop = new Vector2(enemyCenterX,enemyTopY);

            Vector2 collisionArea = new Vector2(this.m_boxCollider.size.x * this.transform.lossyScale.x * 0.45f,0.1f);

            Collider2D collisionPlayer = Physics2D.OverlapArea(enemyTop + collisionArea,enemyTop - collisionArea,this.whatIsPlayer);

            if(collisionPlayer){
                PlayerController playerController = collisionPlayer.GetComponent<PlayerController>();

                if(playerController){
                    playerController.enemyJumpForce(this.jumpPower);
                }
                this.gameObject.SetActive(false);
            }

            else{
                GameManager gameManager = (GameManager)GameObject.FindObjectOfType(typeof(GameManager));
                if(gameManager)
                    gameManager.GameInitialize();

                if(GameCoinController.m_totalCoinsCollected > 1){
                    GameCoinController.m_totalCoinsCollected = 0;
                }    
            }
        }
    }

    public void Initialize(){
        this.transform.position = this.m_initialPosition;
        this.gameObject.SetActive(true);
    }
}
