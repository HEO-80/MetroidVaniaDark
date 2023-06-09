using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;


public class AudioManager : MonoBehaviour
{
    public AudioMixer musicMixer, effectsMixer;
    public AudioSource backGroundMusic, enemyDeath, hit,jump, mainMenu, gameOver, playerDeath, diamon, heart;

    public static AudioManager instance;

    [Range(-80, 10)]
    public float masterVol, effectsVol;
    public Slider masterSlider, effectsSlider;


    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        

        PlayAudio(backGroundMusic);
        masterSlider.value = masterVol;
        effectsSlider.value = effectsVol;

        masterSlider.minValue= -20;
        masterSlider.maxValue = 10;

        effectsSlider.minValue = -20;
        effectsSlider.maxValue = 10;
   
           

    }

    // Update is called once per frame
    void Update()
    {
        MasterVolume();
        EffectsVolume();

    }

    public void MasterVolume()//para manejar el volumen del master
    {
        musicMixer.SetFloat("masterVolume", masterSlider.value);

    }
    public void EffectsVolume()//para manejar el volumen de los efectos
    {
        effectsMixer.SetFloat("effectsVolume", effectsSlider.value);
    }

        public void PlayAudio(AudioSource audio)
    {
        audio.Play();
    }
}
