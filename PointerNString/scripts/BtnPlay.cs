using Godot;
using System;

public partial class BtnPlay : Button
{
	private void _on_pressed()
	{
		GetTree().ChangeSceneToFile("res://level_maze_1.tscn");
	}
	
	private void _on_btn_menu_pressed()
	{
		GetTree().ChangeSceneToFile("res://menu.tscn");
	}
	
	private void _on_btn_quit_pressed()
	{
		GetTree().Quit();
	}
	
	private void _on_btn_play_2_pressed()
	{
		GetTree().ChangeSceneToFile("res://level_maze_2.tscn");
	}
}
