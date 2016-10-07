using hashtables.HTBT;
using System;
using System.IO;

namespace hashtables {
    class Program {

        //Тестирование бинарного дерева
        static void TestBinaryTree() {
            BinaryTree<HashTableItemBT> bt = new BinaryTree<HashTableItemBT>();

            bt.Add(new HashTableItemBT("abc", 123));
            bt.Add(new HashTableItemBT("def", 456));
            bt.Add(new HashTableItemBT("hgk", 789));
            bt.Add(new HashTableItemBT("zxc", 234));
            bt.Add(new HashTableItemBT("abc", 345));

            foreach (HashTableItemBT item in bt)
                Console.WriteLine(item.GetKey() + " " + item.GetValue().ToString());
        }

        static void Main(string[] args) {
            //Подгружаем файл
            Console.WriteLine("Введите путь к файлу");
            string path = Console.ReadLine();

            if (!File.Exists(path)) {
                Console.WriteLine("Файл не существует");
                return;
            }

            //Читаем файл
            string[] lines = File.ReadAllLines(path);

            //Выводим заголовки таблицы
            Console.WriteLine("\nРазмер хеш-таблицы\tЗначений\tСравнений\tКоллизий");
            Console.WriteLine(new String('-', 64));

            //Генерируем хеш-таблицы с разный размером
            for (int size = 1; size <= lines.Length; size++) {

                HashTableBT htbt = new HashTableBT(size);

                //Добавляем значения в хе-таблицу
                for (int i = 0; i < lines.Length; i++) {
                    string[] values = lines[i].Split(' ');
                    htbt.Set(values[0], values[1]);
                }

                //Получаем значения по всем ключам и считаем среднее количество сравнений
                double comparingCount = 0;
                string[] keys = htbt.GetKeys();
                foreach (string key in keys) {
                    htbt.Get(key);
                    comparingCount += htbt.ComparingCount;
                }
                comparingCount /= keys.Length;

                //Получаем количество коллизий
                double collision = htbt.GetAverageCollisionCount();

                //Выводим данные в консоль
                Console.WriteLine(String.Format(
                    "{0}\t\t\t{1}\t\t{2}\t\t{3}",
                    size,
                    lines.Length,
                    Math.Round(comparingCount, 2),
                    Math.Round(collision, 2)
                ));
            }

            Console.ReadKey();
        }
    }
}
