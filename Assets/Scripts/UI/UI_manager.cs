using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_manager : MonoBehaviour
{
    public PlayerController p;

    public int maxLife = 4;
    public Image[] coracao;
    public Sprite cheio;
    public Sprite vazio;
    public Slider _musicSlider, _sfxSlider;

    public Text coinText;

    private void Awake()
    {
        _musicSlider.onValueChanged.AddListener(delegate { OnMusicVolumeChanged(); });
        _sfxSlider.onValueChanged.AddListener(delegate { OnSFXVolumeChanged(); });

        // Carrega as configura��es de volume ao iniciar
        LoadVolumeSettings();
    }

    void Start()
    {
        UpdateLifeBar();
    }

    // Chama essa fun��o sempre que precisar atualizar a barra de vida
    public void UpdateLifeBar()
    {
        // Se p.life for maior que maxLife, ajusta para maxLife
        if (p.life > maxLife)
        {
            p.life = maxLife;
        }

        for (int i = 0; i < coracao.Length; i++)
        {
            // Define o sprite do cora��o como cheio se estiver dentro da vida do jogador, caso contr�rio, vazio
            coracao[i].sprite = (i < p.life) ? cheio : vazio;

            // Habilita ou desabilita o cora��o de acordo com o m�ximo de vida
            coracao[i].enabled = (i < maxLife);
        }
    }

    // Fun��o para atualizar a quantidade de moedas
    public void CoinsAmount(int totalMoedas)
    {
        coinText.text = totalMoedas.ToString();
    }

    // Fun��o para salvar os valores dos sliders de volume
    // Fun��o para salvar os valores dos sliders de volume
    private void SaveVolumeSettings()
    {
        PlayerPrefs.SetFloat("MusicVolume", _musicSlider.value);
        PlayerPrefs.SetFloat("SFXVolume", _sfxSlider.value);
        PlayerPrefs.Save(); // Salva as altera��es
    }


    // Fun��o para carregar os valores dos sliders de volume
    void LoadVolumeSettings()
    {
        // Supondo que voc� salvou os volumes com as chaves "MusicVolume" e "SFXVolume"
        float musicVolume = PlayerPrefs.GetFloat("MusicVolume");
        float sfxVolume = PlayerPrefs.GetFloat("SFXVolume");
        // Ajusta os sliders para refletir os valores salvos
        _musicSlider.value = musicVolume;
        _sfxSlider.value = sfxVolume;

        // Ajusta tamb�m o volume das AudioSource diretamente
        AudioManager.Instance.musicSource.volume = musicVolume;
        AudioManager.Instance.sfxSource.volume = sfxVolume;
    }

    public void OnMusicVolumeChanged()
    {
        float volume = _musicSlider.value;
        PlayerPrefs.SetFloat("MusicVolume", volume);
        AudioManager.Instance.musicSource.volume = volume;
    }

    public void OnSFXVolumeChanged()
    {
        float volume = _sfxSlider.value;
        PlayerPrefs.SetFloat("SFXVolume", volume);
        AudioManager.Instance.sfxSource.volume = volume;
    }



    public void SFXVolume()
    {
        AudioManager.Instance.SFXVolume(_sfxSlider.value);
        SaveVolumeSettings(); // Salva o valor do slider de SFX
    }

    public void MusicVolume()
    {
        AudioManager.Instance.MusicVolume(_musicSlider.value);
        SaveVolumeSettings(); // Salva o valor do slider de m�sica
    }
}
