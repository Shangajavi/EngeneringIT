using System;
using System.Collections;
using UnityEngine;
using TMPro;

public class Dialogo : MonoBehaviour
{
    private bool isInRange = false;
    private bool isTalking = false;
    private int lineasIndex;
    [SerializeField] private float tiempoTexto = 0.05f;
    [SerializeField] private GameObject exlamationMark;
    [SerializeField] private GameObject panelDialogo;
    [SerializeField] private string[] lineas;
    [SerializeField] private TMP_Text dialogoText;
    [SerializeField] private AudioClip sonidoLetras;
    private SpriteRenderer spRender;
    
    private AudioSource audioSound;

    private void Awake()
    {
        spRender = GetComponent<SpriteRenderer>();
        audioSound = GetComponent<AudioSource>();
        exlamationMark.SetActive(false);
        spRender.flipX = true;
        panelDialogo.SetActive(false);
    }



    void Update()
    {
        if (isInRange == true && Input.GetKeyDown(KeyCode.Q))
        {
            if (!isTalking)
            { 
                EmpezarDialogo();
            }
            else if (dialogoText.text == lineas[lineasIndex])
            {
                SiguinteLinea();
            }
            else
            {
                StopAllCoroutines();
                dialogoText.text = lineas[lineasIndex];
            }
            
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            isInRange = true;
            exlamationMark.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            isInRange = false;
            exlamationMark.SetActive(false);
        }
    }
    private void EmpezarDialogo()
    {
        isTalking = true;
        panelDialogo.SetActive(true);
        exlamationMark.SetActive(false);
        lineasIndex = 0;
        Time.timeScale = 0;
        StartCoroutine(MostrarLineas());
    }

    private void SiguinteLinea()
    {
        lineasIndex++;
        if (lineasIndex < lineas.Length)
        {
            StartCoroutine(MostrarLineas());
        }
        else
        {
            isTalking = false;
            panelDialogo.SetActive(false);
            exlamationMark.SetActive(true);
            Time.timeScale = 1;
        }
    }
    private IEnumerator MostrarLineas()
    {
        dialogoText.text = string.Empty;
        foreach (char ch in lineas[lineasIndex])
        {

            dialogoText.text += ch;

            if (sonidoLetras != null && audioSound != null)
            {
                audioSound.PlayOneShot(sonidoLetras, 1f);
            }

            yield return new WaitForSecondsRealtime(tiempoTexto);

        }
    }
}
