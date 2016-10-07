using System;

namespace hashtables.HTBT {

    //Класс элемента хеш-таблицы
    class HashTableItemBT : IComparable {

        private string key;         //Ключ
        private object value;       //Значение



        //Конструктор
        public HashTableItemBT(string key) {
            this.key = key;
        }

        //Конструктор
        public HashTableItemBT(string key, object value) {
            this.key = key;
            this.value = value;
        }



        //Возвращает ключ
        public string GetKey() {
            return key;
        }

        //Возвращает значение
        public object GetValue() {
            return value;
        }

        //Устанавливает значение
        public void SetValue(object value) {
            this.value = value;
        }



        //Сравнивает объекты по ключу
        public int CompareTo(object value) {
            return key.CompareTo((value as HashTableItemBT).GetKey());
        }

        //Проверяет равенство объектов по ключу
        public override bool Equals(object value) {
            return CompareTo(value) == 0;
        }

    }
}
