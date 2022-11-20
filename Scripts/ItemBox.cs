using Godot;
using System;
using System.Linq;

public class ItemBox : Area2D
{
	[Export] public string ItemName { get; set; }

	private bool mouseOver = false;

	private PackedScene dragItemPrefab = ResourceLoader.Load<PackedScene>("res://Scenes/DragItem.tscn");
	private ItemType itemType;

	public override void _Ready()
	{
		itemType = ItemType.ItemTypes.Where(x => x.Name == ItemName).First();
		GetNode<Sprite>("Sprite2").Texture = ResourceLoader.Load<Texture>(itemType.IconPath);
	}


	private void _on_ItemBox_mouse_entered()
	{
		mouseOver = true;
	}


	private void _on_ItemBox_mouse_exited()
	{
		mouseOver = false;
	}

	public override void _Input(InputEvent _event)
	{

		if (mouseOver)
		{
			if (_event is InputEventMouseButton x && _event.IsPressed())
			{
				if (x.ButtonIndex == 1)
				{
					var item = dragItemPrefab.Instance<DragItem>();
					item.ItemType = itemType;
					GetParent().AddChild(item);
				}
			}
		}
	}

}
