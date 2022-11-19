using Godot;

public class ShopperSpawner : Node2D
{
	public int MaxShoppers = 5;
	public float ShopperChance = 1;
	private PackedScene ShopperPrefab = ResourceLoader.Load<PackedScene>("res://Scenes/Shopper.tscn");

	private PathFollow2D spawnPathObj;

	public override void _Ready()
	{
		spawnPathObj = GetNode<PathFollow2D>("SpawnPath/Obj");
	}

	public override void _PhysicsProcess(float delta)
	{
		var numShoppers = GetTree().GetNodesInGroup("Shopper").Count;
		if (numShoppers < MaxShoppers && GD.Randf() < ShopperChance * delta)
		{
			var prefab = ShopperPrefab.Instance<Node2D>();
			spawnPathObj.Offset = GD.Randi();
			prefab.GlobalPosition = spawnPathObj.GlobalPosition;
			GetParent().AddChild(prefab);
		}
	}
}
