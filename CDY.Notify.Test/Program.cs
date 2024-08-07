namespace CDY.Notify.Test
{
    internal class Program
    {
        static void Main(string[] args)
        {
            try
            {
                Bark bark = new Bark("eEpXGotfiqDbGgawdCCmG9");
                _ = bark.SendAsync("测试", "测试2");
                _ = bark.SendAsync("测试", "测试2", "https://file.chendaye.com/yb.png");

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            Console.ReadKey();
        }
    }
}