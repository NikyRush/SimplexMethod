using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Windows.Forms;

namespace SimplexMethod
{
    public partial class MainForm : Form
    {
        public int countVariable;
        public int countLimited;
        public int countNewCoefficientAsBasis;

        public TargetFunction targetFunction;
        public List<LimitedFunction> listLimitedFunction;
        public List<float> listDelta;

        public List<Form> listForm;
        public string tempText;
        public int countForm;

        public MainForm()
        {
            InitializeComponent();
            GenerateTargetFunction();
            GenerateLimitedFunction();

            targetFunction = new TargetFunction();
            listLimitedFunction = new List<LimitedFunction>();
            listDelta = new List<float>();
            listForm = new List<Form>();
            countForm = 0;
            tempText = "";
        }

        //Вывод результата
        private void PrintResult()
        {
            string message = "";
            float temp;
            int countAllCoefficient = countVariable + countNewCoefficientAsBasis;
            for (int countX = 1; countX <= countAllCoefficient; countX++)
            {
                temp = 0;
                message += "X" + countX.ToString() + " = ";
                GetResult(ref temp, countX);
                message += temp.ToString() + "\r\n";
            }
            message += "F = " + listDelta[listDelta.Count - 1].ToString();
            tempText = message;
            RecordStep("Ответ");
            MessageBox.Show(message);
        }
        private void GetResult(ref float temp, int countX)
        {
            foreach (LimitedFunction function in listLimitedFunction)
            {
                if (function.Basis == countX)
                {
                    temp = function.FreeCoefficient;
                    break;
                }
            }
        }

        //6-й шаг. Переход к оптимальному решению
        private void GetOptimality(ref string flag)
        {
            int indexDelta = 0;
            if (targetFunction.MaxMin.Equals("max"))
            {
                tempText += "При максимизации ищем столбец с наименьшей дельтой.\r\n";
                GetMinDelta(ref indexDelta);
            }
            else
            {
                tempText += "При минимизации ищем столбец с наибольшей дельтой.\r\n";
                GetMaxDelta(ref indexDelta);
            }

            int indexLimitedFunction = -1;
            GetSimplexRelation(in indexDelta, ref indexLimitedFunction);
            if (indexLimitedFunction == -1)
            {
                flag = "error";
                tempText += "Нет отношений => Целевая функция не ограничена и решения не существует.";
            }
            else
            {
                tempText += $"Делим {indexLimitedFunction+1} ограничение на разрешающий элемент = {listLimitedFunction[indexLimitedFunction].GetRefListCoefficient[indexDelta]}:\r\n";
                listLimitedFunction[indexLimitedFunction].MultiplicationData(1 /
                    listLimitedFunction[indexLimitedFunction].GetRefListCoefficient[indexDelta]);
                PrintLimitedFunction(indexLimitedFunction);
                tempText += "Из других строк вычитаем текущую, умноженную на соотвествующий элемент:\r\n";
                SubtractionFunction(indexLimitedFunction, indexDelta);
                for (int indexFunction = 0; indexFunction < listLimitedFunction.Count; indexFunction++)
                    PrintLimitedFunction(indexFunction);
                listLimitedFunction[indexLimitedFunction].Basis = indexDelta + 1;
                tempText += $"Меняем базис текущего ограничения на x{listLimitedFunction[indexLimitedFunction].Basis}\r\n";
            }
            RecordStep("Переход к оптимальному решению");
        }
        private void GetSimplexRelation(in int indexDelta, ref int indexLimitedFunction)
        {//Возможно непонятка с отрицательным свободным членом, в теории, такого быть не должно
            tempText += $"Найдена d{indexDelta+1} = {listDelta[indexDelta]}\r\n" +
                $"Вычислим симплекс-отношения:\r\n";
            float minSimplexRelation = 1000000;
            float tempValue;
            int indexFunctinNow = 0;
            foreach (LimitedFunction function in listLimitedFunction)
            {
                if (function.GetRefListCoefficient[indexDelta] > 0)
                {
                    tempValue = function.FreeCoefficient / function.GetRefListCoefficient[indexDelta];
                    tempText += $"S{indexFunctinNow + 1} = {function.FreeCoefficient} " +
                        $"/ {function.GetRefListCoefficient[indexDelta]} " +
                        $"= {tempValue}\r\n";
                    if (tempValue < minSimplexRelation)
                    {
                        minSimplexRelation = tempValue;
                        indexLimitedFunction = indexFunctinNow;
                    }
                }
                indexFunctinNow++;
            }
            if(indexLimitedFunction != -1)
            {
                tempText += $"Среди них найдем наименьшее отношение = {minSimplexRelation}\r\n";
            }
        }
        private void GetMinDelta(ref int indexDelta)
        {
            float minDelta = listDelta[0];
            for (int index = 1; index < listDelta.Count - 1; index++)
                if (listDelta[index] < minDelta)
                {
                    minDelta = listDelta[index];
                    indexDelta = index;
                }
        }
        private void GetMaxDelta(ref int indexDelta)
        {
            float maxDelta = listDelta[0];
            for (int index = 1; index < listDelta.Count - 1; index++)
                if (listDelta[index] > maxDelta)
                {
                    maxDelta = listDelta[index];
                    indexDelta = index;
                }
        }

