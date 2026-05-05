using Godot;
using System;

public partial class Player : CharacterBody2D
{
    // ======================================================
    // CONFIGURAÇÕES DO PERSONAGEM
    // ======================================================

    // Velocidade horizontal do personagem
    private const float SPEED = 200.0f;

    // Força do pulo (negativo pois Y cresce para baixo na Godot)
    private const float JUMP_FORCE = -300.0f;

    // Gravidade padrão do projeto (vinda das configurações da Godot)
    private float _gravity = ProjectSettings
        .GetSetting("physics/2d/default_gravity")
        .AsSingle();

    // ======================================================
    // REFERÊNCIAS DE NÓS
    // ======================================================

    // Referência ao AnimatedSprite2D (responsável pelas animações)
    private AnimatedSprite2D _sprite;

    // ======================================================
    // MÉTODOS DE INICIALIZAÇÃO
    // ======================================================

    public override void _Ready()
    {
        // Busca o nó AnimatedSprite2D dentro da cena
        _sprite = GetNode<AnimatedSprite2D>("AnimatedSprite2D");
    }

    // ======================================================
    // LOOP PRINCIPAL DE FÍSICA
    // ======================================================

    public override void _PhysicsProcess(double delta)
    {
        // Copiamos a velocidade atual do personagem
        Vector2 velocity = Velocity;

        // 1. Aplica gravidade
        ApplyGravity(ref velocity, delta);

        // 2. Processa pulo
        HandleJump(ref velocity);

        // 3. Processa movimento horizontal
        float direction = GetMovementInput();
        ApplyHorizontalMovement(ref velocity, direction);

        // 4. Atualiza animação com base no movimento
        UpdateAnimation(direction);

        // 5. Aplica movimento final ao personagem
        Velocity = velocity;
        MoveAndSlide();
    }

    // ======================================================
    // SISTEMA DE GRAVIDADE
    // ======================================================

    private void ApplyGravity(ref Vector2 velocity, double delta)
    {
        // Se NÃO estiver no chão, aplica gravidade
        if (!IsOnFloor())
        {
            velocity.Y += _gravity * (float)delta;
        }
    }

    // ======================================================
    // SISTEMA DE PULO
    // ======================================================

    private void HandleJump(ref Vector2 velocity)
    {
        // Se pressionou o botão de pulo E está no chão
        if (Input.IsActionJustPressed("ui_accept") && IsOnFloor())
        {
            velocity.Y = JUMP_FORCE;
        }
    }

    // ======================================================
    // INPUT DO JOGADOR
    // ======================================================

    private float GetMovementInput()
    {
        // Retorna:
        // -1 (esquerda), 1 (direita), 0 (parado)
        return Input.GetAxis("ui_left", "ui_right");
    }

    // ======================================================
    // MOVIMENTO HORIZONTAL
    // ======================================================

    private void ApplyHorizontalMovement(ref Vector2 velocity, float direction)
    {
        if (direction != 0)
        {
            // Move diretamente na direção pressionada
            velocity.X = direction * SPEED;
        }
        else
        {
            // Desacelera suavemente até parar
            velocity.X = Mathf.MoveToward(
                velocity.X, // valor atual
                0,          // destino
                SPEED       // taxa de desaceleração
            );
        }
    }

    // ======================================================
    // SISTEMA DE ANIMAÇÃO
    // ======================================================

    private void UpdateAnimation(float direction)
    {
        // Se o personagem está se movendo horizontalmente
        if (direction != 0)
        {
            PlayAnimation("run");

            // Vira o sprite conforme a direção
            _sprite.FlipH = direction < 0;
        }
        else
        {
            PlayAnimation("idle");
        }
    }

    // ======================================================
    // CONTROLE DE ANIMAÇÃO (EVITA REPETIÇÃO)
    // ======================================================

    private void PlayAnimation(string animationName)
    {
        // Só troca a animação se for diferente da atual
        if (_sprite.Animation != animationName)
        {
            _sprite.Play(animationName);
        }
    }
}