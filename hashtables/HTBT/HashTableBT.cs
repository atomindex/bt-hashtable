namespace hashtables.HTBT {

    //Класс хеш-таблицы с разрешением коллизий с помощью бинарного дерева
    public class HashTableBT {

        private BinaryTree<HashTableItemBT>[] table;        //Массив бинарных деревьев
        private int size;                                   //Общее количество значений в таблице
        private int comparingCount;                         //Количество сравнений при поиске значения



        //Свойство для получения количества значений
        public int ComparingCount {
            get { return comparingCount; }
        }



        //Конструктор
        public HashTableBT(int size) {
            table = new BinaryTree<HashTableItemBT>[size];
        }



        //Хеш-функция
        public int hash(string key) {
            if (key.Length == 0)
                return 0;
            if (key.Length == 1)
                return (int)key[0] % table.Length;
            return (key[key.Length - 1] + key[key.Length - 2]) % table.Length;
        }



        //Добавление значения
        public void Set(string key, object value) {
            int h = hash(key);

            //Создаем бинарное дерево если его нет
            if (table[h] == null)
                table[h] = new BinaryTree<HashTableItemBT>();

            //Ищем значение в бинарном дереве, если его нет, то добавляем, иначе обновляем значение
            BinaryTreeNode<HashTableItemBT> node = table[h].Find(new HashTableItemBT(key));
            if (node == null) {
                table[h].Add(new HashTableItemBT(key, value));
                size++;
            } else
                node.Value.SetValue(value);
        }

        //Возвращает значение по ключу или null
        public object Get(string key) {
            int h = hash(key);

            comparingCount = 1;

            if (table[h] == null)
                return null;

            BinaryTreeNode<HashTableItemBT> node = table[h].Find(new HashTableItemBT(key));
            comparingCount += table[h].ComparingCount;

            return node == null ? null : node.Value.GetValue();
        }

        //Удаляет значение по ключу
        public void Remove(string key) {
            int h = hash(key);

            if (table[h] == null)
                return;

            if (table[h].Remove(new HashTableItemBT(key))) {
                size--;
                if (table[h].Count == 0)
                    table[h] = null;
            }
        }



        //Возвращает массив всех значений
        public object[] GetValues() {
            object[] result = new object[size];

            int i = 0;
            foreach (BinaryTree<HashTableItemBT> htItem in table) {
                if (htItem == null) continue;
                foreach (HashTableItemBT btItem in htItem) {
                    result[i] = btItem.GetValue();
                    i++;
                }
            }

            return result;
        }

        //Возвращает массив всех ключей
        public string[] GetKeys() {
            string[] result = new string[size];

            int i = 0;
            foreach (BinaryTree<HashTableItemBT> htItem in table) {
                if (htItem == null) continue;
                foreach (HashTableItemBT btItem in htItem) {
                    result[i] = btItem.GetKey();
                    i++;
                }
            }

            return result;
        }



        //Возвращает среднее число коллизий
        public double GetAverageCollisionCount() {
            int count = 0;
            foreach (BinaryTree<HashTableItemBT> htItem in table) {
                if (htItem == null) continue;
                count += htItem.Count - 1;
            }
            return (double)count / table.Length;
        }

    }
}
