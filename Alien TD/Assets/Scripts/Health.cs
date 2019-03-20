using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{

    [SerializeField] GameObject UIPrefab;
    public float SetHealth = 100f;
    public Transform target;
    Canvas canvas;
    Transform ui;
    Image healhtSlider;
    Transform cam;
    [SerializeField] float yoffset = -10;
    [SerializeField] float xoffset = -3;
    [SerializeField] float zoffset = 7;

    [SerializeField]
    public float Hp
    {
        get { return _Health; }
        set
        {
            if (value == SetHealth)
                healhtSlider.fillAmount  = 1f;
            float temp = value;
            if (value < -10)
                temp += Armor;
            if (temp < 0)
                _Health += temp;
            else _Health += value;
            float healthPercent = _Health / SetHealth;
            healhtSlider.fillAmount = healthPercent;
        }
    }
    float _Health;

    float Armor;

    private void Start()
    {
        Armor = Random.Range(0, 20);
        canvas = GameManager.Instance.HealthCanvas;
        cam = Camera.main.transform;
        ui = Instantiate(UIPrefab, canvas.transform).transform;
        healhtSlider = ui.GetChild(0).GetComponent<Image>();
        Hp = SetHealth;
    }

    private void LateUpdate()
    {
        ui.position = target.position + new Vector3(xoffset, yoffset, zoffset);
        ui.forward = -cam.forward;
    }

    public void CloseHPUI()
    {
        Destroy(healhtSlider);
    }

}
