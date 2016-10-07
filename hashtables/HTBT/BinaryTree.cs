using System;
using System.Collections;
using System.Collections.Generic;

namespace hashtables.HTBT {

    //Типы обхода дерева
    public enum TraversalMode {
        InOrder = 0,
        PostOrder,
        PreOrder
    }


    //Класс бинарного дерева
    public class BinaryTree<T> : ICollection<T> where T : IComparable {

        private BinaryTreeNode<T> root;         //Корень дерева
        private int size;                       //Количество элементов
        private int comparingCount;             //Количество сравнений при последнем поиске

        public TraversalMode TraversalOrder = TraversalMode.InOrder; //Тип обхода дерева

        
        //Реализация абстрактного свойства
        public bool IsReadOnly {
            get { return false; }
        }

        //Корень дерева
        public BinaryTreeNode<T> Root {
            get { return root; }
        }

        //Количество элементов
        public int Count {
            get { return size; }
        }

        //Количество сравнений при последнем поиске
        public int ComparingCount {
            get { return comparingCount; }
        }



        //Добавляет элемент
        public void Add(T value) {
            Add(new BinaryTreeNode<T>(value));
        }

        //Добавляет узел
        public void Add(BinaryTreeNode<T> node) {
            if (root == null) {
                
                //Добавляем первый узел
                root = node;
                node.Tree = this;
                size++;

            } else {

                if (node.Parent == null)
                    node.Parent = root;

                //Узел меньше или равен чем родитель
                bool insertLeftSide = node.Value.CompareTo(node.Parent.Value) <= 0;

                if (insertLeftSide) {
                    //Если у родителя не установлен левый потомок
                    if (node.Parent.LeftChild == null) {
                        //Делаем текущий узел левым потомком
                        node.Parent.LeftChild = node;
                        size++;
                        node.Tree = this;
                    } else {
                        //Устанавливаем левый узел в качестве родителя, и рекурсивно вызываем добавление
                        node.Parent = node.Parent.LeftChild;
                        Add(node);
                    }
                } else {
                    //Если у родителя не установлен правый потомок
                    if (node.Parent.RightChild == null) {
                        //Делаем текущий узел правым потомком
                        node.Parent.RightChild = node;
                        size++;
                        node.Tree = this;
                    } else {
                        //Устанавливаем правый узел в качестве родителя, и рекурсивно вызываем добавление
                        node.Parent = node.Parent.RightChild;
                        Add(node);
                    }
                }

            }
        }



        //Поиск узла по значению
        public BinaryTreeNode<T> Find(T value) {
            BinaryTreeNode<T> node = root;
            comparingCount = 0;

            while (node != null) {
                comparingCount++;

                //Если текущий узел равен запрошенному
                if (node.Value.Equals(value))
                    return node;
                else {
                    //Запрошенный узел меньше чем текущий
                    bool searchLeft = value.CompareTo(node.Value) < 0;
                    node = searchLeft ? node.LeftChild : node.RightChild; 
                }
            }

            return null;
        }

        //Определяет наличие значения в дереве
        public bool Contains(T value) {
            return Find(value) != null;
        }



        //Удаляет узел со значением, возвращает true при удачном удалении
        public bool Remove(T value) {
            return Remove(Find(value));
        }

