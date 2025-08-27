/* An object to handle the level grids. */


using System.Numerics;

public class Grid
{

    // Cell variables
    public int CellSize { get; }
    public int Columns { get; }
    public int Rows { get; }
    public bool[,] Cells { get; private set; }

    public Grid(int columns, int rows, int cellSize)
    {
        CellSize = cellSize;
        Columns = columns;
        Rows = rows;
        Cells = new bool[columns, rows];
    }

    public bool CheckIfInGrid(Vector2 vectorPosition)
    {
        if (
            vectorPosition.X > 0 &&
            vectorPosition.X < Columns * CellSize &&
            vectorPosition.Y > 0 &&
            vectorPosition.Y < Rows * CellSize
         ) {
            return true;
        }
        return false;
    }

    private bool CheckColumnRowValidity(int column, int row)
    {
        if (column < 0) return false;
        if (row < 0) return false;
        if (column >= Columns) return false;
        if (row >= Rows) return false;
        return true;
    }

    public bool GetCell(int column, int row)
    {
        bool validInput = CheckColumnRowValidity(column, row);
        if (!validInput)
        {
            return false;
        }

        return Cells[column, row];
    }

    public void SetCell(int column, int row, bool value)
    {
        if (CheckColumnRowValidity(column, row))
        {
            Cells[column, row] = value;
        }
        else
        {
            throw new IndexOutOfRangeException($@"Tried to update value at column and row {column}, {row} 
            But grid only has dimensions {Columns}, {Rows}.");
        }
    }

    public Vector2 ToWorld(int column, int row)
    {
        bool validInput = CheckColumnRowValidity(column, row);
        if (!validInput)
        {
            throw new ArgumentException("Invalid input.");
        }
        Vector2 answerVector = new();
        answerVector.X = column * CellSize;
        answerVector.Y = row * CellSize;
        return answerVector;
    }
    public (int, int) ToGrid(Vector2 position)
    {
        int column = (int)(position.X / CellSize);
        int row = (int)(position.Y / CellSize);
        return (column, row);
    }

    public bool[] MooreNeighborhood(int column, int row)
    {
        var answerList = new bool[8];
        int[] neighborOffsetX = { -1, 0, 1, -1, 1, -1, 0, 1 };
        int[] neighborOffsetY = { -1, -1, -1, 0, 0, 1, 1, 1 };
        for (int i = 0; i < neighborOffsetX.Length; i++)
        {
            if (CheckColumnRowValidity(column + neighborOffsetX[i], row + neighborOffsetY[i]))
            {
                answerList[i] = Cells[column + neighborOffsetX[i], row + neighborOffsetY[i]];
            }
            else
            {
                // By default all cells outside of our grid are false.
                answerList[i] = false;
            }
        }
        return answerList;
    }

    public int MooreNeighborhoodCount(int column, int row)
    {
        int count = 0;
        var answerList = MooreNeighborhood(column, row);
        for (int i = 0; i < answerList.Length; i++)
        {
            if (answerList[i])
            {
                count++;
            }
        }
        return count;
    }

    public void UpdateGrid(bool[,] nextCells)
    {
        Cells = nextCells;
    }
}
