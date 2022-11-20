using Godot;

public class LoseAndWinScreen : Control
{
	public override void _Ready()
	{
		GetNode<Label>("Label2").Text = Progress.LoseReason;
	}

	private void OnButtonPressed()
	{
		GetTree().ChangeScene("res://Scenes/StartScreen.tscn");
	}
}
