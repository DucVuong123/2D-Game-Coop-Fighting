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
                NoiDung.text = "Kỹ năng chính: Nhân vật tăng tốc độ bắn đạn.\n\n" +
                    "Kỹ năng phụ: Khi người chơi ở trạng thái vừa chạy vừa bắn, " +
                    "nhân vật sẽ lướt tiến tới một đoạn ngắn.\n\n" +
                    "Bối cảnh: Người lính trong thời chiến."; 
                break;
            case "Du_Kich":
                TieuDe.text = "CHI TIẾT NHÂN VẬT LÍNH CẬN CHIẾN";
                NoiDung.text =
                    "Kỹ năng chính: Ném 1 quả bom tới vị trí cố định, " +
                    "gây sát thương theo vụ nổ trong một vùng nhỏ.\n\n" +
                    "Kỹ năng phụ: Nhân vật sẽ tiến vào trạng thái tàng hình " +
                    "khi đứng gần bụi cỏ và các vật thể tương tự.\n\n" +
                    "Bối cảnh: Người lính trong thời chiến.";
                break;
            case "Map_Tutor1":
                TieuDe.text = "CHI TIẾT MAP HƯỚNG DẪN 1";
                NoiDung.text =
                    "Tutorial 1 – Làm quen điều khiển cơ bản:\n" +
                    "- Di chuyển trái/phải\n" +
                    "- Nhảy, leo bậc thang\n" +
                    "- Sử dụng vũ khí cơ bản\n" +
                    "Mục tiêu: Người chơi nắm được toàn bộ thao tác di chuyển và tấn công cơ bản.";
                break;
            case "Map_Tutor2":
                TieuDe.text = "CHI TIẾT MAP HƯỚNG DẪN 2";
                NoiDung.text =
                    "Tutorial 2 – Tương tác & phối hợp:\n" +
                    "- Thực hành ném lựu đạn\n" +
                    "- Tương tác vật phẩm\n" +
                    "- Hỗ trợ đồng đội (revive, cover)\n" +
                    "Mục tiêu: Hiểu cơ chế teamwork và sử dụng hiệu quả các kỹ năng hỗ trợ.";
                break;
            case "Map1":
                TieuDe.text = "MÀN 1: XÂM NHẬP RỪNG RẬM";
                NoiDung.text =
                    "Mục tiêu: Tiêu diệt Sĩ Quan Huấn Luyện.\n\n" +
                    "Yêu cầu gameplay:\n" +
                    "- Di chuyển nhanh, nhảy, leo bậc thang.\n" +
                    "- Bắn assault để thu hút (distract) lính.\n" +
                    "- Chiến đấu sinh tồn (combat liên tục).\n\n" +
                    "Cơ chế boss:\n" +
                    "- Dịch chuyển giữa nhiều vị trí.\n" +
                    "- Spawn thêm lính tiểu đội hỗ trợ.";
                break;
            case "Map2":
                TieuDe.text = "MÀN 2: BẪY RẬP BAN ĐẦU";
                NoiDung.text =
                    "Mục tiêu: Phá hủy các vũ khí hạng nặng.\n\n" +
                    "Yêu cầu gameplay:\n" +
                    "- Cận chiến nhóm lính.\n" +
                    "- Sử dụng tàng hình để áp sát.\n" +
                    "- Phá hủy vũ khí hạng nặng.\n" +
                    "- Chiến đấu sinh tồn.\n\n" +
                    "Cơ chế boss:\n" +
                    "- Xe tăng và máy bay tấn công mạnh, sát thương cao.";
                break;
            case "Map3":
                TieuDe.text = "MÀN 3: TRUY KÍCH CHỈ HUY";
                NoiDung.text =
                    "Mục tiêu: Đuổi và đánh bại Chỉ Huy.\n\n" +
                    "Yêu cầu gameplay:\n" +
                    "- Vừa chiến đấu vừa truy đuổi mục tiêu.\n" +
                    "- Dọn đường, tiêu diệt lính cản trở.\n\n" +
                    "Cơ chế boss:\n" +
                    "- Chỉ Huy xuất hiện từng đoạn trên bản đồ.\n" +
                    "- Gọi lính theo từng địa điểm.\n" +
                    "- Khi bị ép đến cuối đường sẽ đầu hàng.";
                break;

        }

    }
    public void OnClose() => Panel_Detail.SetActive(false);
}
