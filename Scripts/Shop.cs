using Godot;
using System.Collections.Generic;
using System.Linq;

public class Shop : Node2D
{
	private bool rentDue = false;
	// probability that there is no rent this frame
	private float noRentProbability = 1;
	private float rentProbabilityIncrement = 0.1f;
	private float rentProbabilityMultiplier = 0.1f;

	private Label payRentIndicator;

	public override void _Ready()
	{
		payRentIndicator = GetNode<Label>("CanvasLayer/UI/PayRentIndicator");
		payRentIndicator.Visible = false;
		GetNode<Label>("CanvasLayer/UI/Lavel").Text = $"Level {Progress.Level}";

		var items = ItemType.ItemTypes.Where(x => x.Level == Progress.Level).ToList();
		var i1 = items[0];
		var i2 = items[1];
		GetNode<ItemBox>("ItemBox1").ItemName = i1.Name;
		GetNode<ItemBox>("ItemBox1")._Ready();
		GetNode<ItemBox>("ItemBox2").ItemName = i2.Name;
		GetNode<ItemBox>("ItemBox2")._Ready();

	}

	public override void _PhysicsProcess(float delta)
	{
		UpdateUI();
		if (rentDue)
		{
			noRentProbability = Mathf.Max(noRentProbability - rentProbabilityIncrement * delta, 0);
		}
		CheckIfLost(delta);
		CheckIfWon();
	}

	private void CheckIfLost(float delta)
	{
		var broke = MoneyTracker.Money < MoneyTracker.MaxDebt;
		var rent = GD.Randf() < (1 - noRentProbability) * rentProbabilityMultiplier * delta;
		if (broke)
		{
			Progress.LoseReason = $"You had too much debt (you had ${-MoneyTracker.Money} and max is ${-MoneyTracker.MaxDebt})";
			GetTree().ChangeScene("res://Scenes/LoseScreen.tscn");
		}
		else if (rent && rentDue)
		{

			Progress.LoseReason = "You couldn't pay rent and were evicted";
			GetTree().ChangeScene("res://Scenes/LoseScreen.tscn");
		}
	}

	private void CheckIfWon()
	{
		if (MoneyTracker.Money >= 0)
		{
			Progress.LoseReason = "You are no longer broke";

			GetTree().ChangeScene("res://Scenes/WinScreen.tscn");
		}

	}

	private void UpdateUI()
	{
		GetNode<Label>("CanvasLayer/UI/Money").Text = $"${MoneyTracker.Money}";
		GetNode<Button>("CanvasLayer/UI/BuyNextShop").Text = $"Go to next shop for ${MoneyTracker.ShopPrice(Progress.Level + 1)}";
	}

	private void OnRentTimerTimeout()
	{
		payRentIndicator.Visible = true;
		rentDue = true;
	}

	public static string LoadRandomShop()
	{
		var shopName = Utils.RandomElement(new List<string>() { "Shop1", "Shop2", "Shop3" });
		return $"res://Scenes/Shops/{shopName}.tscn";
	}

	private void _on_BuyNextShop_pressed()
	{
		if (Progress.Level < 4) Progress.Level += 1;
		MoneyTracker.Money -= MoneyTracker.ShopPrice(Progress.Level);
		GD.Print(MoneyTracker.Money);
		CheckIfLost(0);
		GetTree().ChangeScene(Shop.LoadRandomShop());
	}
}
