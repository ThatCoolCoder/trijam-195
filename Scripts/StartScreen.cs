using Godot;

public class StartScreen : Control
{
	private void OnPlayButtonPressed()
	{
		MoneyTracker.Reset();
		Progress.Reset();
		GetTree().ChangeScene(Shop.LoadRandomShop());
	}
}
