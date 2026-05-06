using Godot;
using System;

public partial class DeathZone : Area2D
{
    public override void _Ready()
    {
        // Conecta o sinal via código
        BodyEntered += OnBodyEntered;
    }

    private void OnBodyEntered(Node body)
    {
        // Verifica se quem entrou foi o Player
        if (body is CharacterBody2D)
        {
            GetTree().ReloadCurrentScene();
        }
    }
}