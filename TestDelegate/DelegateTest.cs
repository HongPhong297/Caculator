namespace TestDelegate
{
    public delegate int PhepToan(int a, int b);

    public class DelegateTest
    {

        public static int Cong(int a, int b) => a + b;
        public static int Tru(int a, int b) => a - b;
    }
}
