using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerManager : MonoBehaviour
{
    public float upForce = 1.5f;

    public GameObject caterpillar;

    public Animator cam;
    public DistanceE distanceSc;

    private void Start()
    {
        cam.SetTrigger("First");
    }
    void FixedUpdate()
    {
        Move();
    }
    public void Move()
    {
        transform.Translate(Vector3.up * upForce * Time.deltaTime);
        //Karakterimi vector kullanarak hareket ettiriyorum.
    }
    public void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "leaf")
        {
            distanceSc.Calculate();
        }
        if (other.gameObject.tag == "cocoon")
        {
            //caterpillar.SetActive(false);
        }
        //Bir takým fizik iþlemleri, yapraktan düþtüðü zaman ve kozadan çýktýðý zamanki iþlemler.
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("cube"))
        {
            //Finish çizgisine girdiði zaman. Distance scriptindeki skor hesaplama fonksiyonuna yönlendiriyorum.
            distanceSc.Calculate();
            transform.position = new Vector3(0.055f, transform.position.y, transform.position.z);
            //Karakterin pozisyonunu belirli bir noktaya atýyorum ki tam aðaç dalýna oturmuþ halde çýksýn.
            gameObject.transform.rotation = Quaternion.Euler(86, 0, 0);
            //Rotasyonuyla oynuyorum.
            cam.SetTrigger("Cam");
            //Finish çizgisine gelince kameramýn animasyonunu oynatýyorum.
        }
        if (other.gameObject.CompareTag("cocoon"))
        {
            distanceSc.ButterFly();
            caterpillar.SetActive(false);
            //Kelebek partikül sistemini çalýþtýrýyorum.
        }
    }

}
