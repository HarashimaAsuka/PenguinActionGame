using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartBlockController : MonoBehaviour
{
    private BoxCollider2D m_boxCollider;
    public LayerMask whatIsPlayer;

    private void Awake(){
        this.m_boxCollider = this.GetComponent<BoxCollider2D>();

        if(Time.timeScale == 0f){
            Time.timeScale = 1f;
        }

        if(GameCoinController.m_totalCoinsCollected > 1)
            GameCoinController.m_totalCoinsCollected = 0;
    }

    private void OnCollisionEnter2D(Collision2D collision){
        if(collision.gameObject.tag == "Player"){
            Vector2 pos = this.transform.position;
            float bottomY = pos.y - (this.m_boxCollider.size.y * 0.5f) * this.transform.lossyScale.y;
            Vector2 blockBottom = new Vector2(pos.x,bottomY);
            Vector2 bottomCollisionArea = new Vector2(this.m_boxCollider.size.x * this.transform.lossyScale.x * 0.45f,0.1f);
            Collider2D colPlayer = Physics2D.OverlapArea(blockBottom + bottomCollisionArea,blockBottom - bottomCollisionArea,this.whatIsPlayer);

            if(colPlayer){
                SceneManager.LoadScene("Game");
            }
        }
    }
}
