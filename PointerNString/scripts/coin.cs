using Godot;
using System;

public partial class coin : Area3D
{
	public Timer Timer1 {get; set;}
	public AnimationPlayer Animation1 {get; set;}
	
	[Signal]
	public delegate void CoinCollectedEventHandler();
	
	public override void _Ready()
	{
		Timer1 = (Timer)GetNode("CoinTimer");
		Animation1 = (AnimationPlayer)GetNode("CoinAnimation");
	}
	
	public override void _PhysicsProcess(double delta)
	{
		RotateY(6);
	}
	
	private void _on_body_entered(Node3D body)
	{
		if(body.Name == "Player1")
		{
			Timer1.Start();
			Animation1.Play("bounce");
		}
	}
	
	private void _on_coin_timer_timeout()
	{
		QueueFree();
		EmitSignal(SignalName.CoinCollected);
	}
}
