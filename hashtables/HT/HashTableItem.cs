using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hashtables.HT {
    public class HashTableItem {

        private string key;
        private object value;

        public HashTableItem(string key, object value) {
            this.key = key;
            this.value = value;
        }

        public string GetKey() {
            return key;
        }

        public object GetValue() {
            return value;
        }

        public void SetValue(object value) {
            this.value = value;
        }

    }
}
