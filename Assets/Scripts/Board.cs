using UnityEngine;

public class Board : MonoBehaviour
{
    private static Board _instance;

    public static Board Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<Board>();
                
                if (_instance == null)
                {
                    GameObject obj = new GameObject();
                    obj.name = typeof(Board).Name;
                    _instance = obj.AddComponent<Board>();
                }
            }

            return _instance;
        }
    }

    public GameObject cellPrefab;
    public GameObject dicePrefab;
    public int numRows = 9;
    public int numCols = 9;

    public Cell[,] grid;

    public Level level;

    protected virtual void Awake()
    {
        if (_instance == null)
        {
            _instance = this as Board;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    void Start()
    {
        grid = new Cell[9, 9];
        level = LevelManager.Instance.CurrentLevel();

        LevelCellsCreate(level);
    }

    private void LevelCellsCreate(Level level)
    {
        //create level grids
        for (int row = 0; row < numRows; row++)
        {
            for (int col = 0; col < 9; col++)
            {
                Cell cell = Instantiate(cellPrefab, new Vector3(row, col, 0), Quaternion.identity, this.transform).GetComponent<Cell>();
                int value = level.values[row].values[col] == "" ? 0 : int.Parse(level.values[row].values[col]);
                grid[row, col] = cell;
                cell.Init(row, col, level.columns[row].rows[col] , value);
            }
        }

        //created grids match control
        for (int i = 0; i < 9; i++)
        {
            for (int j = 0; j < 9 - 2; j++)
            {
                if (grid[i, j] == null || grid[i, j].value == 0)
                {
                    continue;
                }
                grid[i, j].MatchControl(grid[i,j].value);
            }
        }
    }
}
