using Raylib_cs;

[TestClass]
public class DirectionBlockTests
{
    private Grid _grid = new Grid(10, 10, 10, 0, 0);
    private PlayerHandler _playerHandler => ServiceLocator.Get<PlayerHandler>();
    private EntityHandler _entityHandler => ServiceLocator.Get<EntityHandler>();

    [TestInitialize]
    public void Setup()
    {
        ServiceLocator.Reset();
        EntityHandler entityHandler = new();
        _grid = new Grid(10, 10, 10, 0, 0);
        PlayerHandler playerHandler = new(0, Color.Blue);
    }

    [TestMethod]
    public void DirectionBlock_ShouldRegisterWithEntityHandler()
    {
        var block = new DirectionBlock(CellCoordinates.right, 5, new CellCoordinates(2, 2), _grid, _playerHandler);
        var retrieved = _entityHandler.GetEntity(block.GetID());

        Assert.AreEqual(block, retrieved, "DirectionBlock should be registered in EntityHandler");
    }

    [TestMethod]
    public void DirectionBlock_ShouldBePlaced_OnGrid()
    {
        var block = new DirectionBlock(CellCoordinates.up, 5, new CellCoordinates(0, 0), _grid, _playerHandler);
        var pos = new CellCoordinates(3, 3);

        block.Place(pos, CellCoordinates.down);
        _grid.Update();

        Assert.AreEqual(pos, block.GetPosition(), "DirectionBlock should store the correct position");
        Assert.AreEqual(CellCoordinates.down, block.GetDirection(), "DirectionBlock should store the correct direction");
        Assert.AreEqual(block.GetID(), _grid.Cells[pos.X, pos.Y], "Grid should mark the block's cell as occupied");
    }

    [TestMethod]
    public void DirectionBlock_ShouldDisable_WhenCollided()
    {
        var block = new DirectionBlock(CellCoordinates.left, 5, new CellCoordinates(4, 4), _grid, _playerHandler);
        block.Place(new CellCoordinates(4, 4), CellCoordinates.left);
        _grid.Update();

        block.Collide(new DummyEntity());
        _grid.Update();

        Assert.AreEqual(Entity.EntityState.disabled, block.GetState(), "Block should be disabled after collision");
        Assert.AreEqual(0, _grid.Cells[4, 4], "Block's grid cell should be freed");
        Assert.IsTrue(_playerHandler.GetQueue().Contains(block), "Block should be added back to PlayerHandler queue");
    }

    [TestMethod]
    public void DirectionBlock_Reset_ShouldDisableActiveBlock()
    {
        var block = new DirectionBlock(CellCoordinates.up, 5, new CellCoordinates(1, 1), _grid, _playerHandler);
        block.Place(new CellCoordinates(1, 1), CellCoordinates.up);
        _grid.Update();

        block.Reset();
        _grid.Update();

        Assert.AreEqual(Entity.EntityState.disabled, block.GetState(), "Reset should disable the block");
        Assert.AreEqual(0, _grid.Cells[1, 1], "Reset should free the grid cell");
        Assert.IsTrue(_playerHandler.GetQueue().Contains(block), "Reset should add block back to PlayerHandler queue");
    }
}
