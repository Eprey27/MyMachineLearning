using System;
using MyMachineLearning;
using Xunit;

namespace MyMachineLearning.Tests
{
    public class GenerarDatosXORTests
    {
        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        public void GenerarDatosXOR_RegresaSalidasEsperadasParaTodosLosCasos(int numEntradas)
        {
            double[][] salidas = Program.GenerarDatosXOR(numEntradas);

            int numEjemplosEsperados = 1 << numEntradas;
            Assert.Equal(numEjemplosEsperados, salidas.Length);

            for (int i = 0; i < salidas.Length; i++)
            {
                Assert.Single(salidas[i]);

                string binario = Convert.ToString(i, 2).PadLeft(numEntradas, '0');
                int xorEsperado = 0;

                foreach (char bit in binario)
                {
                    xorEsperado ^= bit == '1' ? 1 : 0;
                }

                Assert.Equal(xorEsperado, salidas[i][0], 5);
            }
        }
    }
}
