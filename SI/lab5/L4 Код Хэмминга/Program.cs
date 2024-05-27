using System;

namespace lab6
{
    class Program
    {
        static void Main(string[] args)
        {
            string Xk = "11010001011101000010000001";
            string Xr = "100101";

            int k = Xk.Length;
            int n = 31;
            int r = 5;

            int error;

            int[] masXk = new int[k];
            Operations.StringInArray(masXk, Xk);

            int[] masXr = new int[Xr.Length];
            Operations.StringInArray(masXr, Xr);

            Console.WriteLine("\tИсходное сообщение: " + Xk);
            Console.WriteLine("\tПораждающий полином в бинарном виде: " + Xr);
            Console.WriteLine("\t k = {0}, r = {1}, n = {2}", k, r, n);
            Console.WriteLine("------------------------------------------------------");

            int[,] generationMatrix = new int[k, n];
            Operations.CreateGenerationMatrix(generationMatrix, masXr, k, n);

            Console.WriteLine("\n\tПорождающая матрица:\n");
            Operations.PrintMatrix(generationMatrix, k, n);

            Operations.CreateMatrixCanon(generationMatrix, k, n);
            Console.WriteLine();

            Console.WriteLine("\nПорождающая матрица в каноническом виде:\n");
            Operations.PrintMatrix(generationMatrix, k, n);

            int[,] checkMatrix = new int[n, r];
            Operations.CreateMatrixForCheck(checkMatrix, generationMatrix, k, n);
            Console.WriteLine();

            Console.WriteLine("\nПроверочная матрица:\n");
            Operations.PrintMatrix(checkMatrix, n, r);

            int[] masXn = new int[n];
            Operations.ShiftR(masXn, masXk, r);

            Console.WriteLine();

            Console.WriteLine("\n\tДеление:\n");
            Operations.SearchRes(masXn, masXr);

            Console.WriteLine("Остаток от деления:");
            Operations.PrintArray(masXn);

            Console.WriteLine();
            Console.WriteLine("\n\tКодовое слово:\n");
            Operations.ShiftR(masXn, masXk, r);
            Operations.PrintArray(masXn);

            Console.WriteLine();
       
           
          

            try
            {
                Console.WriteLine("Введите позицию ошибки:");
                error = Convert.ToInt32(Console.ReadLine()) - 1;
                if (masXn[error] == 1) masXn[error] = 0;
                else masXn[error] = 1;
            }
            catch { }

            Console.WriteLine("Строка с ошибкой:");
            Operations.PrintArray(masXn);

            Operations.SearchingMistake(masXn, masXr, checkMatrix, r);

            Console.WriteLine("--------------------------------------------------------------------------------");
            try
            {
                Console.WriteLine("Введите позицию первой ошибки: ");
                error = Convert.ToInt32(Console.ReadLine()) - 1;
                if (masXn[error] == 1)
                    masXn[error] = 0;
                else masXn[error] = 1;

                Console.WriteLine("Введите позицию второй ошибки: ");
                error = Convert.ToInt32(Console.ReadLine()) - 1;
                if (masXn[error] == 1)
                    masXn[error] = 0;
                else masXn[error] = 1;
            }
            catch { }

            Console.WriteLine("Строка с ошибкой:");
            Operations.PrintArray(masXn);

            Operations.SearchingMistake(masXn, masXr, checkMatrix, r);

          
        }
    }
}