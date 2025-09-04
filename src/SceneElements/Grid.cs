/* An object to handle the level grids. */


using System.Numerics;
using Raylib_cs;

public class Grid
{
    #region Characteristics
    /// <summary>
    /// Check if elements are in the Grid and do operations on coordinates.
    /// </summary>

    /// Information on cell dimensions.
    public int CellSize { get; private set; }
    public int Columns { get; private set; }
    public int Rows { get; private set; }
    #endregion

    #region Draw information
    public int OffsetX { get; private set; }
    public int OffsetY { get; private set; }
    public Color GridColor = Color.White;
    #endregion

    public int[,] Cells { get; private set; }
    private Dictionary<int, CellCoordinates> _occupancyDict = new();

    public Grid(int columns, int rows, int cellSize, int offsetX, int offsetY)
    {
        CellSize = cellSize;
        Columns = columns;
        Rows = rows;
        OffsetX = offsetX;
        OffsetY = offsetY;
        Cells = new int[columns, rows];
    }

    #region Getter
    public (int, int) GetOffset()
    {
        return (OffsetX, OffsetY);
    }

    public (int, int) GetDimensions()
    {
        return (Columns, Rows);
    }

    public int GetCellSize()
    {
        return CellSize;
    }

    public bool CheckIfEmptyCell(CellCoordinates coordinates)
    {
        return CheckIfEmptyCell(coordinates.X, coordinates.Y);
    }

    public bool CheckIfEmptyCell(int column, int row)
    {
        return Cells[column, row] == 0;
    }

    public void FreeCell(CellCoordinates cell)
    {
        Cells[cell.X, cell.Y] = 0;
    }

    public void OccupyCell(CellCoordinates cell, int id)
    {
        _occupancyDict[id] = cell;
    }
    #endregion

    #region Check if elements are in a grid.
    /// <summary>
    /// Method to check if a vector position is in the grid (example: mouseposition)
    /// </summary>
    /// <param name="vectorPosition"></param>
    /// <returns></returns>
    public bool CheckIfInGrid(Vector2 vectorPosition)
    {
        if (
            vectorPosition.X >= 0 &&
            vectorPosition.X < Columns * CellSize &&
            vectorPosition.Y >= 0 &&
            vectorPosition.Y < Rows * CellSize
         )
        {
            return true;
        }
        return false;
    }

    /// <summary>
    /// Check if a column an drow is the grid (to say when elements are out of bound).
    /// </summary>
    /// <param name="column"></param>
    /// <param name="row"></param>
    /// <returns></returns>
    public bool CheckIfInGrid(int column, int row)
    {
        if (
            column >= 0 &&
            column < Columns &&
            row >= 0 &&
            row < Rows
         )
        {
            return true;
        }
        return false;
    }

    /// <summary>
    /// Check if coordinates are in a grid (check if coordinates are valid)
    /// </summary>
    /// <param name="coordinates"></param>
    /// <returns></returns>
    public bool CheckIfInGrid(CellCoordinates coordinates)
    {
        return CheckIfInGrid(coordinates.X, coordinates.Y);
    }

    #endregion

    #region Converts positions to cellCoordinates and vice versa.
    /// <summary>
    /// Gives position on the screen of a column and row in the grid.
    /// </summary>
    /// <param name="column"></param>
    /// <param name="row"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentException"></exception>
    public Vector2 ToWorld(int column, int row)
    {
        bool validInput = CheckIfInGrid(column, row);
        if (!validInput)
        {
            throw new ArgumentException("Element is outside of the Grid.");
        }
        Vector2 answerVector = new();
        answerVector.X = column * CellSize;
        answerVector.Y = row * CellSize;
        return answerVector;
    }

    /// <summary>
    /// Gives the position on the screen of a cellcoordinate on the screen.
    /// </summary>
    /// <param name="coordinates"></param>
    /// <returns></returns>
    public Vector2 ToWorld(CellCoordinates coordinates)
    {
        return ToWorld(coordinates.X, coordinates.Y);
    }

    /// <summary>
    /// Gives the position on the grid of a point in space.
    /// </summary>
    /// <param name="position"></param>
    /// <returns></returns>
    public CellCoordinates ToGrid(Vector2 position)
    {
        bool validInput = CheckIfInGrid(position);
        if (!validInput)
        {
            throw new ArgumentException("Element is outside of the Grid.");
        }
        int column = (int)(position.X / CellSize);
        int row = (int)(position.Y / CellSize);
        CellCoordinates coordinates = new(column, row);
        return coordinates;
    }
    #endregion

    #region Get Neighboors
    public bool CheckIfNeumannNeighborhood(CellCoordinates coordinates)
    {
        bool hasNeighbor = false;
        int[] neighborOffsetX = { 0, 0, 1, -1 };
        int[] neighborOffsetY = { 1, -1, 0, 0 };
        for (int i = 0; i < neighborOffsetX.Length; i++)
        {
            hasNeighbor = !CheckIfEmptyCell((coordinates.X + neighborOffsetX[i] + Columns) % Columns, (coordinates.Y + neighborOffsetY[i] + Rows) % Rows);
            if (hasNeighbor)
            {
                return hasNeighbor;
            }
        }
        return hasNeighbor;
    }
    public bool CheckIfMooreNeighborhood(CellCoordinates coordinates)
    {
        bool hasNeighbor = false;
        int[] neighborOffsetX = { -1, 0, 1, -1, 1, -1, 0, 1 };
        int[] neighborOffsetY = { -1, -1, -1, 0, 0, 1, 1, 1 };
        for (int i = 0; i < neighborOffsetX.Length; i++)
        {
            hasNeighbor = !CheckIfEmptyCell((coordinates.X + neighborOffsetX[i] + Columns) % Columns, (coordinates.Y + neighborOffsetY[i] + Rows) % Rows);
            if (hasNeighbor)
            {
                return hasNeighbor;
            }
        }
        return hasNeighbor;
    }
    #endregion

    public void Update()
    {
        foreach (KeyValuePair<int, CellCoordinates> occupy in _occupancyDict)
        {
            if (CheckIfEmptyCell(occupy.Value) && Cells[occupy.Value.X, occupy.Value.Y] != occupy.Key)
            {
                Cells[occupy.Value.X, occupy.Value.Y] = occupy.Key;
            }
            else
            {
                EntityHandler entityHandler = ServiceLocator.Get<EntityHandler>();
                int finalIndex = entityHandler.EvaluateCollision(occupy.Key, Cells[occupy.Value.X, occupy.Value.Y]);
                Cells[occupy.Value.X, occupy.Value.Y] = finalIndex;
            }
        }
        _occupancyDict = new();
    }

    public void Draw()
    {
        for (int interRows = 0; interRows < Rows; interRows++)
        {
            for (int interColumns = 0; interColumns < Columns; interColumns++)
            {
                Vector2 cellPosition = ToWorld(interRows, interColumns);
                Raylib.DrawRectangleLines((int)cellPosition.X + OffsetX, (int)cellPosition.Y + OffsetY, CellSize, CellSize, GridColor);
            }
        }
    }

}
