using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using mvcblog.Data;
using mvcblog.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace mvcblog.Controllers {
    [Route ("/products")]
    public class ProductController : Controller {
        private readonly ILogger<ProductController> _logger;

        private readonly AppDbContext _context;

        // Key lưu chuỗi json của Cart
        public const string CARTKEY = "cart";

        // Lấy cart từ Session (danh sách CartItem)
        List<CartItem> GetCartItems () {

            var session = HttpContext.Session;
            string jsoncart = session.GetString (CARTKEY);
            if (jsoncart != null) {
                return JsonConvert.DeserializeObject<List<CartItem>> (jsoncart);
            }
            return new List<CartItem> ();
        }

        // Xóa cart khỏi session
        void ClearCart () {
            var session = HttpContext.Session;
            session.Remove (CARTKEY);
        }

        // Lưu Cart (Danh sách CartItem) vào session
        void SaveCartSession (List<CartItem> ls) {
            var session = HttpContext.Session;
            string jsoncart = JsonConvert.SerializeObject (ls);
            session.SetString (CARTKEY, jsoncart);
        }


        public ProductController (ILogger<ProductController> logger, AppDbContext context) {
            _logger = logger;
            _context = context;
        }

        // Hiện thị danh sách sản phẩm, có nút chọn đưa vào giỏ hàng
        public IActionResult Index () {
            var products = _context.Products.ToList ();
            return View (products);
        }

        /// Thêm sản phẩm vào cart
        [Route ("addcart/{productid:int}", Name = "addcart")]
        public IActionResult AddToCart ([FromRoute] int productid) {

            var product = _context.Products
                .Where (p => p.ProductId == productid)
                .FirstOrDefault ();

            if (product == null)
                return NotFound ("Không có sản phẩm");

            // Xử lý đưa vào Cart ...
            var cart = GetCartItems ();
            var cartitem = cart.Find (p => p.product.ProductId == productid);
            if (cartitem != null) {
                // Đã tồn tại, tăng thêm 1
                cartitem.quantity++;
            } else {
                //  Thêm mới
                cart.Add (new CartItem () { quantity = 1, product = product });
            }

            // Lưu cart vào Session
            SaveCartSession (cart);
            // Chuyển đến trang hiện thị Cart
            return RedirectToAction (nameof (Cart));
        }



        /// xóa item trong cart
        [Route ("/removecart/{productid:int}", Name = "removecart")]
        public IActionResult RemoveCart ([FromRoute] int productid) {
            var cart = GetCartItems ();
            var cartitem = cart.Find (p => p.product.ProductId == productid);
            if (cartitem != null) {
                // Đã tồn tại, tăng thêm 1
                cart.Remove(cartitem);
            } 

            SaveCartSession (cart);
            return RedirectToAction (nameof (Cart));
        }

        /// Cập nhật
        [Route ("/updatecart", Name = "updatecart")]
        [HttpPost]
        public IActionResult UpdateCart ([FromForm] int productid, [FromForm] int quantity) {
            // Cập nhật Cart thay đổi số lượng quantity ...
            var cart = GetCartItems ();
            var cartitem = cart.Find (p => p.product.ProductId == productid);
            if (cartitem != null) {
                // Đã tồn tại, tăng thêm 1
                cartitem.quantity = quantity;
            } 
            SaveCartSession (cart);
            // Trả về mã thành công (không có nội dung gì - chỉ để Ajax gọi)
            return Ok();
        }

        // Hiện thị giỏ hàng
        [Route ("/cart", Name = "cart")]
        public IActionResult Cart () {
            return View (GetCartItems());
        }

        [Route ("/checkout")]
        public IActionResult CheckOut ([FromForm] string email, [FromForm] string address) {
            
            // Xử lý khi đặt hàng
            var cart = GetCartItems();
            ViewData["email"] = email;
            ViewData["address"] = address;
            ViewData["cart"] = cart;

            if (!string.IsNullOrEmpty(email)) {
                // hãy tạo cấu trúc db lưu lại đơn hàng và xóa cart khỏi session

                ClearCart();
                RedirectToAction(nameof(Index));
            }

            return View();
        }

    }
}