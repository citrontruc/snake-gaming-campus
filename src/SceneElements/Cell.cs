/* A structure to do operations on elements of a grid (movements, element comparisons...) */

public struct CellCoordinates
{
    /// <summary>
    /// Position of a cell in a grid;
    /// </summary>
    public int X { get; private set; }
    public int Y { get; private set; }

    public CellCoordinates(int x, int y)
    {
        X = x;
        Y = y;
    }

    #region Operations on CellCoordinates
    /// <summary>
    /// Methods to implement operations elementWise.
    /// </summary>
    /// <param name="cellA"></param>
    /// <param name="cellB"></param>
    /// <returns></returns>
    public static CellCoordinates operator +(CellCoordinates cellA, CellCoordinates cellB)
    {
        CellCoordinates cellResult = new(cellA.X + cellB.X, cellA.Y + cellB.Y);
        return cellResult;
    }

    public static CellCoordinates operator -(CellCoordinates cellA, CellCoordinates cellB)
    {
        CellCoordinates cellResult = new(cellA.X - cellB.X, cellA.Y - cellB.Y);
        return cellResult;
    }

    public static CellCoordinates operator *(CellCoordinates cellA, CellCoordinates cellB)
    {
        CellCoordinates cellResult = new(cellA.X * cellB.X, cellA.Y * cellB.Y);
        return cellResult;
    }

    public static CellCoordinates operator *(CellCoordinates cellA, int coefficient)
    {
        CellCoordinates cellResult = new(cellA.X * coefficient, cellA.Y * coefficient);
        return cellResult;
    }

    public static CellCoordinates operator *(int coefficient, CellCoordinates cellA)
    {
        CellCoordinates cellResult = cellA * coefficient;
        return cellResult;
    }

    public static bool operator ==(CellCoordinates cellA, CellCoordinates cellB)
    {
        return (cellA.X == cellB.X) && (cellA.Y == cellB.Y);
    }

    public static bool operator !=(CellCoordinates cellA, CellCoordinates cellB)
    {
        return (cellA.X != cellB.X) || (cellA.Y != cellB.Y);
    }

    #endregion

    #region Override common methods
    public override string ToString()
    {
        return $"Cell with coordinates ({X}, {Y}).";
    }

    public override bool Equals(object? obj)
    {
        if (obj is CellCoordinates)
        {
            return Equals((CellCoordinates)obj);
        }
        return false;
    }

    public bool Equals(CellCoordinates cell)
    {
        return this == cell;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(X, Y);
    }

    #endregion

}
