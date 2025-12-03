using TMPro;
using UnityEngine;
using PurrNet;
public class Inddex_Players_In_Room : NetworkBehaviour
{
    public TextMeshProUGUI txtName;
    public TextMeshProUGUI txt_char_select;
    public SyncVar<string> _name = new();
    public SyncVar<string> _select = new();
    protected override void OnSpawned()
    {
        base.OnSpawned();
        txtName.text = _name.value;
        txt_char_select.text = _select.value;
        _name.onChanged += _name_onChanged;
        _select.onChanged += _select_onChanged;
    }

    private void _select_onChanged(string obj)
    {
        txt_char_select.text = _select.value;
    }

    private void _name_onChanged(string obj)
    {
        txtName.text = _name.value;
    }

    [ServerRpc(requireOwnership:false)]
    public void SetData(string TenNguoiChoi = "null", string TenNhanVatDuocChon = "null")
    {
        _name.value = TenNguoiChoi;
        _select.value = TenNhanVatDuocChon;
        txtName.text = _name.value;
        txt_char_select.text = _select.value;
    }

}
