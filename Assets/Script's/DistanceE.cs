using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using DG.Tweening;

public class DistanceE : MonoBehaviour
{
    public Vector3 oldPosition;
    public float distance = 0f;
    public Rigidbody catRigid;
    public Slider slider;
    [Header("Particle Effect's")]
    public ParticleSystem confetti;
    public ParticleSystem butterfly;
    public TrailRenderer trail;
    public ParticleSystem finishPartic;
    public TrailRenderer trail2;
    [Header("Animator's")]
    public Text failTxt, successTxt;
    public Transform posToGo1, posToGo2;
    public GameObject cam;
    [Header("Script's")]
    public TouchSc touchSc;
    public PlayerManager moveSc;
    
    public bool finishB = true;
    public bool isPlaying;
    void Start()
    {
        Application.targetFrameRate = 60;
        //Optimizasyon i�in fps'i sabitliyorum. Bu cihaz�n bataryadan yeme oran�n� ve �s�nmas�n� azalt�yor.
        oldPosition = transform.position;
        finishB = true;
    }

    void Update()
    {
        if (finishB)
        {
            //Bu k�s�m� anlamak i�in bu scriptle ayn� konumdaki �ema.png dosyaya bak�n�z.
            Vector3 dist = transform.position - oldPosition;
            float distT = dist.magnitude;
            distance += distT;
            slider.value = distance;
            oldPosition = transform.position;
        }
        if (isPlaying)
        {
            cam.transform.LookAt(transform.position);
        }

        if (trail.startWidth >= 0.240f)
        {
            trail.startWidth = 0.240f;
        }
        else
        {
            trail.startWidth += .01f * Time.deltaTime * 2;
        }
        //Burda her saniyede Trail Rendererin widht de�erini art�yorum.(G�zel g�z�ks�n diye)
    }
    public void Calculate()
    {
        //Fizik i�lemlerinden sonra Movement.cs dosyas�ndan buraya geliyorduk.
        trail.emitting = false;
        trail2.emitting = false;
        //Trail Renderer'lerin iz b�rakma �zelli�ini kapat�yorum.
        touchSc.enabled = false;

        if (distance <= 11.49f)
        {
            failTxt.transform.DOMove(posToGo1.position, 1.5f)
                .OnComplete(()=>failTxt.transform.DOMove(posToGo2.position, 1.5f));
            finishB = false;
            catRigid.useGravity = true;
            StartCoroutine(Final(5));
            Fall();

        }
        else if (distance > 11.49f && distance <= 14.04f)
        {
            successTxt.transform.DOMove(posToGo1.position, 1.5f)
            .OnComplete(() => successTxt.transform.DOMove(posToGo2.position, 1.5f));
            finishB = false;
            catRigid.useGravity = false;
            StartCoroutine(Final(8));
            confetti.Play();
        }
        else
        {
            failTxt.transform.DOMove(posToGo1.position, 1.5f)
                .OnComplete(() => failTxt.transform.DOMove(posToGo2.position, 1.5f));
            StartCoroutine(Final(5));

            finishB = false;
            Fall();

            catRigid.useGravity = true;
            //fail.SetTrigger("Fail");
        }
        //�st k�s�mdaki if bloklar� skor i�lemleri ve sonucunda ne olaca��na dair.
    }
    public void ButterFly()
    {
        butterfly.Play();
        //Kelebek partik�l sistemini �al��t�r�yorum.
    }

    public void Fall()
    {
        this.GetComponent<BoxCollider>().isTrigger = false;
        StartCoroutine(Final(5));
        isPlaying = true;
        finishPartic.Play();
        finishPartic.transform.position = new Vector3(this.transform.position.x, this.transform.position.y - 14f, transform.position.z);
        moveSc.enabled = false;
        cam.transform.position = new Vector3(12, 6.9f, -22);
    }
    public IEnumerator Final(int second)
    {
        yield return new WaitForSeconds(second+1f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
