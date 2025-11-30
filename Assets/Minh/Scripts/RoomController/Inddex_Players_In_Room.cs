using TMPro;
using UnityEngine;

public class Inddex_Players_In_Room : MonoBehaviour
{
    public TextMeshProUGUI txtName;
    public TextMeshProUGUI txt_char_select;

    public void SetData(string TenNguoiChoi = "null", string TenNhanVatDuocChon = "null")
    {
        txtName.text = TenNguoiChoi;
        txt_char_select.text = TenNhanVatDuocChon;
    }
}
