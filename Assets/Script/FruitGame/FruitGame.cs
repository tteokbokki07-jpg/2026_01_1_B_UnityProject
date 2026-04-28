using UnityEngine;

public class FruitGame : MonoBehaviour
{
    public GameObject[] fruitPrefebs;
    public float[] fruitSized = { 0.5f, 0.75f, 1f, 1.25f, 1.5f };
    public GameObject currentFruit;
    public int cuttrentFruitType;

    public float fruitStartHeight = 6f;
    public float gameWidth = 5.0f;
    public bool isGameOver = false;
    public Camera mainCamera;

    public float fruitTimer;

    void Start()
    {
        mainCamera = Camera.main;
        SpawnNewFruit();
        fruitTimer = -3.0f;
    }

    void Update()
    {
        if (isGameOver) return;

        if (fruitTimer >= 0)
        {
            fruitTimer -= Time.deltaTime;

        }
        if (fruitTimer < 0 && fruitTimer > -2)
        {
            SpawnNewFruit();
            fruitTimer = -3.0f;
        }

        if (currentFruit != null)
        {
            Vector3 mousePosition = Input.mousePosition;
            Vector3 worldPosition = mainCamera.ScreenToWorldPoint(mousePosition);  //마우스 2D좌표를 월드3D좌표로 변환
            Vector3 newPosition = currentFruit.transform.position;
            newPosition.x = worldPosition.x;
            float halfFruitSize = fruitSized[cuttrentFruitType] / 2f;

            if (newPosition.x < -gameWidth / 2 + halfFruitSize)
            {
                newPosition.x = -gameWidth / 2 + halfFruitSize;
            }
            else if (newPosition.x > gameWidth / 2 - halfFruitSize)
            {
                newPosition.x = gameWidth / 2 - halfFruitSize;
            }
            currentFruit.transform.position = newPosition;

            if (Input.GetMouseButtonDown(0) && fruitTimer == -3.0f)
            {
                DropFruit();
            }
        }
    }
    void SpawnNewFruit()
    {
        if (!isGameOver)
        {
            cuttrentFruitType = Random.Range(0, 3);
            Vector3 mousePosition = Input.mousePosition;
            Vector3 worldPosition = mainCamera.ScreenToWorldPoint(mousePosition);  //마우스 2D좌표를 월드3D좌표로 변환

            Vector3 spawnPosition = new Vector3(worldPosition.x, fruitStartHeight, 0);
            float halfFruitSize = fruitSized[cuttrentFruitType] / 2f;

            spawnPosition.x = Mathf.Clamp(spawnPosition.x, -gameWidth / 2 + halfFruitSize, gameWidth / 2 - halfFruitSize);

            currentFruit = Instantiate(fruitPrefebs[cuttrentFruitType], spawnPosition, Quaternion.identity);
            currentFruit.transform.localScale = new Vector3(fruitSized[cuttrentFruitType], fruitSized[cuttrentFruitType], 1);
            Rigidbody2D rb = currentFruit.GetComponent<Rigidbody2D>();

            if (rb != null)
            {
                rb.gravityScale = 0f;
            }
        }
    }
    void DropFruit()
    {
        Rigidbody2D rb = currentFruit.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.gravityScale = 1f;
            currentFruit = null;
            fruitTimer = 1.0f;
        }
    }
    public void MergeFruits(int fruitType, Vector3 position)
    {
        if (fruitType < fruitPrefebs.Length - 1)
        {
            GameObject newFruit = Instantiate(fruitPrefebs[fruitType + 1], position, Quaternion.identity);
            newFruit.transform.localScale = new Vector3(fruitSized[fruitType + 1], fruitSized[fruitType + 1], 1.0f);
        }
    }
}
