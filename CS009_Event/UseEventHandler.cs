using System;

namespace CS009 {

  // Xây dựng lớp MyEventArgs kế thừa từ EventArgs
  public class MyEventArgs : EventArgs {
    public MyEventArgs (string data) {
      this.data = data;
    }
    // Lưu dữ liệu gửi đi từ publisher
    private string data;

    public string Data {
      get { return data; }
    }
  }

  // Xây dựng lớp, phát đi sự kiện (data)
  public class ClassA {
    // Tạo Event với EventHandler
    public event EventHandler<MyEventArgs> event_news;

    public void Send () {
      event_news?.Invoke (this, new MyEventArgs ("Có tin mới Abc ..."));
    }
  }

  public class ClassB {
    public void Sub (ClassA p) 
    {
      p.event_news += ReceiverFromPublisher;
    }

    private void ReceiverFromPublisher (object sender, MyEventArgs e) 
    {
      Console.WriteLine ("ClassB: " + e.Data);
    }
  }
  public class ClassC {
    public void Sub (ClassA p) 
    {
      p.event_news += ReceiverFromPublisher;
    }

    private void ReceiverFromPublisher (object sender, MyEventArgs e) 
    {
      Console.WriteLine ("ClassC: " + e.Data);
    }
  }

}