        //5-й шаг. Проверка на оптимальность
        private string CheckOptimality()
        {
            if (targetFunction.MaxMin.Equals("max"))
            {
                tempText += "При максимизации функции, решение оптимально, если отсутствуют отрицательные дельты.\r\n";
                for (int indexDelta = 0; indexDelta < (listDelta.Count - 1); indexDelta++)
                    if (listDelta[indexDelta] < 0)
                    {
                        tempText += $"Решение не оптимально, найдена отрицательная дельта: d{indexDelta+1} = {listDelta[indexDelta]}";
                        return "false";
                    }
            }
            else
            {
                tempText += "При минимизации функции, решение оптимально, если отсутствуют положительные дельты.\r\n";
                for (int indexDelta = 0; indexDelta < (listDelta.Count - 1); indexDelta++)
                    if (listDelta[indexDelta] > 0)
                    {
                        tempText += $"Решение не оптимально, найдена положительная дельта: d{indexDelta + 1} = {listDelta[indexDelta]}";
                        return "false";
                    }
            }
            tempText += "Решение оптимально, идём дальше.";
            return "true";
        }

        //4-й шаг. Вычисление дельт
        private void CalculatingDelta()
        {
            listDelta.Clear();
            int countAllCoefficient = countVariable + countNewCoefficientAsBasis;
            double tempDelta;
            int indexBasis;
            int indexColumn;
            //Дельты при коэффициентах
            for (indexColumn = 0; indexColumn < countAllCoefficient; indexColumn++)
            {
                tempText += $"d{indexColumn + 1} = ";
                tempDelta = 0;
                for (int indexLine = 0; indexLine < countLimited; indexLine++)
                {
                    indexBasis = listLimitedFunction[indexLine].Basis - 1;
                    tempDelta += listLimitedFunction[indexLine].GetRefListCoefficient[indexColumn]
                        * targetFunction.GetRefListCoefficient[indexBasis];
                    if (indexLine != 0)
                        tempText += "+ ";
                    if (listLimitedFunction[indexLine].GetRefListCoefficient[indexColumn] < 0)
                        tempText += "( ";
                    tempText += $"{listLimitedFunction[indexLine].GetRefListCoefficient[indexColumn]} ";
                    if (listLimitedFunction[indexLine].GetRefListCoefficient[indexColumn] < 0)
                        tempText += ") ";
                    tempText += "* ";
                    if (targetFunction.GetRefListCoefficient[indexBasis] < 0)
                        tempText += "( ";
                    tempText += $"{targetFunction.GetRefListCoefficient[indexBasis]} ";
                    if (targetFunction.GetRefListCoefficient[indexBasis] < 0)
                        tempText += ") ";
                }
                tempDelta -= targetFunction.GetRefListCoefficient[indexColumn];
                tempText += $"- ";
                if(targetFunction.GetRefListCoefficient[indexColumn] < 0)
                    tempText += "( ";
                tempText += $"{targetFunction.GetRefListCoefficient[indexColumn]} ";
                if (targetFunction.GetRefListCoefficient[indexColumn] < 0)
                    tempText += ") ";
                tempText += $"= {tempDelta}\r\n";
                listDelta.Add((float)tempDelta);
            }

            tempDelta = 0;
            tempText += $"d{indexColumn + 1} = ";
            //дельта при свободных коэффициентах
            for (int indexLine = 0; indexLine < countLimited; indexLine++)
            {
                indexBasis = listLimitedFunction[indexLine].Basis - 1;
                tempDelta += listLimitedFunction[indexLine].FreeCoefficient
                    * targetFunction.GetRefListCoefficient[indexBasis];
                if (indexLine != 0)
                    tempText += "+ ";
                tempText += $"{listLimitedFunction[indexLine].FreeCoefficient} ";
                tempText += "* ";
                if (targetFunction.GetRefListCoefficient[indexBasis] < 0)
                    tempText += "( ";
                tempText += $"{targetFunction.GetRefListCoefficient[indexBasis]} ";
                if (targetFunction.GetRefListCoefficient[indexBasis] < 0)
                    tempText += ") ";
            }
            tempText += $"= {tempDelta}\r\n";
            listDelta.Add((float)tempDelta);
            RecordStep("Вычисление дельт");
        }

