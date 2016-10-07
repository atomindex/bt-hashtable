using System;

namespace hashtables.HTBT {

    //Класс узла бинарного дерева
    public class BinaryTreeNode<T> where T : IComparable {

        public T Value;                         //Значение узла

        public BinaryTree<T> Tree;              //Дерево

        public BinaryTreeNode<T> Parent;        //Родитель
        public BinaryTreeNode<T> LeftChild;     //Левый потомок
        public BinaryTreeNode<T> RightChild;    //Правый потомок



        //Конструктор
        public BinaryTreeNode(T value) {
            Value = value;
        }



        //Определяет, является ли узел листовым
        public bool IsLeaf() {
            return GetChildCount() == 0;
        }

        //Определяет является ли узел родителем
        public bool IsInternal() {
            return GetChildCount() > 0;
        }

        //Определяет, является ли узел левым потомком
        public bool IsLeftChild() {
            return Parent != null && Parent.LeftChild == this;
        }

        //Определяет, является ли узел правым потомком
        public bool IsRightChild() {
            return Parent != null && Parent.RightChild == this;
        }



        //Возвращает количество потомков
        public int GetChildCount() {
            int count = 0;

            if (LeftChild != null)
                count++;

            if (RightChild != null)
                count++;

            return count;
        }

        //Определяет наличие левого потомка
        public bool HasLeftChild() {
            return LeftChild != null;
        }

        //Определяет наличие правого потомка
        public bool HasRightChild() {
            return RightChild != null; 
        }

    }

}
