using UnityEngine;

public class MyBall : MonoBehaviour
{
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log(collision.gameObject.name + "와 충돌");

        if (collision.gameObject.tag == "Ground")
        {
            Debug.Log("충돌");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("트리거 입장");
    }
    private void OnTriggerExit(Collider other)
    {
        Debug.Log("트리거 퇴장");
    }
}