        //3-й шаг. Изабвляемся от отрицательных свободных коэффициентов
        private bool HaveNegativeFreeMembers()
        {
            int indexNegativeFunction;
            int indexNegativeCoefficient;
            while (true)
            {
                indexNegativeFunction = -1;
                indexNegativeCoefficient = -1;
                tempText += "Ищем ограничение с максимальным по модулю отрицательным свободным коеффициентом:\r\n";
                GetIndexNegativeFunction(ref indexNegativeFunction);
                if (indexNegativeFunction != -1)
                {
                    tempText += $"Это {indexNegativeFunction + 1} ограничение со значением  " +
                        $"= {listLimitedFunction[indexNegativeFunction].FreeCoefficient}. " +
                        $"Теперь ищем максимальный отрицательный по модулю элемент этой строки:\r\n";
                    GetIndexNegativeCoefficient(ref indexNegativeCoefficient, indexNegativeFunction);
                    if (indexNegativeCoefficient != -1)
                    {
                        tempText += $"Это {indexNegativeCoefficient + 1} коеффициент " +
                            $"со значением = {listLimitedFunction[indexNegativeFunction].GetRefListCoefficient[indexNegativeCoefficient]}." +
                            $" Поделим строку на этот элемент. Получим:\r\n";
                        listLimitedFunction[indexNegativeFunction].MultiplicationData(
                            1 / listLimitedFunction[indexNegativeFunction].GetRefListCoefficient[indexNegativeCoefficient]);
                        PrintLimitedFunction(indexNegativeFunction);
                        tempText += $"И из остальных строк вычтем текущую, умноженную на соответствующее значение в ранее найденном столбце. Получим:\r\n";
                        SubtractionFunction(indexNegativeFunction, indexNegativeCoefficient);
                        for (int indexLimitedFunction = 0; indexLimitedFunction < listLimitedFunction.Count; indexLimitedFunction++)
                            PrintLimitedFunction(indexLimitedFunction);
                        listLimitedFunction[indexNegativeFunction].Basis = indexNegativeCoefficient + 1;
                        tempText += $"И установим новый базис этого ограничения = x{listLimitedFunction[indexNegativeFunction].Basis}";
                    }
                    else
                    {
                        tempText += "Нет решений. У функции с отрицательным свободным коэффициентом все остальные коэффициенты положительные!";
                        RecordStep("Избавляемся от отрицательных свободных коэффициентов");
                        return true;
                    }
                }
                else
                    break;
            }
            tempText += "Таких ограничений нет, а значит, идём дальше)";
            RecordStep("Избавляемся от отрицательных свободных коэффициентов");
            return false;
        }
        private void GetIndexNegativeCoefficient(ref int indexNegativeCoefficient, int indexNegativeFunction)
        {
            float maxNegativeCoefficient = -1;
            int tempIndexCoefficient = 0;
            float tempValue;
            foreach (float coefficient in listLimitedFunction[indexNegativeFunction].GetRefListCoefficient)
            {
                if (coefficient < 0)
                {
                    tempValue = Math.Abs(coefficient);
                    if (tempValue > maxNegativeCoefficient)
                    {
                        maxNegativeCoefficient = tempValue;
                        indexNegativeCoefficient = tempIndexCoefficient;
                    }
                }
                tempIndexCoefficient++;
            }
        }
        private void GetIndexNegativeFunction(ref int indexNegativeFunction)
        {
            float maxNegativeFunction = -1;
            float temp;
            for (int indexFunction = 0; indexFunction < listLimitedFunction.Count; indexFunction++)
            {
                temp = listLimitedFunction[indexFunction].FreeCoefficient;
                if (temp < 0)
                {
                    temp = Math.Abs(temp);
                    if (temp > maxNegativeFunction)
                    {
                        maxNegativeFunction = temp;
                        indexNegativeFunction = indexFunction;
                    }
                }
            }
        }

