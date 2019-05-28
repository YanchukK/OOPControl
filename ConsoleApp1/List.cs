using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    interface IDynamicArr<T>
    {
        void Add(T item);
        void Insert(int index, T item);
        void Remove(T item);
        void RemoveAt(int index);
        void Clear();
        bool Contains(T item);
        int IndexOf(T item);
        T[] ToArray();
        void Reverse();
    }

    class List<T> : IDynamicArr<T>
    {
        private T[] inner;

        private int сount;
        public int Count
        {
            get
            {
                return сount;
            }
            private set
            {
                if (value > 0)
                {
                    сount = value;
                }
            }
        }

        //индексатор
        public T this[int index]
        {
            get
            {
                return inner[index];
            }
            set
            {
                inner[index] = value;
            }
        }


        public void Add(T item)
        {
            if (inner == null)
            {
                inner = new T[] { item };
            }
            else
            {
                T[] newinner = new T[inner.Length + 1];

                int i = 0;
                while (i != (newinner.Length - 1))
                {
                    newinner[i] = inner[i];
                    i++;
                }

                newinner[newinner.Length - 1] = item;

                inner = newinner;
            }

            Count++;
        }

        public void Insert(int index, T item)
        {
            if (inner == null || inner.Length < index || index < 0)
            {
                Console.WriteLine("Ошибка");
            }
            else
            {
                T[] newinner = new T[inner.Length + 1];

                int i = 0;
                while (i != index - 1)
                {
                    newinner[i] = inner[i];
                    i++;
                }

                newinner[index - 1] = item;

                i = index - 1;
                while (i < newinner.Length - 1)
                {
                    newinner[i + 1] = inner[i];
                    i++;
                }

                inner = newinner;
            }
        }

        public void Remove(T item)
        {
            if (IndexOf(item) != -1)
            {
                RemoveAt(IndexOf(item));
            }
        }

        public void RemoveAt(int index)
        {
            T[] newinner = new T[inner.Length - 1];

            int i = 0;
            while (i != index)
            {
                newinner[i] = inner[i];
                i++;
            }

            i = index;
            while (i < newinner.Length)
            {
                newinner[i] = inner[i + 1];
                i++;
            }

            inner = newinner;
        }

        public int IndexOf(T item)
        {
            int index = -1;

            for (int j = 0; j < inner.Length; j++)
            {
                if (inner[j].Equals(item))
                {
                    index = j;
                    j = inner.Length;
                }
            }

            return index;
        }

        public void Clear()
        {
            T[] newinner = new T[0];

            inner = newinner;
        }

        public bool Contains(T item)
        {
            bool result = false;

            for (int i = 0; i < inner.Length; i++)
            {
                if (inner[i].Equals(item))
                {
                    result = true;
                }
            }

            return result;
        }

        public T[] ToArray()
        {
            T[] result = new T[this.Count];

            for (int i = 0; i < this.Count; i++)
            {
                result[i] = inner[i];
            }

            return result;
        }

        public void Reverse()
        {
            T[] result = new T[inner.Length];

            int i = 0;
            for (int j = inner.Length - 1; j >= 0; j--)
            {
                result[i] = inner[j];
                i++;
            }

            inner = result;
        }
    }
}