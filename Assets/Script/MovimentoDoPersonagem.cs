
using UnityEngine;
using UnityEngine.InputSystem;

public class MovimentoDoPersonagem : MonoBehaviour
{
    public float velocidade = 5f;

    private Animator animator;
    private string animacaoAtual = "";

    void Start()
    {
        animator = GetComponent<Animator>();

        if (animator == null)
        {
            Debug.LogError("Adicione um Animator ao personagem!");
        }
    }

    void Update()
    {
        if (animator == null) return;

        float movimento = 0f;

        if (Keyboard.current != null)
        {
            if (Keyboard.current.aKey.isPressed ||
                Keyboard.current.leftArrowKey.isPressed)
            {
                movimento = -1f;
            }
            else if (Keyboard.current.dKey.isPressed ||
                     Keyboard.current.rightArrowKey.isPressed)
            {
                movimento = 1f;
            }
        }

        // Movimenta o personagem
        transform.position += new Vector3(
            movimento * velocidade * Time.deltaTime,
            0f,
            0f
        );

        // Escolhe a animação
        string proximaAnimacao =
            movimento != 0f ? "Walk_work" : "Idle_work";

        // Só troca se a animação for diferente da atual
        if (animacaoAtual != proximaAnimacao)
        {
            animator.Play(proximaAnimacao);
            animacaoAtual = proximaAnimacao;
        }

        // Vira o personagem na direção do movimento
        if (movimento != 0f)
        {
            Vector3 escala = transform.localScale;
            escala.x = Mathf.Abs(escala.x) * Mathf.Sign(movimento);
            transform.localScale = escala;
        }
    }
}

