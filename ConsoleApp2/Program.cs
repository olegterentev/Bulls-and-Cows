using System;

namespace ConsoleApp2
{
    //создадим новый класс с названием game, в нем будут основные функции с игрой.
    class game
    {
        uint N = 4;
        public bool check(int num)
        {
            //представим число в виде строки, и пройдемся по строке через for ища повторяющиеся символы. 
            string a = num.ToString();
            for (int i = 0; i < a.Length; i++)
            {
                for (int j = i + 1; j < a.Length; j++)
                {
                    if (a[i] == a[j])
                    {
                        return true;
                    }
                }
            }
            if(a.Length != N)
            {
                return true;
            }
            return false;
        }
        public (int, int) summ(int num, int user_num)
        {
            string num_a = num.ToString();
            string num_b = user_num.ToString();
            int cow = 0, bull = 0;
            //найдем количество быков и коров
            for (int i = 0; i < N; i++)
            {
                ///если элемент одной строки соответсвует элементу на этой же позииции в другой, мы нашли быка 
                if (num_a[i] == num_b[i])
                {
                    bull += 1;
                }
                ///для ловли коров воспользовался методом Contains для поиска подстроки в строке 
                ///он возвращает переменную типа bool
                else if (num_b.Contains(num_a[i]))
                {
                    cow += 1;
                }
            }
            return (cow, bull);

        }
        public int Random_number()
        {
            ///функция создающее раномное число с нужным количеством знаков.
            Random rand = new Random();
            int output;
            do
            {
                //с помощью math.pow(10, n-1) мы получаем минимальное n значное число, а с math.pow(10, n) максимальное.
                int a = (int)Math.Pow(10, N - 1);
                int b = (int)Math.Pow(10, N);
                output = rand.Next(a, b);
                //проверка полученного числа в функции check().
            } while (check(output));

            return output;
        }
        public void setting()
        {   
            //функция изменяет N.
            Console.Clear();
            Console.WriteLine("вы перешли в настройки");
            Console.WriteLine("введите количество цифр которое вы хотите угадывать:");
            while (true)
            {
                if (!uint.TryParse(Console.ReadLine(), out N))
                {
                    Console.WriteLine("Ошибка ввода! Пожалуйста введите целое, положительное число");
                    continue;
                }
                else if(N==0 || N>9)
                {
                    Console.WriteLine("Ошибка ввода! введите целое число в диапазоне от 1 до 9");
                    continue;
                }
                else
                {
                    break;
                }
            }
            Console.WriteLine($"Вы поменяли значение N на {N}");
            Console.ReadKey();
            Console.Clear();
        }
        public void info()
        {
            Console.Clear();
            Console.WriteLine("ВЫ ПЕРЕШЛИ В РАЗДЕЛ С ИНФОРМАЦИЕЙ.");
            Console.WriteLine("В данной игре вам придется угадать загаданное компьютером число.");
            Console.WriteLine("Для помощи в этом придуманны коровы и быки.");
            Console.WriteLine("Корова - угаданная цифра, стоящая не на своем месте.");
            Console.WriteLine("Бык - угаднная цифра, стоящая на свом месте.");
            Console.WriteLine("После каждого введенного вами числа вам будет показано количество коров и быков, которое вы угадали.");
            Console.WriteLine("Чтобы изменить количество цифр(по стандарту 4) в загаданном числе перейдите в режим setting");
            Console.ReadKey();
            Console.Clear();
            
        }
        public void start_game()
        {
            Console.Clear();
            ///не обязательный switch просто так красивее. можно было написать:
            switch (N)
            {
                case 1:
                    Console.WriteLine($"вы начили игру с {N}-но значным числом");
                    break;
                case 2:
                case 3:
                case 4:
                    Console.WriteLine($"вы начили игру с {N}-х значным числом");
                    break;
                case 5:
                case 6:
                    Console.WriteLine($"вы начили игру с {N}-ти значным числом");
                    break;
                case 7:
                case 8:
                case 9:
                    Console.WriteLine($"вы начили игру с {N}-ми значным числом");
                    break;
            }
            //создаю рандомное число подробнее в функции
            int num = Random_number();

            uint user_num;
            while (true)
            {
                Console.WriteLine("введите число:");
                while (true)
                {
                    ///через uint.tryParse проверяю целое положительное
                    ///с помощью check проверяю, что нет повторяющихся цифр в веденном числе
                    ///первый if проверяет не нулевой первый символ
                    string input = Console.ReadLine();
                    if (input[0] == '0')
                    {
                        Console.WriteLine("некоректный ввод, убедитесь в правильности введеных данных, и введите еще раз.");
                    }
                    else if (uint.TryParse(input, out user_num) && !check((int)user_num))
                    {
                        break;
                    }
                    else
                    {
                        Console.WriteLine("некоректный ввод, убедитесь в правильности введеных данных, и введите еще раз.");
                    }
                    
                }
                (int, int) ans = summ(num, (int)user_num);

                if (ans.Item2 == N)
                {
                    Console.WriteLine("Поздравляю, вы прошли игру.");
                    Console.ReadKey();
                    Console.Clear();
                    return;
                }
                else
                {
                    Console.WriteLine($"количество коров = {ans.Item1}, количество быков = {ans.Item2}");
                    Console.WriteLine("");
                }
            }
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Привет, ты запустил игру \"Быки и коровы\"");
            //создаем переменную класса game который описывает игру
            game gm = new game();
            while (true)
            {
                bool flag = false;
                Console.WriteLine("Для начала игры введите \"START\"");
                Console.WriteLine("Для изменения параметров игры введите \"SETTING\"");
                Console.WriteLine("Для дополнительной информации об игре введите \"INFO\"");
                Console.WriteLine("Для выхода из игры введите \"EXIT\"");
                Console.WriteLine("ввод допускается в любом регистре");
                //считывание данных, функция tolower позволяет не учитывать регистр
                do {
                    string input = Console.ReadLine().ToLower();
                    switch (input)
                    {
                        case "start":
                            flag = false;
                            gm.start_game();
                            break;
                        case "setting":
                            flag = false;
                            gm.setting();
                            break;
                        case "info":
                            flag = false;
                            gm.info();
                            break;
                        case "exit":
                            return;
                        default:
                            flag = true;
                            Console.WriteLine("");
                            Console.WriteLine("Ошибка ввода! Пожалуйста проверьте правильность введеных данных, и введите еще раз");
                            break;
                    }
                } while (flag);

            }
        }
    }
}
