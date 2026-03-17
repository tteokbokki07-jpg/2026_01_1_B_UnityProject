using UnityEngine;

public class MyCharacter : MonoBehaviour
{
    public int Health = 100;   //정수 표현
    public float Timer = 1.0f;  //실수 표현

    void Start()
    {
        Health = Health + 100;
    }

    void Update()
    {
        Timer = Timer - Time.deltaTime;

        if (Timer <= 0 )
        {
            Timer = 1.0f;
            Health = Health - 20;
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Health = Health + 2;
        }

        if (Health <= 0)
        {
            Destroy(this.gameObject);
        }
    }
}
