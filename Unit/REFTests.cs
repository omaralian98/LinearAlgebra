namespace Unit
{
    public class REFTests
    {
        [Fact]
        public void Test1()
        {
            decimal[,] matrix = { {5, 1, 2 }, {1, 3, 7}, {2, 7, 6} };
            decimal[,] expectedValue = { {5, 1, 2 }, {0, 14m/5, 33m/5}, {0, 0, -145m/14} };
            decimal[,] realValue = Linear.REF(matrix);
            Assert.Equal(expectedValue, realValue);
        }

        [Fact]
        public void Test2()
        {
            double[,] matrix = { { 5, 6, 6 }, { 1, 9, 5 }, { 4, 5, 9 } };
            decimal[,] expectedValue = { { 5, 6, 6 }, { 0, 39m/5, 19m/5 }, { 0, 0, 160m/39 } };
            decimal[,] realValue = Linear.REF(matrix);
            Assert.Equal(expectedValue, realValue);
        }
        [Fact]
        public void Test3()
        {
            string[,] matrix = { { "10/2", "1", "2" }, { "1", "3", "7" }, { "2", "7", "6" } };
            decimal[,] expectedValue = { { 5, 1, 2 }, { 0, 14m / 5, 33m / 5 }, { 0, 0, -145m / 14 } };
            decimal[,] realValue = Linear.REF(matrix);
            Assert.Equal(expectedValue, realValue);
        }
        [Fact]
        public void Test4()
        {
            string[,] matrix = { { "10/2", "1", "2" }, { "1", "3", "7" }, { "2", "7", "6" } };
            string[,] expectedValue = { { "5", "1", "2" }, { "0", "14/5", "33/5" }, { "0", "0", "-145/14" } };
            string[,] realValue = Linear.REFAsString(matrix);
            Assert.Equal(expectedValue, realValue);
        }
        [Fact]
        public void Test5()
        {
            string[,] matrix = { { "2.5", "1", "2" }, { "1", "3", "7" }, { "2", "7", "6" } };
            Assert.Throws<ArithmeticException>(() => Linear.REFAsString(matrix));
        }
        [Fact]
        public void Test6()
        {
            string[,] matrix = { { "10/0", "1", "2" }, { "1", "3", "7" }, { "2", "7", "6" } };
            Assert.Throws<DivideByZeroException>(() => Linear.REFAsString(matrix));
        }
        [Fact]
        public void Test7()
        {
            string[,] matrix = { { "1", "1", "2" }, { "1", "3", "7" }, { "2", "6", "6" } };
            string[] coe = { "0", "39/5" };
            Assert.Throws<ArgumentException>(() => Linear.REFAsString(matrix, coe));
        }
        [Fact]
        public void Test8()
        {
            decimal[,] matrix =  { { 1, 3 }, { 9, 3 }, { 1, 9 } };
            decimal[,] expectedValue = { { 1, 3 }, { 0, -24 }, { 0, 0 } }; 
            var realValue = Linear.REF(matrix);
            Assert.Equal(expectedValue, realValue);
        }
        [Fact]
        public void Test9()
        {
            decimal[,] matrix = { { 3, 5}, { 8, 5 }, { 9, 8 }, { 1, 6 } };
            string[,] expectedValue = { { "3", "5" }, { "0", "-25/3" }, { "0", "0" }, { "0", "0" } }; 
            var realValue = Linear.REFAsString(matrix);
            Assert.Equal(expectedValue, realValue);
        }
        [Fact]
        public void Testa1()
        {
            decimal[,] matrix = { { 2, 2, 1, 7 }, { 7, 0, 9, 7 } };
            string[,] expectedValue = { { "2", "2", "1", "7" }, { "0", "-7", "11/2", "-35/2" } };
            var realValue = Linear.REFAsString(matrix);
            Assert.Equal(expectedValue, realValue);
        }
        [Fact]
        public void Testa2()
        {
            decimal[,] matrix = { { 1, 4, 1, 5 }, { 1, 4, 0, 5 }, { 2, 8, 7, 6 } };
            decimal[,] expectedValue = { { 1, 4, 1, 5 }, { 0, 0, -1, 0 }, { 0, 0, 0, -4 } };
            var realValue = Linear.REF(matrix);
            Assert.Equal(expectedValue, realValue);
        }
        [Fact]
        public void Testa3()
        {
            decimal[,] matrix = { { 1, 1, 2 }, { 1, 3, 7 }, { 2, 6, 6 } };
            decimal[] coefficient = { 1, 1, 1 };
            decimal[,] expectedValue = { { 1, 1, 2 }, { 0, 2, 5 }, { 0, 0, -8 } };
            decimal[] expectedValueCoe = { 1, 0, -1 };
            var (realValue, realValueCoe) = Linear.REF(matrix, coefficient);
            Assert.Equal(expectedValue, realValue);
            Assert.Equal(expectedValueCoe, realValueCoe);
        }
        [Fact]
        public void Testa4()
        {
            decimal[,] matrix = { { 1, 0, 3, 7, 2 }, { 4, 6, 9, 5, 4 }, { 4, 9, 7, 6, 0 }, { 6, 6, 7, 5, 5 }, { 2, 4, 9, 7, 1 } };
            decimal[,] expectedValue = { { 1, 0, 3, 7, 2 }, { 0, 6, -3, -23, -4 }, { 0, 0, -1m / 2, 25m / 2, -2 }, { 0, 0, 0, -214, 29 }, { 0, 0, 0, 0, -727m / 321 } };
            var realValue = Linear.REF(matrix);
            Assert.Equal(expectedValue, realValue);
        }
        [Fact]
        public void Testa5()
        {
            decimal[,] matrix =
            {
                { 1, 1, 1, 1, 2 },
                { -1, -1, -1, 1, 2 },
                { 3, 3, 3, 4, 7 }
            };
            decimal[,] expectedValue =
            {
                { 1, 1, 1, 1, 2 },
                { 0, 0, 0, 2, 4 },
                { 0, 0, 0, 0, -1 }
            };
            var realValue = Linear.REF(matrix);
            Assert.Equal(expectedValue, realValue);
        }
    }
}