        //2-й шаг. Формирование базиса
        private bool FormationInitialBasis()
        {
            for (int indexFunction = 0; indexFunction < listLimitedFunction.Count; indexFunction++)
            {
                if (listLimitedFunction[indexFunction].Basis == 0)
                {
                    tempText += $"Найдём базис для {indexFunction + 1} ограничения:\r\n";
                    if (!GetBasis(indexFunction))
                        return false;
                    RecordStep("Формирование базиса");
                }
                else
                    tempText += $"Базис {indexFunction + 1} ограничения: X{listLimitedFunction[indexFunction].Basis}\r\n";
            }
            return true;
        }
        private bool GetBasis(int indexFunction)
        {
            bool basisIsFound = false;
            int countCoefficient = countVariable + countNewCoefficientAsBasis; //Всего коеффициентов, с учетом новосозданных при приведении к каноническому виду
            float coefficient;
            tempText += "Сначала попробуем найти столбец, в котором все ячейки, кроме нашей будут равны 0:\r\n";
            for (int indexCoefficient = 0; indexCoefficient < countCoefficient; indexCoefficient++)
            {
                coefficient = listLimitedFunction[indexFunction].GetRefListCoefficient[indexCoefficient];
                if (coefficient != 0)
                {
                    if (CheckZeroCoefficient(indexFunction, indexCoefficient))
                    {
                        tempText += $"Это {indexCoefficient + 1} столбец.\r\n" +
                            $"Значит базис нашего ограничения: X{indexCoefficient + 1}.\r\n";
                        listLimitedFunction[indexFunction].Basis = indexCoefficient + 1;
                        basisIsFound = true;
                        tempText += $"Теперь поделим нашу строку на число ({coefficient}) в ячейке, образованной пересечением нашей строки с найденным столбцом.\r\n";
                        listLimitedFunction[indexFunction].MultiplicationData(1 / coefficient);
                        tempText += "Тогда наше ограничение выглядит следующим образом:\r\n";
                        PrintLimitedFunction(indexFunction);
                        break;
                    }
                }
            }

            if (basisIsFound.Equals(false))
            {
                tempText += "Такой столбец не найден, а значит, ищем ненулевой столбец:\r\n";
                for (int indexCoefficient = 0; indexCoefficient < countCoefficient; indexCoefficient++)
                {
                    if (CheckNotZero(indexCoefficient) && CheckNotBasis(indexCoefficient + 1))
                    {
                        tempText += $"Это {indexCoefficient + 1} столбец.\r\n";
                        listLimitedFunction[indexFunction].Basis = indexCoefficient + 1;
                        tempText += $"Теперь поделим нашу строку на число ({listLimitedFunction[indexFunction].GetRefListCoefficient[indexCoefficient]}) " +
                            $"в ячейке, образованной пересечением нашей строки с найденным столбцом.\r\n";
                        listLimitedFunction[indexFunction].MultiplicationData(1 / listLimitedFunction[indexFunction].GetRefListCoefficient[indexCoefficient]);
                        tempText += "И вычтем из остальных строк текущую, умноженную на значение в соответсвующей ячейке\r\n";
                        SubtractionFunction(indexFunction, indexCoefficient);
                        basisIsFound = true;
                        break;
                    }
                }
            }
            if (basisIsFound.Equals(false))
                return false;
            for (int indexLimitedFunction = 0; indexLimitedFunction < listLimitedFunction.Count; indexLimitedFunction++)
                PrintLimitedFunction(indexLimitedFunction);
            return true;
        }
        private void AddZeroCoefficient()
        {
            int allCoefficient = countVariable + countNewCoefficientAsBasis;
            for (int indexFunction = 0; indexFunction < listLimitedFunction.Count; indexFunction++)
            {
                while (listLimitedFunction[indexFunction].GetRefListCoefficient.Count < allCoefficient)
                {
                    listLimitedFunction[indexFunction].AddCoefficient(0);
                }
            }

            while (targetFunction.GetRefListCoefficient.Count < allCoefficient)
                targetFunction.AddCoefficient(0);
        }
        private void SubtractionFunction(int indexFunction, int indexCoefficient)
        {
            float coefficient;
            int indexFunctionNow = 0;
            int countAllCoefficient = countVariable + countNewCoefficientAsBasis;
            foreach (LimitedFunction function in listLimitedFunction)
            {
                if (indexFunctionNow != indexFunction)
                {
                    coefficient = function.GetRefListCoefficient[indexCoefficient];
                    for (int index = 0; index < countAllCoefficient; index++)
                        function.GetRefListCoefficient[index] -= coefficient * listLimitedFunction[indexFunction].GetRefListCoefficient[index];
                    function.FreeCoefficient -= coefficient * listLimitedFunction[indexFunction].FreeCoefficient;
                }
                indexFunctionNow++;
            }
        }
        private bool CheckNotBasis(int indexCoefficient)
        {
            foreach (LimitedFunction function in listLimitedFunction)
            {
                if (function.Basis == indexCoefficient)
                    return false;
            }
            return true;
        }
        private bool CheckNotZero(int indexCoefficient)
        {
            foreach (LimitedFunction function in listLimitedFunction)
            {
                if (function.GetRefListCoefficient[indexCoefficient] == 0)
                    return false;
            }
            return true;
        }
        private bool CheckZeroCoefficient(int indexBasis, int indexCoefficient)
        {
            float coefficient;
            for (int indexLimitedFucntion = 0; indexLimitedFucntion < listLimitedFunction.Count; indexLimitedFucntion++)
            {
                if (indexLimitedFucntion != indexBasis)
                {
                    coefficient = listLimitedFunction[indexLimitedFucntion].GetRefListCoefficient[indexCoefficient];
                    if (coefficient == 0)
                        continue;
                    return false;
                }
            }
            return true;
        }
        private void PrintLimitedFunction(int indexFunction)
        {
            int indexCoefficicent = 0;
            LimitedFunction function = listLimitedFunction[indexFunction];
            List<float> list = function.GetRefListCoefficient;
            tempText += $"{list[0]}x{indexCoefficicent+1} ";
            for(indexCoefficicent = 1; indexCoefficicent < list.Count; indexCoefficicent++)
            {
                if (list[indexCoefficicent] >= 0)
                    tempText += "+ ";
                tempText += $"{list[indexCoefficicent]}x{indexCoefficicent+1} ";
            }
            tempText += $"= {function.FreeCoefficient}\r\n";
        }

