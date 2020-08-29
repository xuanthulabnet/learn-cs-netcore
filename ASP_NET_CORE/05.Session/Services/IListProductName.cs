using System.Collections.Generic;
using System.Collections;

  public interface IListProductName
  {
        // Trả về danh sách các tên
        IEnumerable<string> GetNames();
  }