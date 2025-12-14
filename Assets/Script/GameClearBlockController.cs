using UnityEngine;

public class GameClearBlockController : MonoBehaviour
{
    public static GameClearBlockController Instance{get; private set;}
    public GameObject objectToActivate;
    public GameObject objectClearBotton;
    private BoxCollider2D m_boxCollider;
    public LayerMask whatIsPlayer;

    private bool playerHasInteracted = false;


    private void Awake(){
        if(Instance == null){
            Instance = this;
        }
        else{
            Destroy(gameObject);
        }
        if(objectToActivate != null){
            objectToActivate.SetActive(false);
        }
        m_boxCollider = GetComponent<BoxCollider2D>();
    }

    public void CheckCoinCount(){
        Debug.Log("Checking coin count. Current count:" + GameCoinController.TotalCoinsCollected);
        if(GameCoinController.TotalCoinsCollected == 5 && objectToActivate != null){
            Debug.Log("Coin count is 5. Activating object.");
            objectToActivate.SetActive(true);
        }
    }

    public void OnCollisionEnter2D(Collision2D collision){
        if(collision.gameObject.tag == "Player"){
            playerHasInteracted = true;
            ShowCanvas();
        }
    }

    private void ShowCanvas(){
        if(objectToActivate != null && playerHasInteracted){
            objectClearBotton.SetActive(true);
            Time.timeScale = 0.0f;
        }
    }
}