        //1-й шаг. Канонический вид
        private void GetCanonicalView()
        {
            bool greaterOrEqual = false;
            bool lessOrEqual = false;

            int indexLimitedFunction = 0;
            foreach (LimitedFunction function in listLimitedFunction)
            {
                if (function.SignFunction.Equals("≥"))
                {
                    greaterOrEqual = true;
                    listLimitedFunction[indexLimitedFunction].MultiplicationData(-1);
                    lessOrEqual = true;
                    LessOrEqual(indexLimitedFunction);
                }
                else if (function.SignFunction.Equals("≤"))
                {
                    lessOrEqual = true;
                    LessOrEqual(indexLimitedFunction);
                }
                indexLimitedFunction++;
            }
            if (greaterOrEqual == true)
                tempText += $"Меняем знаки у ограничений с '≥', путём умножения на -1.\r\n\r\n";
            if (lessOrEqual == true)
            {
                tempText += $"Для каждого ограничения с неравенством добавляем дополнительные переменные:\r\n";
                foreach (LimitedFunction function in listLimitedFunction)
                    if (function.Basis != 0)
                        tempText += $"x{function.Basis}\r\t";
                tempText += "\r\nИ меняем знак на '='\r\n";
            }
            AddZeroCoefficient();
            if (tempText.Length > 0)
                tempText += "Ограничения в каноническом виде выглядят следующим образом:\r\n";

            else
                tempText += "Уже в каноническом виде, никаких дополнительных преобразований не требуется.\r\n";

            for (int indexFunction = 0; indexFunction < listLimitedFunction.Count; indexFunction++)
                PrintLimitedFunction(indexFunction);

            RecordStep("Приведение к каноническому виду");
        }
        private void LessOrEqual(int indexLimitedFunction)
        {
            countNewCoefficientAsBasis++;
            int allCoefficient = countVariable + countNewCoefficientAsBasis;
            while (listLimitedFunction[indexLimitedFunction].GetRefListCoefficient.Count + 1 != allCoefficient)
                listLimitedFunction[indexLimitedFunction].AddCoefficient(0);
            listLimitedFunction[indexLimitedFunction].AddCoefficient(1);

            listLimitedFunction[indexLimitedFunction].Basis = allCoefficient;
            listLimitedFunction[indexLimitedFunction].SignFunction = "=";
        }