        //Удаляет узел, возвращает true при удачном удалении
        public bool Remove(BinaryTreeNode<T> removeNode) {
            if (removeNode == null || removeNode.Tree != this)
                return false;

            bool wasHead = removeNode == root;

            if (Count == 1) {

                //Удаление единственного узла
                root = null;
                removeNode.Tree = null;
                size--;

            } else if (removeNode.IsLeaf()) {

                //Удаление листового узла
                if (removeNode.IsLeftChild())
                    removeNode.Parent.LeftChild = null;
                else
                    removeNode.Parent.RightChild = null;

                removeNode.Tree = null;
                removeNode.Parent = null;

                size--; 

            } else if (removeNode.GetChildCount() == 1) {

                //Если только один потомок, меняем его родителя
                if (removeNode.HasLeftChild()) {
                    removeNode.LeftChild.Parent = removeNode.Parent;

                    if (wasHead)
                        root = removeNode.LeftChild;

                    if (removeNode.IsLeftChild())
                        removeNode.Parent.LeftChild = removeNode.LeftChild;
                    else
                        removeNode.Parent.RightChild = removeNode.LeftChild;
                } else {
                    removeNode.RightChild.Parent = removeNode.Parent;

                    if (wasHead)
                        root = removeNode.RightChild;

                    if (removeNode.IsLeftChild())
                        removeNode.Parent.LeftChild = removeNode.RightChild;
                    else
                        removeNode.Parent.RightChild = removeNode.RightChild;
                }

                removeNode.Tree = null;
                removeNode.Parent = null;
                removeNode.LeftChild = null;
                removeNode.RightChild = null;

                size--;

            } else {

                //Если 2 потомка, то заменяем удаляемый узел на крайний левый в правой ветке
                BinaryTreeNode<T> successorNode = removeNode.LeftChild;
                while (successorNode.RightChild != null)
                    successorNode = successorNode.RightChild;

                removeNode.Value = successorNode.Value;

                Remove(successorNode);

            }

            return true;
        }

        //Очистка дерева
        public void Clear() {
            IEnumerator<T> enumerator = GetPostOrderEnumerator();
            while (enumerator.MoveNext())
                Remove(enumerator.Current);

            enumerator.Dispose();
        }



        //Возвращает высоту дерева
        public int GetHeight() {
            return GetHeight(root);
        }

        //Возвращает высоту значения
        public int GetHeight(T value) {
            BinaryTreeNode<T> valueNode = Find(value);
            if (value != null)
                return GetHeight(valueNode);
            else
                return 0;
        }

        //Возвращае высоту узла
        public int GetHeight(BinaryTreeNode<T> startNode) {
            if (startNode == null)
                return 0;
            else
                return 1 + Math.Max(GetHeight(startNode.LeftChild), GetHeight(startNode.RightChild));
        }



        //Возвращает глубину значения
        public int GetDepth(T value) {
            return GetDepth(Find(value));
        }

        //Возвращает глубину узла
        public int GetDepth(BinaryTreeNode<T> startNode) {
            int depth = 0;

            if (startNode == null)
                return depth;

            BinaryTreeNode<T> parentNode = startNode.Parent;
            while (parentNode != null) {
                depth++;
                parentNode = parentNode.Parent;
            }

            return depth;
        }



        //Возвращает перечислитель взависимости от настроек
        public IEnumerator<T> GetEnumerator() {
            switch (TraversalOrder) {
                case TraversalMode.InOrder:
                    return GetInOrderEnumerator();
                case TraversalMode.PostOrder:
                    return GetPostOrderEnumerator();
                case TraversalMode.PreOrder:
                    return GetPreOrderEnumerator();
                default:
                    return GetInOrderEnumerator();
            }
        }

        //Возвращает перечислитель взависимости от настроек
        IEnumerator IEnumerable.GetEnumerator() {
            return GetEnumerator();
        }

        //Возвращает InOrder перечеслитель
        public IEnumerator<T> GetInOrderEnumerator() {
            return new BinaryTreeInOrderEnumerator<T>(this);
        }

        //Возвращает PostOrder перечеслитель
        public IEnumerator<T> GetPostOrderEnumerator() {
            return new BinaryTreePostOrderEnumerator<T>(this);
        }

        //Возвращает PreOrder перечеслитель
        public IEnumerator<T> GetPreOrderEnumerator() {
            return new BinaryTreePreOrderEnumerator<T>(this);
        }



        //Копирует бинарное дерево в массив
        public void CopyTo(T[] array, int startIndex) {
            IEnumerator<T> enumerator = GetEnumerator();

            for (int i = startIndex; i < array.Length; i++) {
                if (enumerator.MoveNext())
                    array[i] = enumerator.Current;
                else
                    break;
            }
        }

    }
}