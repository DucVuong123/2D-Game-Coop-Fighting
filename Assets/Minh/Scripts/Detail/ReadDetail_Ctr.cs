using System;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;




public class ReadDetail_Ctr : MonoBehaviour
{
    [SerializeField] private GameObject Panel_Detail;
    [SerializeField] private TextMeshProUGUI TieuDe;
   [SerializeField] private TextMeshProUGUI NoiDung;

    public void Onclick_Detail(string detail_type)
    {
        Panel_Detail.SetActive(true);
        switch (detail_type)
        { 
        case "Bo_Binh":
                TieuDe.text = "CHI TIẾT NHÂN VẬT BỘ BINH";
                NoiDung.text = "Mô tả nhân vật bộ binh...";
                break;
            case "Du_Kich":
                TieuDe.text = "CHI TIẾT NHÂN VẬT DU KÍCH";
                NoiDung.text = "Mô tả nhân vật du kích...";
                break;
            case "Map_Tutor1":
                TieuDe.text = "CHI TIẾT MAP HƯỚNG DẪN 1";
                NoiDung.text = "Mô tả map hướng dẫn 1...";
                break;
            case "Map_Tutor2":
                TieuDe.text = "CHI TIẾT MAP HƯỚNG DẪN 1";
                NoiDung.text = "Mô tả map hướng dẫn 2...";
                break;
            case "Map1":
                
                break;
            case "Map2":
                
                break;
            case "Map3":
               
                break;
        }

    }
    public void OnClose() => Panel_Detail.SetActive(false);
}