        //Запись шагов
        private void RecordStep(string nameForm)
        {
            countForm++;
            TextForm textForm = new TextForm(this, countForm - 1);
            textForm.Text = nameForm;
            listForm.Add(textForm);
            countForm++;
            DataForm dataForm = new DataForm(this, countForm - 1);
            listForm.Add(dataForm);
            tempText = "";
        }

        //Получение данных из функций
        private bool GetTargetFunction()
        {
            foreach (Control control in pnl_TargetFunction.Controls)
            {
                if (control is TextBox)
                {
                    float result = 0;
                    if (GetDecimal(control.Text, ref result))
                    {
                        targetFunction.AddCoefficient(result);
                        control.BackColor = Color.White;
                    }
                    else
                    {
                        control.BackColor = Color.Red;
                        return false;
                    }
                }
                else if (control is ComboBox)
                {
                    targetFunction.MaxMin = control.Text;
                }
            }
            return true;
        }
        private bool GetLimitedFunction()
        {
            int countControl = 0;
            int indexLimitedFunctionNow = 0;
            int countVariableNow = 0; // Количество переменных для каждой ограничительной функции

            float result = 0;

            listLimitedFunction.Add(new LimitedFunction());

            foreach (Control control in pnl_LimitedFunction.Controls)
            {
                countControl++;
                if (control is TextBox)
                {
                    if (GetDecimal(control.Text, ref result)) //TODO: добавить фокус на пустой textboox в для избавления от TryParse
                    {
                        if (countVariableNow == countVariable)
                        {
                            listLimitedFunction[indexLimitedFunctionNow].FreeCoefficient = result;
                            if (countControl < pnl_LimitedFunction.Controls.Count)
                            {
                                listLimitedFunction.Add(new LimitedFunction());
                                indexLimitedFunctionNow++;
                                countVariableNow = 0;
                            }
                        }
                        else
                        {
                            listLimitedFunction[indexLimitedFunctionNow].AddCoefficient(result);
                            countVariableNow++;
                        }
                        control.BackColor = Color.White;
                    }
                    else
                    {
                        control.BackColor = Color.Red;
                        return false;
                    }
                }
                else if (control is ComboBox)
                {
                    listLimitedFunction[indexLimitedFunctionNow].SignFunction = control.Text;
                }

            }
            return true;
        }
        private bool GetDecimal(string text, ref float result)
        {
            int index = text.IndexOf("/");
            if (index > -1)
            {
                if (index == text.Length - 1)
                {
                    MessageBox.Show("Неправильно записана дробь!");
                    return false;
                }
                else
                {
                    float firstNum;
                    float secondNum;
                    if (float.TryParse(text.Substring(0, index), NumberStyles.Any, CultureInfo.InvariantCulture, out firstNum))
                        if (float.TryParse(text.Substring(index + 1), NumberStyles.Any, CultureInfo.InvariantCulture, out secondNum) && secondNum != 0)
                        {
                            result = firstNum / secondNum;
                            return true;
                        }
                        else
                        {
                            MessageBox.Show("Ошибка числа после '/'!");
                            return false;
                        }
                    else
                    {
                        MessageBox.Show("Ошибка числа перед '/'!");
                        return false;
                    }
                }
            }
            else
            {
                if (float.TryParse(text, NumberStyles.Any, CultureInfo.InvariantCulture, out result))
                    return true;
                else
                {
                    MessageBox.Show("Неправильно записана дробь!");
                    return false;
                }
            }
        }
        public void ClearDateFromFunction()
        {
            foreach (Form form in listForm)
                form.Dispose();
            foreach (var function in listLimitedFunction)
                function.GetRefListCoefficient.Clear();

            listForm.Clear();
            listLimitedFunction.Clear();
            targetFunction.GetRefListCoefficient.Clear();
            countNewCoefficientAsBasis = 0;
            listDelta.Clear();
            tempText = "";
            countForm = 0;
        }

