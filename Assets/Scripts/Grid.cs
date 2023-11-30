using Assets.Scripts;
using UnityEngine;
using UnityEngine.InputSystem;

public class Grid : MonoBehaviour, IGameListener
{
    public GameObject cellPrefab;
    private const float CELL_OFFSET = .002f;

    [Header("Dimensions")]
    public int nbCellsOnX;
    public int nbCellsOnY;
    public int nbCellsOnZ;

    private Vector3 cellDimension;
    private GameObject[,] cells;
    public GameObject dominantHand;
    public void Start()
    {
        GameObject.FindGameObjectWithTag("GAME").GetComponent<Game>().AddListener(this);
        cells = new GameObject[nbCellsOnX, nbCellsOnZ];
        cellDimension = cellPrefab.GetComponentInChildren<MeshRenderer>().bounds.size;
        for (int idxCellR = 0; idxCellR < nbCellsOnX; idxCellR++)
        {
            for (int idxCellC = 0; idxCellC < nbCellsOnZ; idxCellC++)
            {
                cells[idxCellR, idxCellC] = Instantiate(cellPrefab, transform);
                cells[idxCellR, idxCellC].transform.Translate(new Vector3(idxCellR * (cellDimension.x + CELL_OFFSET), 0, idxCellC * (CELL_OFFSET + cellDimension.z)));
            }
        }
    }

    //public void Update()
    //{
    //    foreach(GameObject cellGO in cells)
    //    {
    //        if (cellGO.GetComponent<Cell>().ContainsContent("AGENT"))
    //        {
    //            cellGO.GetComponentInChildren<AgentBehaviour>().Update();
    //        }
    //    }
    //}

    public void OnDominantHandChange(EHand newDominantHand)
    {
        dominantHand = GameObject.FindGameObjectWithTag(newDominantHand + " HAND");
    }

    public Cell[,] GetCells()
    {
        Cell[,] cellsComps = new Cell[nbCellsOnX, nbCellsOnZ];
        for (int idxCellR = 0; idxCellR < nbCellsOnX; idxCellR++)
        {
            for (int idxCellC = 0; idxCellC < nbCellsOnZ; idxCellC++)
            {
                cellsComps[idxCellR, idxCellC] = cells[idxCellC, idxCellR].GetComponent<Cell>();
            }
        }
        return cellsComps;

    }
    public void MoveAgentTo(AgentBehaviour agent, Cell targetCall)
    {

    }
}