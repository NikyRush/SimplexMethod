using System.Collections.Generic;

namespace SimplexMethod
{
    public class Coefficient
    {
        private List<float> _listCoefficient;
        public Coefficient()
        {
            _listCoefficient = new List<float>();
        }

        public void AddCoefficient(float result)
        {
            _listCoefficient.Add(result);
        }

        public ref List<float> GetRefListCoefficient { get { return ref _listCoefficient; } }
    }

    public class TargetFunction : Coefficient
    {
        public string MaxMin { get; set; }
    }

    public class LimitedFunction : Coefficient
    {
        public string SignFunction { get; set; }
        public float FreeCoefficient { get; set; }
        public int Basis { get; set; } //Индекс при X, который является базисом

        public void MultiplicationData(float multiplier)
        {
            FreeCoefficient *= multiplier;
            for (int indexCoefficient = 0; indexCoefficient < GetRefListCoefficient.Count; indexCoefficient++)
                GetRefListCoefficient[indexCoefficient] *= multiplier;
        }
    }

}
