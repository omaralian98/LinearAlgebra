namespace Unit
{
    public class REFTests
    {
        [Fact]
        public void Test1()
        {
            decimal[,] matrix = { {5, 1, 2 }, {1, 3, 7}, {2, 7, 6} };
            decimal[,] expectedValue = { {5, 1, 2 }, {0, 14m/5, 33m/5}, {0, 0, -145m/14} };
            decimal[,] realValue = matrix.REF();
            Assert.Equal(expectedValue, realValue);
        }

        [Fact]
        public void Test2()
        {
            double[,] matrix = { { 5, 6, 6 }, { 1, 9, 5 }, { 4, 5, 9 } };
            decimal[,] expectedValue = { { 5, 6, 6 }, { 0, 39m/5, 19m/5 }, { 0, 0, 160m/39 } };
            decimal[,] realValue = matrix.REF();
            Assert.Equal(expectedValue, realValue);
        }
        [Fact]
        public void Test3()
        {
            string[,] matrix = { { "10/2", "1", "2" }, { "1", "3", "7" }, { "2", "7", "6" } };
            decimal[,] expectedValue = { { 5, 1, 2 }, { 0, 14m / 5, 33m / 5 }, { 0, 0, -145m / 14 } };
            decimal[,] realValue = matrix.REF();
            Assert.Equal(expectedValue, realValue);
        }
        [Fact]
        public void Test4()
        {
            string[,] matrix = { { "10/2", "1", "2" }, { "1", "3", "7" }, { "2", "7", "6" } };
            string[,] expectedValue = { { "5", "1", "2" }, { "0", "14/5", "33/5" }, { "0", "0", "-145/14" } };
            string[,] realValue = matrix.REFAsString();
            Assert.Equal(expectedValue, realValue);
        }
        [Fact]
        public void Test5()
        {
            string[,] matrix = { { "2.5", "1", "2" }, { "1", "3", "7" }, { "2", "7", "6" } };
            Assert.Throws<ArithmeticException>(() => matrix.REFAsString());
        }
        [Fact]
        public void Test6()
        {
            string[,] matrix = { { "10/0", "1", "2" }, { "1", "3", "7" }, { "2", "7", "6" } };
            Assert.Throws<DivideByZeroException>(() => matrix.REFAsString());
        }
        [Fact]
        public void Test7()
        {
            string[,] matrix = { { "1", "1", "2" }, { "1", "3", "7" }, { "2", "6", "6" } };
            string[] coe = { "0", "39/5" };
            Assert.Throws<ArgumentException>(() => matrix.REFAsString(coe));
        }
    }
}