using Godot;

public class LoseAndWinScreen : Control
{
	private void OnButtonPressed()
	{
		GetTree().ChangeScene("res://Scenes/StartScreen.tscn");
	}
}