        //Динамическое создание функций
        private void GenerateTargetFunction()
        {
            if (int.TryParse(txt_CountVariable.Text, out countVariable) && int.TryParse(txt_CountLimited.Text, out countLimited))
            {
                pnl_TargetFunction.Refresh();
                pnl_TargetFunction.Controls.Clear();

                //Подбор высоты строк из-за наличия verticalScroll
                if (countVariable > 6)
                {
                    tblLP_Main.RowStyles[0].Height = 40F;
                    tblLP_Main.RowStyles[1].Height = 60F;
                }
                else
                {
                    tblLP_Main.RowStyles[0].Height = 32.5F;
                    tblLP_Main.RowStyles[1].Height = 67.5F;
                }

                //Динамическая отрисовка основной части ЦЕЛЕВОЙ функции
                int x = 17, y = 17, countNow;
                for (countNow = 1; countNow < countVariable; countNow++)
                {
                    pnl_TargetFunction.Controls.Add(GenerateTextBox(ref x, y));
                    pnl_TargetFunction.Controls.Add(GenerateXn(ref x, y, countNow));
                    pnl_TargetFunction.Controls.Add(GeneratePlus(ref x, y));
                }

                //Дорисовка конца Целевой функции
                pnl_TargetFunction.Controls.Add(GenerateTextBox(ref x, y));
                pnl_TargetFunction.Controls.Add(GenerateXn(ref x, y, countNow));
                pnl_TargetFunction.Controls.Add(GenerateArrow(ref x, y));
                pnl_TargetFunction.Controls.Add(GenerateComboBoxMinMax(ref x, y));

                GenerateLimitedFunction();
            }
        }
        private void GenerateLimitedFunction()
        {
            if (int.TryParse(txt_CountLimited.Text, out countLimited) && int.TryParse(txt_CountVariable.Text, out countVariable))
            {
                pnl_LimitedFunction.Refresh();
                pnl_LimitedFunction.Controls.Clear();

                int x = 17, y = 17, countVariableNow;
                for (int countLimitedNow = 0; countLimitedNow < countLimited; countLimitedNow++)
                {
                    x = 17;
                    for (countVariableNow = 1; countVariableNow < countVariable; countVariableNow++)
                    {
                        pnl_LimitedFunction.Controls.Add(GenerateTextBox(ref x, y));
                        pnl_LimitedFunction.Controls.Add(GenerateXn(ref x, y, countVariableNow));
                        pnl_LimitedFunction.Controls.Add(GeneratePlus(ref x, y));
                    }
                    pnl_LimitedFunction.Controls.Add(GenerateTextBox(ref x, y));
                    pnl_LimitedFunction.Controls.Add(GenerateXn(ref x, y, countVariableNow));
                    pnl_LimitedFunction.Controls.Add(GenerateComboBoxSign(ref x, y));
                    pnl_LimitedFunction.Controls.Add(GenerateTextBox(ref x, y));
                    y += 43; //Новая строка
                }
            }
        }

