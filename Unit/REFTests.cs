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
            decimal[,] matrix = { { 5, 6, 6 }, { 1, 9, 5 }, { 4, 5, 9 } };
            decimal[,] expectedValue = { { 5, 6, 6 }, { 0, 39m/5, 19m/5 }, { 0, 0, 160m/39 } };
            decimal[,] realValue = matrix.REF();
            Assert.Equal(expectedValue, realValue);
        }
    }
}