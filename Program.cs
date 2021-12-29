namespace Graf
{
    class Program
    {
        static void Main(string[] args)
        {
            var graph = new List<List<List<int>>> { };
                        
            Console.Write("Сколько вершин есть у графа? ");

            int N = int.Parse(Console.ReadLine());

            for (int i = 0; i < N; i++)
            {
                graph.Add(new List<List<int>> { });

                Console.Write($"Сколько смежных с {i} вершин? ");

                int K = int.Parse(Console.ReadLine());

                for (int j = 0; j < K; j++)
                {
                    Console.WriteLine("Введите номер смежной вершины ");

                    int temp1 = int.Parse(Console.ReadLine());

                    Console.WriteLine("Введите расстояние до вершины");

                    int temp2 = int.Parse(Console.ReadLine());

                    graph[i].Add(new List<int> { temp1, temp2 });
                }
            }

            List<int> ex = new List<int> { };

            for (int i = 0; i < graph.Count(); i++)
            {
                ex.Add(0);
            }
            for (int i = 0; i < graph.Count(); i++)
            {
                Console.Write($"Вершина {i}: ");

                foreach (List<int> templ in graph[i])
                {
                    Console.Write($"{templ[0]}({templ[1]}) ");
                }

                Console.WriteLine();
            }

            Console.WriteLine("---------------------------");

            for (int i = 0; i < graph.Count(); i++)
            {
                Console.Write($" {i}");
            }

            Console.WriteLine();

            List<int> exok = new List<int> { };

            for (int i = 0; i < graph.Count(); i++)
            {
                Dictionary<int, int> temp = BreadthFirstSearch(graph, i);

                for (int j = 0; j < graph.Count(); j++)
                {

                    if (ex[j] != -1)
                    {
                        if (temp[j] == -1)
                        {
                            ex[j] = -1;
                        }

                        else
                        {
                            if (ex[j] < temp[j])
                            {
                                ex[j] = temp[j];
                            }
                        }
                    }
                }
            }

            Console.WriteLine("------------------------");

            foreach (int exx in ex)
            {
                if (exx != -1)
                {
                    exok.Add(exx);
                }
            }

            if (exok.Count() == 0)
            {
                Console.WriteLine("Невозможно найти радиус, так как у орграфа отсутствует центр");
            }

            else
            {
                List<int> center = new List<int> { };

                int rad = exok.Min();

                for (int i = 0; i < ex.Count(); i++)
                {
                    if (ex[i] == rad)
                        center.Add(i);
                }

                if (center.Count() == 1)
                {
                    Console.WriteLine($"Радиус орграфа равен {exok.Min()}, центр графа - вершина {center[0]}");

                    Console.WriteLine($"Внешний радиус графа равен {BreadthFirstSearch(graph, center[0]).Values.Max()}");
                }

                else
                {
                    int vnerad = 0;

                    Console.Write($"Радиус орграфа равен {exok.Min()}, центр графа - вершина ");

                    foreach (int elem in center)
                    {
                        Console.Write(elem + " ");

                        if (vnerad < BreadthFirstSearch(graph, elem).Values.Max())
                        {
                            vnerad = BreadthFirstSearch(graph, elem).Values.Max();
                        }
                    }

                    Console.WriteLine();
                    Console.WriteLine($"Внешний радиус графа равен {vnerad}");
                }
            }

            Console.ReadKey();
        }
        static Dictionary<int, int> BreadthFirstSearch(List<List<List<int>>> graph, int start)

        {
            var path = new Dictionary<int, int>();

            var S = new Dictionary<int, int>();

            for (int i = 0; i < graph.Count; i++)
            {
                path.Add(i, 0);
            }

            var queue = new Queue<int>();

            var visited = new List<bool>(new bool[graph.Count]);

            for (int i = 0; i < graph.Count(); i++)
            {
                S.Add(i, -1);
            }

            queue.Enqueue(start);

            visited[start] = true;

            int k = 0;

            S[start] = k;

            int oldv = -1;

            while (queue.Count > 0)
            {
                int v = queue.Dequeue();

                if ((v == start) || (S[v] != S[oldv]))
                {
                    k++;
                }

                foreach (List<int> templ in graph[v])
                {

                    int ver = templ[0];

                    if (path[ver] > (path[v] + templ[1]))
                    {
                        path[ver] = path[v] + templ[1];
                    }
                    
                    if (!visited[ver])
                    {
                        path[ver] = path[v] + templ[1];

                        queue.Enqueue(ver);

                        visited[ver] = true;

                        S[ver] = k;
                    }

                    oldv = v;
                }
            }

            for (int i = 0; i < path.Count(); i++)
            {
                if ((path[i] == 0) && (i != start))
                {
                    path[i]--;
                }
            }

            Console.WriteLine(start + " " + String.Join(" ", path.Values));

            return path;
        }
    }
}