        //Шаблоны динамически создаваемых элементов
        private TextBox GenerateTextBox(ref int x, int y)
        {
            TextBox text = new TextBox
            {
                Location = new Point(x, y),
                Size = new Size(55, 27),
                MaxLength = 6,
                Margin = new System.Windows.Forms.Padding(0),
                TextAlign = HorizontalAlignment.Center,
                Text = "0",
                ShortcutsEnabled = false
            };
            text.KeyPress += PressDecimal;
            text.Enter += SelectText_Enter;

            return text;
        }
        private Label GenerateXn(ref int x, int y, int countNow)
        {
            y += 4;
            x += 55;
            Label label = new Label
            {
                Location = new Point(x, y),
                Size = new Size(24, 20),
                Margin = new System.Windows.Forms.Padding(0),
                TextAlign = ContentAlignment.MiddleCenter,
                Text = "x" + countNow.ToString()
            };
            return label;
        }
        private Label GeneratePlus(ref int x, int y)
        {
            y++;
            x += 24;
            Label label = new Label
            {
                Location = new Point(x, y),
                Size = new Size(22, 23),
                TextAlign = ContentAlignment.MiddleCenter,
                Text = "+",
                Font = new Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Bold)
            };
            x += 25;
            return label;
        }
        private Label GenerateArrow(ref int x, int y)
        {
            x += 24;
            Label label = new Label()
            {
                Location = new Point(x, y),
                Size = new Size(29, 23),
                TextAlign = ContentAlignment.MiddleCenter,
                Text = "->",
                Font = new Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Bold)
            };
            x += 30;
            return label;
        }
        private ComboBox GenerateComboBoxMinMax(ref int x, int y)
        {
            y -= 1;
            ComboBox combo = new ComboBox()
            {
                Items = { "max", "min" },
                Location = new Point(x, y),
                Size = new Size(58, 28),
                SelectedItem = "max",
                DropDownStyle = ComboBoxStyle.DropDownList
            };
            return combo;
        }
        private ComboBox GenerateComboBoxSign(ref int x, int y)
        {
            x += 30;
            ComboBox combo = new ComboBox()
            {
                Items = { "≤", "=", "≥" },
                Location = new Point(x, y),
                Size = new Size() { Width = 40 },
                Font = new Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold),
                SelectedItem = "≤",
                DropDownStyle = ComboBoxStyle.DropDownList,
                Margin = new System.Windows.Forms.Padding(0)
            };
            x += 55;
            return combo;
        }

        //Проверки корректности ввода
        private void PressOnlyDigit(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) || e.KeyChar.Equals('0'))
            {
                e.Handled = true;
            }
        }
        private void PressDecimal(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar) && e.KeyChar != '-' && e.KeyChar != '.' && e.KeyChar != '/')
                e.Handled = true;
            if ((e.KeyChar == '-') && (sender as TextBox).Text.Length >= 1)
                e.Handled = true;
            if (e.KeyChar == '.')
            {
                string text = (sender as TextBox).Text;
                if (text.IndexOf('.') > -1 || text.Length == 0 || text.IndexOf('/') > -1)
                    e.Handled = true;
                else
                    if (text[text.Length - 1].Equals('-') || text[text.Length - 1].Equals('/'))
                    e.Handled = true;
            }
            if (e.KeyChar == '/')
            {
                string text = (sender as TextBox).Text;
                if (text.IndexOf('/') > -1 || text.Length == 0 || text.IndexOf('.') > -1)
                    e.Handled = true;
                else
                    if (text[text.Length - 1].Equals('-') || text[text.Length - 1].Equals('.'))
                    e.Handled = true;
            }

            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar) && e.KeyChar != '-' && e.KeyChar != '.' && e.KeyChar != '/')
                e.Handled = true;
            if ((e.KeyChar == '-') && (sender as TextBox).Text.Length >= 1)
                e.Handled = true;
            if (e.KeyChar == '.')
            {
                string text = (sender as TextBox).Text;
                if (text.IndexOf('.') > -1 || text.Length == 0 || text.IndexOf('/') > -1)
                    e.Handled = true;
                else
                    if (text[text.Length - 1].Equals('-') || text[text.Length - 1].Equals('/'))
                    e.Handled = true;
            }
            if (e.KeyChar == '/')
            {
                string text = (sender as TextBox).Text;
                if (text.IndexOf('/') > -1 || text.Length == 0 || text.IndexOf('.') > -1)
                    e.Handled = true;
                else
                    if (text[text.Length - 1].Equals('-') || text[text.Length - 1].Equals('.'))
                    e.Handled = true;
            }
        }
        //Выделение текста
        private void SelectText_Enter(object sender, EventArgs e)
        {
            (sender as TextBox).SelectionStart = 0;
            (sender as TextBox).SelectionLength = (sender as TextBox).Text.Length;
        }

        //События
        private void txt_CountVariable_TextChanged(object sender, System.EventArgs e)
        {
            GenerateTargetFunction();
        }
        private void txt_CountLimited_TextChanged(object sender, System.EventArgs e)
        {
            GenerateLimitedFunction();

        }
        private void btn_Solve_Click(object sender, System.EventArgs e)
        {
            string flag = "false";
            if (GetTargetFunction() && GetLimitedFunction())
            {
                GetCanonicalView();
                if (FormationInitialBasis())
                {
                    if (HaveNegativeFreeMembers() == false)
                    {
                        while (true)
                        {
                            CalculatingDelta();
                            flag = CheckOptimality();
                            RecordStep("Проверка на оптимальность");
                            if (flag.Equals("true"))
                            {
                                PrintResult();
                                break;
                            }
                            GetOptimality(ref flag);
                            if (flag.Equals("error"))
                            {
                                MessageBox.Show("Целевая функция не ограничена и решения не существует");
                                break;
                            }
                        }
                    }
                    else
                        MessageBox.Show("Нет решений. У функции с отрицательным свободным коэффициентом все остальные коэффициенты положительные!");
                }
                else
                {
                    flag = "inputException";
                    MessageBox.Show("Неверно введенные данные. Не возможно найти базис для пустого ограничеия со знаком '='!");
                }

            }
            if (listForm.Count > 0 && !flag.Equals("inputException"))
            {
                DialogResult dialogResult = MessageBox.Show("Показать решение?", "", MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.Yes)
                    listForm[0].Visible = true;
                else
                    ClearDateFromFunction();
            }
            else
                ClearDateFromFunction();

        }

    }
}
