// Chứa thông tin Email sẽ gửi (Trường hợp này chưa hỗ trợ đính kém file)
public class MailContent
{
    public string To { get; set; }              // Địa chỉ gửi đến
    public string Subject { get; set; }         // Chủ đề (tiêu đề email)
    public string Body { get; set; }            // Nội dung (hỗ trợ HTML) của email

}
