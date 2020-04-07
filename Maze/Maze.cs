using System;
using System.Collections.Generic;
using System.Text;

namespace MazeStructure
{
    class Maze
    {
        /// <summary>
        /// Матрица отображающая лабиринт.
        /// </summary>
        private int[,] mazeStruct;

        /// <summary>
        /// Размер матрицы. Всегда нечетное.
        /// </summary>
        private int size;

        /// <summary>
        /// Кординаты старта.
        /// </summary>
        private int startX;
        private int startY;

        /// <summary>
        /// Кординаты финиша.
        /// </summary>
        private int finishX;
        private int finishY;

        /// <summary>
        /// Матрица отображающая лабиринт.
        /// </summary>
        public int[,] MazeStruct { get { return mazeStruct; } }

        /// <summary>
        /// Создание круглого лабиринта.
        /// </summary>
        /// <param name="mazeSize"></param>
        public Maze(int mazeSize)
        {
            size = mazeSize + 2; // Добавляем буфер.

            startX = (size - 1) / 2; // Центр
            startY = 2;

            finishX = (size - 1) / 2; // Центр
            finishY = (size - 1) / 2; // Центр

            CreatePlatform();
            StepOfMaze(finishX, finishY);
        }



        /// <summary>
        /// Создание круглой матрицы.
        /// </summary>
        private void CreatePlatform()
        {
            bool flag;
            if (mazeStruct == null) mazeStruct = new int[size, size];

            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    flag = Math.Pow(i - (size - 1) / 2, 2) +
                           Math.Pow(j - (size - 1) / 2, 2) <=
                           Math.Pow((size - 4) / 2, 2);

                    mazeStruct[i, j] = flag? 0 : 7;
                }
            }
        }



        /// <summary>
        /// Рекурсия для нахождения старта. Ввести кардинаты начала поиска.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        private bool FindTheStartRecusrion  (int x, int y)
        {
            if (x == startX && y == startY) return true;
            if (mazeStruct[x - 1, y] != 0 && mazeStruct[x + 1, y] != 0 &&
                mazeStruct[x, y - 1] != 0 && mazeStruct[x, y + 1] != 0) return false;

            if (mazeStruct[x, y] == 0) mazeStruct[x, y] = 3;

            if (mazeStruct[x - 1, y] == 0) { if (FindTheStartRecusrion(x - 1, y)) return true; }
            if (mazeStruct[x + 1, y] == 0) { if (FindTheStartRecusrion(x + 1, y)) return true; }
            if (mazeStruct[x, y - 1] == 0) { if (FindTheStartRecusrion(x, y - 1)) return true; }
            if (mazeStruct[x, y + 1] == 0) { if (FindTheStartRecusrion(x, y + 1)) return true; }

            return false;
        }

        /// <summary>
        /// Зачистка площандки после нахождения пути.
        /// </summary>
        private void CleanMaze(int index)
        {
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    if (mazeStruct[i, j] == index) mazeStruct[i, j] = 0;
                }
            }
        }



        /// <summary>
        /// Выставление стенок лабиринта в зависимости от напрвления.
        ///             0
        ///             |
        ///    2 --- точка --- 1
        ///             |
        ///             3
        /// 
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="direction"></param>
        /// <param name="flag"></param>
        /// <returns></returns>
        private bool [] DoStepOfMaze(int x, int y, int direction, bool[] flag )
        {
            if (mazeStruct[x, y] == 0) { mazeStruct[x, y] = 4; flag[4] = true; }

            if (mazeStruct[x - 1, y] == 0 && direction != 0 ) { mazeStruct[x - 1, y] = 1; flag[0] = true; }
            if (mazeStruct[x, y + 1] == 0 && direction != 1 ) { mazeStruct[x, y + 1] = 1; flag[1] = true; }
            if (mazeStruct[x, y - 1] == 0 && direction != 2 ) { mazeStruct[x, y - 1] = 1; flag[2] = true; }
            if (mazeStruct[x + 1, y] == 0 && direction != 3 ) { mazeStruct[x + 1, y] = 1; flag[3] = true; }

            return flag; //Возвращаем тот же масив что и грали. Не надо заново присваивать.
        }

        /// <summary>
        /// Отмена стенок в зависимости от напрвления.
        ///             0
        ///             |
        ///    2 --- точка --- 1
        ///             |
        ///             3
        /// 
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="direction"></param>
        /// <param name="flag"></param>
        /// <returns></returns>
        private bool[] UnDoStepOfMaze(int x, int y, int direction, bool[] flag)
        {
            if (mazeStruct[x, y] == 4 && flag[4]) { mazeStruct[x, y] = 0; flag[4] = false; }

            if (mazeStruct[x - 1, y] == 1 && direction != 0 && flag[0]) { mazeStruct[x - 1, y] = 0; flag[0] = false; }
            if (mazeStruct[x, y + 1] == 1 && direction != 1 && flag[1]) { mazeStruct[x, y + 1] = 0; flag[1] = false; }
            if (mazeStruct[x, y - 1] == 1 && direction != 2 && flag[2]) { mazeStruct[x, y - 1] = 0; flag[2] = false; }
            if (mazeStruct[x + 1, y] == 1 && direction != 3 && flag[3]) { mazeStruct[x + 1, y] = 0; flag[3] = false; }

            return flag; //Возвращаем тот же масив что и брали. Не надо заново присваивать.
        }


        /// <summary>
        /// Пошаговое построение лабиринта.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        private void StepOfMaze(int x, int y)
        {
            // Рандом для направления.
            Random randomazer = new Random();
            var direct = randomazer.Next(3);

            // Счетчик испробованных напрвлений.
            var count = 0;

            // Флаги. Был ли изменен соседний элимент.
            bool[] flag = new bool[5] { false, false, false, false, false };

            do
            {
                // Выбор ноправления.
                switch (direct)
                {
                    case 0: // Верх.
                        if (mazeStruct[x - 1, y] == 0)
                        {
                            // Строим предположение.
                            DoStepOfMaze(x, y, 0, flag);
                            // Проверяем есть ли путь до старта.
                            if (FindTheStartRecusrion(x, y))
                            {
                                // Чистим поле после проверки.
                                CleanMaze(3); 
                                // Есть путь? Тогда снова функцию.
                                StepOfMaze(x - 1, y);
                            }
                            else
                            {
                                // Чистим поле после проверки.
                                CleanMaze(3);
                                // Убираем преположение.
                                UnDoStepOfMaze(x, y, 0, flag); 
                                // Крутим направление.
                                direct=1;
                            }
                            break;
                        }
                        else direct=1;
                        break;
                    case 1: // Право.
                        if (mazeStruct[x, y + 1] == 0)
                        {
                            DoStepOfMaze(x, y, 1, flag);
                            if (FindTheStartRecusrion(x, y))
                            {
                                CleanMaze(3);
                                StepOfMaze(x, y + 1);
                            }
                            else
                            {
                                CleanMaze(3);
                                UnDoStepOfMaze(x, y, 1, flag);
                                direct=2;
                            }
                            break;
                        }
                        else direct=2;
                        break;

                    case 2: // Лево.
                        if (mazeStruct[x, y - 1] == 0)
                        {
                            DoStepOfMaze(x, y, 2, flag);
                            if (FindTheStartRecusrion(x, y))
                            {
                                CleanMaze(3);
                                StepOfMaze(x, y - 1);
                            }
                            else
                            {
                                CleanMaze(3);
                                UnDoStepOfMaze(x, y, 2, flag);
                                direct = 3;
                            }
                            break;
                        }
                        else direct =3;
                        break;

                    case 3: // Низ.
                        if (mazeStruct[x + 1, y] == 0)
                        {
                            DoStepOfMaze(x, y, 3, flag);
                            if (FindTheStartRecusrion(x, y))
                            {
                                CleanMaze(3);
                                StepOfMaze(x + 1, y);
                            }
                            else
                            {
                                CleanMaze(3);
                                UnDoStepOfMaze(x, y, 3, flag);
                                direct = 0;
                            }
                            break;
                        }
                        else direct=0;
                        break;
                }
                // Когда последовательность приходит в тупик(или начало) начинается бесконечный цикл Do и UnDo.
                // Если циклов было больше 3 -  значит все направления перебрали и функция попала в тупик.
                count++;
                if (count > 3) break;

            } while (true);
        }



        /// <summary>
        /// Отображение лабирианта в консоли. 
        /// </summary>
        public void ShowMaze()
        {
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    Console.Write(" " + mazeStruct[i,j]);
                }
                Console.WriteLine();
            }
        }

        /// <summary>
        /// Отображение лабирианта с символами вместо цифр.
        /// </summary>
        public void ShowGoodLookMaze()
        {
            string [,] stringMaze = new string[size, size];


            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    if (mazeStruct[i, j] == 7) stringMaze[i, j] = ".";
                    if (mazeStruct[i, j] == 1) stringMaze[i, j] = "|";
                    if (mazeStruct[i, j] == 4) stringMaze[i, j] = "a";
                    if (mazeStruct[i, j] == 0) stringMaze[i, j] = "H";
                    stringMaze[(size - 1) / 2, (size - 1) / 2] = "0";
                    stringMaze[startX, startY] = "0";
                    Console.Write($"{stringMaze[i, j],3}");
                }
                Console.WriteLine();
            }


        }


    }
}
