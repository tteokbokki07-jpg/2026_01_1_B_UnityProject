using UnityEngine;

public class Fruit : MonoBehaviour
{
    public int fruitType;
    public bool hasMerged = false;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (hasMerged)
            return;

        Fruit otherFruit = collision.gameObject.GetComponent<Fruit>();
        if (otherFruit != null && !otherFruit.hasMerged && otherFruit.fruitType == fruitType)
        {
            hasMerged = true;
            otherFruit.hasMerged = true;
            Vector3 mergePosition = (transform.position + otherFruit.transform.position) / 2;  //두과일중간위치계산

            //게임매니저에서 머지 호출
            FruitGame gameManager = FindAnyObjectByType<FruitGame>();
            if (gameManager != null)
            {
                gameManager.MergeFruits(fruitType, mergePosition);
            }

            Destroy(otherFruit.gameObject);
            Destroy(gameObject);
        }
    }
}
