using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hashtables.HT {
    public class HashTable {

        private static HashTableItem removedItem;

        private HashTableItem[] table;
        private int size;

        static HashTable() {
            removedItem = new HashTableItem(null, null);
        }

        public HashTable(int size) {
            table = new HashTableItem[size];
        }

        private int hash(string key) {
            int h = 0;
            for (int i = 0; i < key.Length; i++)
                h += (int)key[i];
            h %= table.Length;
            return h;
        }

        private int nextHash(int h) {
            return (h + 1) % table.Length;
        }

        public void Set(string key, object value) {
            if (key == null)
                throw new ArgumentNullException();

            int h = hash(key);

            if (table[h] == null || table[h] == removedItem) {
                table[h] = new HashTableItem(key, value);
                size++;
                return;
            } else if (table[h].GetKey() == key) {
                table[h].SetValue(value);
                return;
            }

            int hi = nextHash(h);
            while (hi != h) {
                if (table[hi] == null || table[hi] == removedItem) {
                    table[hi] = new HashTableItem(key, value);
                    size++;
                    return;
                } else if (table[hi].GetKey() == key) {
                    table[hi].SetValue(value);
                    return;
                }
                hi = nextHash(hi);
            }

            throw new IndexOutOfRangeException();
        }

        public object Get(string key) {
            int h = hash(key);

            if (table[h] == null) return null;
            if (table[h].GetKey() == key)
                return table[h].GetValue();

            int hi = nextHash(h);
            while (table[hi] != null && hi != h) {
                if (table[hi].GetKey() == key)
                    return table[hi].GetValue();
                hi = nextHash(hi);
            }

            return null;
        }

        public void Remove(string key) {
            int h = hash(key);

            if (table[h] == null) return;

            if (table[h].GetKey() == key) {
                table[h] = removedItem;
                size--;
                return;
            }

            int hi = nextHash(h);
            while (table[hi] != null && hi != h) {
                if (table[hi].GetKey() == key) {
                    table[hi] = removedItem;
                    size--;
                    break;
                }
                hi = nextHash(hi);
            }
        }

        public object[] GetValues() {
            object[] result = new object[size];

            for (int i = 0, j = 0; i < table.Length; i++) {
                if (table[i] == null || table[i] == removedItem) continue;

                result[j++] = table[i].GetValue();
            }
            
            return result;
        }

        public string[] GetKeys() {
            string[] result = new string[size];

            for (int i = 0, j = 0; i < table.Length; i++) {
                if (table[i] == null || table[i] == removedItem) continue;

                result[j++] = table[i].GetKey();
            }

            return result;
        }

    }
}
