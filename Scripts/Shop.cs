using Godot;
using System.Collections.Generic;

public class Shop : Node2D
{
	private bool rentDue = false;
	// probability that there is no rent this frame
	private float noRentProbability = 1;
	private float rentProbabilityIncrement = 0.999f;
	private float rentProbabilityMultiplier = 0.1f;

	private Sprite payRentIndicator;

	public override void _Ready()
	{
		payRentIndicator = GetNode<Sprite>("CanvasLayer/UI/PayRentIndicator");
		payRentIndicator.Visible = false;
	}

	public override void _PhysicsProcess(float delta)
	{
		UpdateUI();
		if (rentDue)
		{
			noRentProbability -= Mathf.Max(rentProbabilityIncrement * delta, 0);
		}
		CheckIfLost(delta);
		CheckIfWon();
	}

	private void CheckIfLost(float delta)
	{
		var broke = MoneyTracker.Money < MoneyTracker.MaxDebt;
		var rent = GD.Randf() < (1 - noRentProbability) * rentProbabilityMultiplier * delta;
		if (broke || rent)
		{
			GetTree().ChangeScene("res://Scenes/LoseScreen.tscn");
		}
	}

	private void CheckIfWon()
	{
		if (MoneyTracker.Money >= 0) GetTree().ChangeScene("res://Scenes/WinScreen.tscn");
	}

	private void UpdateUI()
	{
		GetNode<Label>("CanvasLayer/UI/Money").Text = $"${MoneyTracker.Money}";
		if (MoneyTracker.Money >= MoneyTracker.ShopPrice(Progress.Level + 1))
		{
			GetNode<Button>("CanvasLayer/UI/BuyNextShop").Visible = true;
			GetNode<Button>("CanvasLayer/UI/BuyNextShop").Text = $"Buy next shop for ${MoneyTracker.ShopPrice(Progress.Level + 1)}";
			GetNode<Button>("CanvasLayer/UI/CantBuyNextShop").Visible = false;
		}
		else
		{

			GetNode<Button>("CanvasLayer/UI/BuyNextShop").Visible = false;
			GetNode<Button>("CanvasLayer/UI/CantBuyNextShop").Visible = true;
			GetNode<Button>("CanvasLayer/UI/CantBuyNextShop").Text = $"Next shop costs ${MoneyTracker.ShopPrice(Progress.Level + 1)}";
		}
	}

	private void OnRentTimerTimeout()
	{
		payRentIndicator.Visible = true;
		rentDue = true;
	}

	private static void LoadRandomShop()
	{
		var shopName = Utils.RandomElement(new List<string>() { "Shop1", "Shop2", "Shop3" });

	}

}
