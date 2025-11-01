using System;
using UnityEngine;

namespace GameDataModels
{
    [Serializable]
    public class TaiKhoan
    {
        public string MaTaiKhoan;
        public string TenDangNhap;
        public string Email;
        public string SoDienThoai;
        public string MatKhau; // hash
    }

    [Serializable]
    public class NguoiChoi
    {
        public string MaNguoiChoi;
        public string TenNguoiChoi;
        public int CapDo;
        public string TrangThai;
        public string MaTranDau;
        public string MaTaiKhoan;
    }

    [Serializable]
    public class CaiDat
    {
        public string MaCaiDat;
        public string AmThanh;
        public string DoHoa;
        public string DieuKhien;
        public string MaNguoiChoi;
    }

    [Serializable]
    public class PhongChoi
    {
        public string MaPhongChoi;
        public string TenPhongChoi;
    }

    [Serializable]
    public class PhongChoi_NguoiChoi
    {
        public string MaPhongChoi;
        public string MaNguoiChoi;
        public string MaThamGia;
        public string VaiTro;
        public string ThoiDiemVao;
    }

    [Serializable]
    public class TranDau
    {
        public string MaTranDau;
        public string TrangThaiTranDau;
        public string MaPhongChoi;
    }

    [Serializable]
    public class LichSuTranDau
    {
        public string MaLichSuTranDau;
        public string TrangThaiTranDau;
        public int Score;
        public string MaNguoiChoi;
        public string MaTranDau;
        public string ThoiGian;
    }

    [Serializable]
    public class NhanVat
    {
        public string MaNhanVat;
        public string TenNhanVat;
        public int Health;
        public int Damage;
        public float MovementSpeed;
        public float AttackSpeed;
        public string TrangThaiNhanVat;
    }

    [Serializable]
    public class NhanVat_NguoiChoi
    {
        public string MaNhanVat;
        public string MaNguoiChoi;
        public string MaSoHuu;
        public string TrangThaiSoHuu;
    }

    [Serializable]
    public class VatPham
    {
        public string MaVatPham;
        public string TenVatPham;
        public string LoaiVatPham;
        public int GiaTri;
        public string MaNguoiChoi;
        public string MaTranDau;
    }

    [Serializable]
    public class TranDau_NhanVat
    {
        public string MaTranDau;
        public string MaLichSuTranDau;
        public string MaChiTiet;
    }
}

