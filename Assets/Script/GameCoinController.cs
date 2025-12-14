using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameCoinController : MonoBehaviour
{
    private BoxCollider2D m_boxCollider;
    private Vector3 m_initialPosition;
    public static int m_totalCoinsCollected;

    public static int TotalCoinsCollected{
        get{return m_totalCoinsCollected;}
    }

    private void Awake(){
        this.m_boxCollider = this.GetComponent<BoxCollider2D>();
        this.m_initialPosition = this.transform.position;
    }

    private void OnTriggerEnter2D(Collider2D collision){
        if(collision.gameObject.tag == "Player"){
            m_totalCoinsCollected++;
            Debug.Log("Coin collected. Total coins:" + m_totalCoinsCollected);
            this.gameObject.SetActive(false);
            if(GameClearBlockController.Instance != null){
                GameClearBlockController.Instance.CheckCoinCount();
            }
            else{
                Debug.LogError("GameClearBlockController instance is null.");
            }           
        }
    }

    public void Initialize(){
        this.transform.position = this.m_initialPosition;
        this.gameObject.SetActive(true);
    }

}