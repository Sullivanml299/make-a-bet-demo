using NUnit.Framework;
using UnityEngine;

public class MultiplierTest
{
    // A Test behaves as an ordinary method
    [Test]
    public void MultiplierTestSimplePasses()
    {
        int iterations = 100000;
        int typeACount, typeBCount, typeCCount, typeDCount;
        typeACount = typeBCount = typeCCount = typeDCount = 0;
        float aTargetProbability = 0.5f;
        float bTargetProbability = 0.3f;
        float cTargetProbability = 0.15f;
        float dTargetProbability = 0.05f;

        for (int i = 0; i < iterations; i++)
        {
            switch (Multiplier.GetRandomMultplier())
            {
                case var expression when expression <= 0:
                    typeACount++;
                    break;

                case var expression when expression <= 10:
                    typeBCount++;
                    break;

                case var expression when expression <= 64:
                    typeCCount++;
                    break;

                case var expression when expression <= 500:
                    typeDCount++;
                    break;
            }
        }

        float aActualProbability = (float)typeACount / iterations;
        float bActualProbability = (float)typeBCount / iterations;
        float cActualProbability = (float)typeCCount / iterations;
        float dActualProbability = (float)typeDCount / iterations;

        Assert.LessOrEqual(Mathf.Abs(aTargetProbability - aActualProbability), 0.01f);
        Assert.LessOrEqual(Mathf.Abs(bTargetProbability - bActualProbability), 0.01f);
        Assert.LessOrEqual(Mathf.Abs(cTargetProbability - cActualProbability), 0.01f);
        Assert.LessOrEqual(Mathf.Abs(dTargetProbability - dActualProbability), 0.01f);
    }

}
