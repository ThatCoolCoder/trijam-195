using Godot;
using System.Collections.Generic;
using System.Linq;

public class Shopper : Area2D
{
	public Vector2 Target { get; set; } = Vector2.Zero;
	public float Speed { get; set; } = 100;
	public List<ItemType> WantedItems { get; set; } = new();
	public const int MaxItems = 4;
	private SpeechBubble speechBubble { get; set; }
	private bool isSatisfied = false;

	private Vector2 orig;

	private List<DragItem> pots = new();


	public override void _Ready()
	{
		orig = new Vector2(GlobalPosition.x, 10000);
		speechBubble = GetNode<SpeechBubble>("SpeechBubble");
		Target = new Vector2(GlobalPosition.x, 0);
		var x = GD.RandRange(0, MaxItems);
		var availableItems = ItemType.ItemTypes.Where(x => x.Level == Progress.Level).ToList();
		for (int i = 0; i < x; i++)
		{
			WantedItems.Add(Utils.RandomElement(availableItems));
		}
		speechBubble.Items = WantedItems;
	}

	public override void _Process(float delta)
	{
		speechBubble.Visible = !isSatisfied;
	}

	public override void _PhysicsProcess(float delta)
	{
		var displacement = Target - GlobalPosition;
		if (displacement.LengthSquared() > Speed * delta * Speed * delta)
		{
			displacement = displacement.Normalized();
			displacement *= Speed * delta;
			Position += displacement;
		}
		else
		{
			GlobalPosition = Target;
		}

		var toRem = new List<DragItem>();
		foreach (var pot in pots)
		{
			if (!pot.dropped) continue;
			if (WantedItems.Any(x => x.Name == pot.ItemType.Name))
			{
				WantedItems.Remove(pot.ItemType);
				MoneyTracker.Money += pot.ItemType.Cost;
				pot.QueueFree();
				if (WantedItems.Count == 0)
				{
					Target = orig;
					isSatisfied = true;

				}
				toRem.Add(pot);
			}
		}
		foreach (var item in toRem) pots.Remove(item);
	}

	private void OnViewportExit(object viewport)
	{
		QueueFree();
	}


	private void OnAreaEntered(Node2D area)
	{
		if (area.IsInGroup("Counter"))
		{
			Target = GlobalPosition;
		}
		if (area is DragItem item)
		{
			pots.Add(item);

		}
	}

	private void _on_Shopper_area_exited(Node2D area)
	{
		if (area is DragItem item) pots.Remove(item);
	}
}
