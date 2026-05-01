using Godot;
using System;

public partial class Player : CharacterBody2D
{
    // velocidade horizontal do personagem.
    //private: acessivel somente nesta classe
    private const float SPEED = 200.0F;
    // força do pulo
    // negativo pq na godot o eixo y cresce para baixo
    private const float JUMPFORCE = -300.0F;

    //Gravidade do projeto.
    //Pegamos automaticamente a gravidade definida nas configuraçoes da Godot
    // AsSingle() converte o valor retornado para float.
    private float _gravity = ProjectSettings.GetSetting("physics/2d/default_gravity").AsSingle();

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
        
        // Se o personagem não estiver no chão...
        if (!IsOnFloor())
        {
            // aplicamos gravidade no eixo Y.
            // Multiplicamos por delta para manter movimento consistente
            // independentemente do FPS
            velocity.Y += _gravity * (float)delta;
        }

        // ======================================================
        // PULO
        // ======================================================

        // Se o jogador apertou o botão de pulo
        // E o personagem está no chão...    
        if (Input.IsActionJustPressed("ui_accept") && IsOnFloor())
        {
            //Aplicamos a força do pulo.
            velocity.Y = JUMPFORCE;
        }

        // ======================================================
        // MOVIMENTO HORIZONTAL
        // ======================================================

        // Retorna:
        // -1 quando aperta esquerda
        // 1 quando aperta direita 
        // 0 quando nao aperta nada
        float direction = Input.GetAxis("ui_left", "ui_right");

        //Se existe alguma direção...
        if (direction != 0)
        {
            // Move o personagem
            velocity.X = direction * SPEED;
        }else
        {
            // caso contrario, desacelera suavemente até parar.
            velocity.X = Mathf.MoveToward(
                velocity.X, // valor atual
                0,          // destino
                SPEED       // velocidade da desaceleraçao 
            );
        }

        // ======================================================
        // APLICA MOVIMENTO
        // ======================================================

        // atualiza a propriedade velocity do characterody2D
        Velocity = velocity;

        // Move o personagem usando colisao e fisica
        MoveAndSlide();

    }
}
