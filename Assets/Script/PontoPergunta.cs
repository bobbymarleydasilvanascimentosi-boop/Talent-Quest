
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PontoPergunta : MonoBehaviour
{
    public GameObject painelPergunta;
    public TMP_Text textoPergunta;
    public Button[] botoesRespostas;

    [TextArea]
    public string pergunta = "Qual atitude é importante no ambiente de trabalho?";

    public string[] respostas =
    {
        "Trabalhar em equipe",
        "Respeitar os colegas",
        "Ignorar as tarefas",
        "Desrespeitar o chefe"
    };

    public int respostaCorreta = 0;

    private bool ativado = false;
    private MovimentoDoPersonagem movimento;

    void Start()
    {
        painelPergunta.SetActive(false);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (ativado || !other.CompareTag("Player"))
            return;

        ativado = true;

        movimento = other.GetComponent<MovimentoDoPersonagem>();

        if (movimento != null)
            movimento.enabled = false;

        painelPergunta.SetActive(true);
        textoPergunta.text = pergunta;

        for (int i = 0; i < botoesRespostas.Length; i++)
        {
            int indice = i;
            Button botao = botoesRespostas[i];

            botao.onClick.RemoveAllListeners();

            if (i < respostas.Length)
            {
                botao.gameObject.SetActive(true);

                TMP_Text texto = botao.GetComponentInChildren<TMP_Text>();

                if (texto != null)
                    texto.text = respostas[i];

                botao.onClick.AddListener(() => Responder(indice));
            }
        }
    }

    void Responder(int indice)
    {
        if (indice == respostaCorreta)
            Debug.Log("Resposta correta!");
        else
            Debug.Log("Resposta incorreta!");

        painelPergunta.SetActive(false);

        if (movimento != null)
            movimento.enabled = true;
    }
}
