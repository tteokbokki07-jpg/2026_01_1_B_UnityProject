using UnityEngine;
using UnityEngine.UIElements;

public class GridCell : MonoBehaviour
{
    public int x, y;
    public DraggableRank currentRank;
    public SpriteRenderer cellRenderers;

    private void Awake()
    {
        cellRenderers = GetComponent<SpriteRenderer>();
    }
    public void Initialize(int gridX, int gridY) //좌표 초기화
    {
        x = gridX;
        y = gridY;
        name = "Cell_" + x + "_" + y;
    }

    public bool isEmpty() //칸 공백 확인
    {
        return currentRank == null;
    }
    public bool ContainsPosition(Vector3 position)
    {
        Bounds bouns = cellRenderers.bounds;
        return bouns.Contains(position);
    }
    public void SetRank(DraggableRank rank)
    {
        currentRank = rank;
        if (rank != null)
        {
            rank.currentCell = this;
        }
        rank.originalPosition = new Vector3(transform.position.x, transform.position.y, 0);
        rank.transform.position = new Vector3(transform.position.x, transform.position.y, 0);
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
