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
        //Bir tak�m fizik i�lemleri, yapraktan d��t��� zaman ve kozadan ��kt��� zamanki i�lemler.
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("cube"))
        {
            //Finish �izgisine girdi�i zaman. Distance scriptindeki skor hesaplama fonksiyonuna y�nlendiriyorum.
            distanceSc.Calculate();
            transform.position = new Vector3(0.055f, transform.position.y, transform.position.z);
            //Karakterin pozisyonunu belirli bir noktaya at�yorum ki tam a�a� dal�na oturmu� halde ��ks�n.
            gameObject.transform.rotation = Quaternion.Euler(86, 0, 0);
            //Rotasyonuyla oynuyorum.
            cam.SetTrigger("Cam");
            //Finish �izgisine gelince kameram�n animasyonunu oynat�yorum.
        }
        if (other.gameObject.CompareTag("cocoon"))
        {
            distanceSc.ButterFly();
            caterpillar.SetActive(false);
            //Kelebek partik�l sistemini �al��t�r�yorum.
        }
    }

}
