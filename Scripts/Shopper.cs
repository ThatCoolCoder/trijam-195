using Godot;
using System.Collections.Generic;
using System.Linq;

public class Shopper : Area2D
{
    public Vector2 Target { get; set; } = Vector2.Zero;
    public float Speed { get; set; } = 100;
    public List<ItemType> WantedItems { get; set; }
    public const int MaxItems = 4;
    private SpeechBubble SpeechBubble { get; set; }
    public override void _Ready()
    {
        Target = (GetTree().GetNodesInGroup("Counter")[0] as Node2D).GlobalPosition;
        var x = GD.RandRange(0, MaxItems);
        var availableItems = ItemType.ItemTypes.Where(x => x.Level == Progress.Level).ToList();
        for (int i = 0; i < x; i++)
        {
            WantedItems.Add(Utils.RandomElement(availableItems));
        }
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
    }

    private void OnViewportExit(object viewport)
    {
        QueueFree();
    }
}
