using Godot;
using System;

public class DragItem : Area2D
{
	public ItemType ItemType { get; set; }
	public bool dropped = false;

	public override void _Ready()
	{
		GetNode<Sprite>("Sprite").Texture = ResourceLoader.Load<Texture>(ItemType.IconPath);
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(float delta)
	{
		if (!dropped) GlobalPosition = GetViewport().GetMousePosition();

		if (Input.IsActionJustReleased("click"))
		{
			dropped = true;
			GetNode<Timer>("DropTimer").Start();
		}
	}


	private void _on_DropTimer_timeout()
	{
		QueueFree();
	}
}
