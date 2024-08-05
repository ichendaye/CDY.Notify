namespace CDY.Notify.Test
{
    internal class Program
    {
        static void Main(string[] args)
        {
            try
            {
                var notify = new DingTalk("测试",
                                          "https://oapi.dingtalk.com/robot/send?access_token=dbbf5c71ee1fede0a32f5be392487778888fd0f9647a10fb6cc3fd4988c4fa3b",
                                          "SECdf96006fc63a5bd1522dde80e5a00bbb33ed58b8a2eb14a330d71e0f41e632f7");
                //_ = notify.SendTextMsgAsync("123");
                _ = notify.SendErrorMsgAsync(new Exception("gagaga"),true);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            Console.ReadKey();
        }
    }
}