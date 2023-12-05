using Godot;
using System;

public partial class player_1 : CharacterBody3D
{
	public float Speed {get; set;} = 10.0f;
	public float JumpVelocity {get; set;} = 4.5f;
	public float Friction { get; set; } = 5.0f;
	
	//toggable jump
	public bool JumpBlock {get; set;} = false;
	//toggable slippy movement
	public bool SlipMove {get; set;} = false;
	
	public AnimatedSprite3D Sprite {get; set;}

	// Get the gravity from the project settings to be synced with RigidBody nodes.
	public float gravity {get; set;} = ProjectSettings.GetSetting("physics/3d/default_gravity").AsSingle();

	public override void _Ready()
	{
		Sprite = (AnimatedSprite3D)GetNode("Player1Sprite");
	}

	public override void _PhysicsProcess(double delta)
	{
		Vector3 velocity = Velocity;

		// gravity to avoid glitches
		if (!IsOnFloor())
			velocity.Y -= gravity * (float)delta;

		if (JumpBlock  && Input.IsActionJustPressed("ui_accept") && IsOnFloor())
			velocity.Y = JumpVelocity;

		// movement mapped by ui_dir to support controller automatically
		Vector2 inputDir = Input.GetVector("ui_left", "ui_right", "ui_up", "ui_down");
		Vector3 direction = (Transform.Basis * new Vector3(inputDir.X, 0, inputDir.Y)).Normalized();
		if (direction != Vector3.Zero)
		{
			velocity.X = direction.X * Speed;
			velocity.Z = direction.Z * Speed;
			
			if(direction.X > 0)
				Sprite.FlipH = false;
			else
				Sprite.FlipH = true;
		}
		else
		{
			if(SlipMove)
			{
				velocity.X = Mathf.Lerp(Velocity.X, 0, Friction * (float)delta);
				velocity.Z = Mathf.Lerp(Velocity.Z, 0, Friction * (float)delta);
			}
			else
			{
				velocity.X = Mathf.MoveToward(Velocity.X, 0, Speed);
				velocity.Z = Mathf.MoveToward(Velocity.Z, 0, Speed);
			}
		}

		Velocity = velocity;
		MoveAndSlide();
	}
	
	private void _on_enemy_body_entered(Node3D body)
	{
		if(body.Name == "Player1")
			GetTree().ChangeSceneToFile("res://game_over.tscn");
	}
}
