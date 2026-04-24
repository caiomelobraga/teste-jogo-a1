using Godot;
using System;
using System.Numerics;

public partial class Player : CharacterBody2D
{
    // velocidade horizontal do personagem.
    //private: acessivel somente nesta classe
    private const float SPEED = 300F;
    // força do pulo
    // negativo pq na godot o eixo y cresce para baixo
    private const float JUMPFORCE = -400F;

    //Gravidade do projeto.
    //Pegamos automaticamente a gravidade definida nas configuraçoes da Godot
    // AsSingle() converte o valor retornado para float.
    private float gravity = ProjectSettings.GetSetting("physics/2D/default_gravity").AsSingle();

    // Método chamado automaticamente a cada frame de física.
    // Geralmente 60 vezes por segundo.
    // delta = tempo desde o ultimo frame.
    public override void _PhysicsProcess(double delta)
    {
        // criamos uma cópia da velocidade atual.
        // Isso facilita modificar cada eixo separadamente.
        Vector2 velocity = Velocity;

        //==============================//
        //==========Gravidade==========//
        //=============================//

        if (!IsOnFloor())
        {
        //
        }
    }
}
