namespace Unit
{
    public class UnitTest1
    {
        [Fact]
        public void Task_REF()
        {
            decimal[,] matrix =
            {
                {5, 1, 2 },
                {1, 3, 7},
                {2, 7, 6}
            };
            decimal[,] expectedValue =
            {
                {5, 1, 2 },
                {0, 14m/5, 33m/5},
                {0, 0, -145m/14}
            };
            decimal[,] result = matrix.REF();
            Assert.Equal(expectedValue, result);
        }
    }
}