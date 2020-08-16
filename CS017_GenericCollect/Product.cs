using System;

namespace CS017_GenericCollect {
    public class Product : IComparable<Product>, IFormattable {
        public int ID { set; get; }
        public string Name { set; get; } // tên
        public double Price { set; get; } // giá
        public string Origin { set; get; } // xuất xứ

        public Product (int id, string name, double price, string origin) {
            ID = id;
            Name = name;
            Price = price;
            Origin = origin;
        }

        //Triển khai IComparable, cho biết vị trí sắp xếp so với đối tượng khác
        // trả về 0 - cùng vị trí; trả về > 0 đứng sau other; < 0 đứng trước trong danh sách
        public int CompareTo (Product other) {
            // sắp xếp về giá
            double delta = this.Price - other.Price;
            if (delta > 0) // giá lớn hơn xếp trước
                return -1;
            else if (delta < 0) // xếp sau, giá nhỏ hơn
                return 1;
            return 0;

        }
        // Triển khai IFormattable, lấy chuỗi thông tin của đối tượng theo định dạng
        // format hỗ trợ "O" và "N"
        public string ToString (string format, IFormatProvider formatProvider) {
            if (format == null) format = "O";
            switch (format.ToUpper ()) {
                case "O": // Xuất xứ trước
                    return $"Xuất xứ: {Origin} - Tên: {Name} - Giá: {Price} - ID: {ID}";
                case "N": // Tên xứ trước
                    return $"Tên: {Name} - Xuất xứ: {Origin} - Giá: {Price} - ID: {ID}";
                default: // Quăng lỗi nếu format sai
                    throw new FormatException ("Không hỗ trợ format này");
            }
        }

        // Nạp chồng ToString
        override public string ToString () => $"{Name} - {Price}";

        // Quá tải thêm ToString - lấy chỗi thông tin sản phẩm theo định dạng
        public string ToString (string format) => this.ToString (format, null);

    }

    public class SearchNameProduct {
        string namesearch;
        public SearchNameProduct (string name) {
            namesearch = name;
        }
        // Hàm gán cho delegage
        public bool search (Product p) {
            return p.Name == namesearch;
        }
    }
}

