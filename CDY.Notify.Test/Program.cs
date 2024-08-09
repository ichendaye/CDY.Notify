namespace CDY.Notify.Test
{
    internal class Program
    {
        static void Main(string[] args)
        {
            try
            {
                Bark bark = new Bark("token");
                bark.Send("测试", "测试2");

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            Console.ReadKey();
        }
    }
}