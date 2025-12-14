using System.Collections;   
using UnityEngine;

public class GameCameraControlller : MonoBehaviour
{
    public Transform target;
    public Transform stopPosition;
    private Camera m_camera;
    private Vector3 m_initialPosition;

    private void Awake(){
        this.m_camera = this.GetComponent<Camera>();
        this.m_initialPosition = this.transform.position;
    }

    private void LateUpdate(){
        Vector3 center = this.m_camera.ViewportToWorldPoint(Vector2.one * 0.5f);

        Vector3 right = this.m_camera.ViewportToWorldPoint(Vector2.right);

        if(right.x >= this.stopPosition.position.x){
            Vector3 def = right - center;
            Vector3 tmpPos = this.m_camera.transform.position;
            tmpPos.x = this.stopPosition.position.x - def.x;
            this.m_camera.transform.position = tmpPos;
            return;
        }

        if(center.x < this.target.position.x){
            Vector3 pos = this.m_camera.transform.position;

            if(Mathf.Abs(pos.x - this.target.position.x) > 0){
                Vector3 tmpPos = this.m_camera.transform.position;
                tmpPos.x = this.target.position.x;
                this.m_camera.transform.position = tmpPos;
            }
        }
    }

    public void Initialize(){
        this.transform.position = this.m_initialPosition;

    }
}
