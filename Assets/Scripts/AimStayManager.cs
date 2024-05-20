using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class AimStayManager : MonoBehaviour
{
    public Cinemachine.AxisState xAxis, yAxis;
    [SerializeField] private Transform camFollowPos;
    [SerializeField] private float smoothSpeed = 10f;

    void Start()
    {
        // Eğer gerekirse buraya başlangıç kodları eklenebilir
    }

    void Update()
    {
        xAxis.Update(Time.deltaTime);
        yAxis.Update(Time.deltaTime);
    }

    private void LateUpdate()
    {
        // xAxis ve yAxis değerlerini kullanarak yeni dönüş açılarını hesapla
        float newRotationY = Mathf.LerpAngle(transform.eulerAngles.y, xAxis.Value, Time.deltaTime * smoothSpeed);
        float newRotationX = Mathf.LerpAngle(camFollowPos.localEulerAngles.x, yAxis.Value, Time.deltaTime * smoothSpeed);

        // camFollowPos'un y ve z eksenlerindeki dönüşlerini koruyarak sadece x eksenindeki dönüşünü güncelle
        camFollowPos.localEulerAngles = new Vector3(newRotationX, camFollowPos.localEulerAngles.y, camFollowPos.localEulerAngles.z);
        
        // transform'un x ve z eksenlerindeki dönüşlerini koruyarak sadece y eksenindeki dönüşünü güncelle
        transform.eulerAngles = new Vector3(transform.eulerAngles.x, newRotationY, transform.eulerAngles.z);
    }
}