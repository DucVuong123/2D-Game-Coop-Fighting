using TMPro;
using UnityEngine;

public class Players_Index_List : MonoBehaviour
{
    public TextMeshProUGUI txtName;
    public TextMeshProUGUI txtLevel;
    public TextMeshProUGUI txtStatus;

    public void SetData(GameDataModels.NguoiChoi data)
    {
        txtName.text = data.TenNguoiChoi;
        txtLevel.text = "Cấp: " + data.CapDo.ToString();
        txtStatus.text = "Trạng thái: " + data.TrangThai;
    }
}
