namespace Unit
{
    public class RankTests
    {
        [Fact]
        public void Test1()
        {
            decimal[,] matrix = { { 5, 1, 2 }, { 1, 3, 7 }, { 2, 7, 6 } };
            int expectedValue = 3;
            int realValue = Linear.Rank(matrix);
            Assert.Equal(expectedValue, realValue);
        }

        [Fact]
        public void Test2()
        {
            decimal[,] matrix = { { 5, 6, 6 }, { 1, 9, 5 }, { 4, 5, 9 } };
            int expectedValue = 3;
            int realValue = Linear.Rank(matrix);
            Assert.Equal(expectedValue, realValue);
        }
    }
}
