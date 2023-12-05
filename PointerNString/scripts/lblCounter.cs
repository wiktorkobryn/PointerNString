using Godot;
using System;

public partial class lblCounter : Label
{
	private int coins = 0;
	private int maxCoins = 150;
	public Timer ExitTimer {get; set;}
	public Node2D WinScreen {get; set;}
	
	public int Coins
	{
		get
		{
			return coins;
		}
		set
		{
			if(value >= 0)
				coins = value;
		}
	}
	
	private void updateText()
	{
		Text = coins.ToString() + "/" + maxCoins.ToString();
	}
	
	public override void _Ready()
	{
		updateText();
		ExitTimer = (Timer)GetNode("ExitTimer");
		WinScreen = (Node2D)GetNode("WinScreen");
	}

	private void _on_coin_collected()
	{
		coins++;
		updateText();
		
		if(coins >= maxCoins)
		{
			WinScreen.Visible = true;
			ExitTimer.Start();	
		}
	}
	
	private void _on_exit_timer_timeout()
	{
		GetTree().ChangeSceneToFile("res://menu.tscn");
	}
}
