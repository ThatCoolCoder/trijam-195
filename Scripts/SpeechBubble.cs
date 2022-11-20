using Godot;
using System;
using System.Linq;
using System.Collections.Generic;

public class SpeechBubble : Node2D
{
	public List<ItemType> Items { get; set; } = new();

	private Node2D line1;
	private Sprite line1i;
	private Label line1t;
	private Node2D line2;
	private Sprite line2i;
	private Label line2t;

	public override void _Ready()
	{
		line1 = GetNode<Node2D>("Line1");
		line1i = GetNode<Sprite>("Line1/Sprite");
		line1t = GetNode<Label>("Line1/Label");
		line2 = GetNode<Node2D>("Line2");
		line2i = GetNode<Sprite>("Line2/Sprite");
		line2t = GetNode<Label>("Line2/Label");
	}

	public override void _Process(float delta)
	{
		var types = Items.GroupBy(x => x.IconPath).ToDictionary(x => x.Key, x => x.Count());
		if (types.Keys.Count > 0)
		{
			var type1 = types.Keys.ToList()[0];
			line1.Visible = true;
			line1i.Texture = ResourceLoader.Load<Texture>(type1);
			line1t.Text = $"x{types[type1]}";
		}
		else
		{
			line1.Visible = false;
		}
		if (types.Keys.Count > 1)
		{

			var type2 = types.Keys.ToList()[1];
			line2.Visible = true;
			line2i.Texture = ResourceLoader.Load<Texture>(type2);
			line2t.Text = $"x{types[type2]}";
		}
		else
		{
			line2.Visible = false;
		}
	}
}
