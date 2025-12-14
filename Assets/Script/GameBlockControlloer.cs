using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameBlockControlloer : MonoBehaviour
{
    private BoxCollider2D m_boxCollider;
    private Vector3 m_initializePosition;
    public LayerMask whatIsPlayer;
    public bool canBreak = false;

    private void Awake(){
        this.m_boxCollider = this.GetComponent<BoxCollider2D>();
        this.m_initializePosition = this.transform.position;
    }

    private void OnCollisionEnter2D(Collision2D collision){
        if(this.canBreak == false)return;
        
        if(collision.gameObject.tag == "Player"){
            Vector2 pos = this.transform.position;
            float bottomY = pos.y - (this.m_boxCollider.size.y * 0.5f) * this.transform.lossyScale.y;
            Vector2 blockBottom = new Vector2(pos.x,bottomY);
            Vector2 bottomCollisionArea = new Vector2(this.m_boxCollider.size.x * this.transform.lossyScale.x * 0.45f,0.1f);
            Collider2D colPlayer = Physics2D.OverlapArea(blockBottom + bottomCollisionArea,blockBottom - bottomCollisionArea,this.whatIsPlayer);

            if(colPlayer){
                this.gameObject.SetActive(false);
            }
        }
    }

    public void Initialize(){
        this.transform.position = this.m_initializePosition;
        this.gameObject.SetActive(true);
    }
}
