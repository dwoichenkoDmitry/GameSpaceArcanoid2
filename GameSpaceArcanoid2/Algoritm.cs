using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameSpaceArcanoid2
{
    public class Algoritm
    {
        public static SinglyLinkedList<Point> FindPaths(int width, int height, Point start, Point destination)
        {
            destination = ChangeDestination(start, destination);
            var way = new Dictionary<Point, SinglyLinkedList<Point>>();
            var visitedPoint = new HashSet<Point>();
            var crawlPoints = new Queue<Point>();
            crawlPoints.Enqueue(start);
            visitedPoint.Add(start);
            
            way.Add(start, new SinglyLinkedList<Point>(start));
            Console.WriteLine(start);
            Console.WriteLine(destination);
            while (crawlPoints.Count != 0 && !way.ContainsKey(destination))
            {
                var point = crawlPoints.Dequeue();
                
                if (destination.X > start.X && point.X < start.X || destination.X<start.X && point.X>start.X)
                    continue;
                if (destination.Y > start.Y && point.Y < start.Y || destination.Y < start.Y && point.Y > start.Y)
                    continue;
                if ((destination.X > start.X && point.X > destination.X ||
                    !(destination.X > start.X) && !(point.X > destination.X)) ||
                     (destination.Y > start.Y && point.Y > destination.Y ||
                    !(destination.Y > start.Y) && !(point.Y > destination.Y))) continue;
                
                Console.WriteLine(point);
                CheckPoint(way, crawlPoints, visitedPoint, point);
            }

            
             if (way.ContainsKey(destination))
            {
                return way[destination];
            }
            else
            {
                return null;
            }
            
        }

        public static void CheckPoint(Dictionary<Point, SinglyLinkedList<Point>> way, Queue<Point> crawlPoints, HashSet<Point> visitedPoint, Point point)
        {
            for (var dy = -10; dy <= 10; dy+=10)
                for (var dx = -10; dx <= 10; dx+=10)
                    if (dx != 0 && dy != 0) { continue; }
                    else
                    {
                        var newPoint = new Point { X = point.X + dx, Y = point.Y + dy };
                        if (visitedPoint.Contains(newPoint)) { continue; }

                        visitedPoint.Add(newPoint);
                        crawlPoints.Enqueue(newPoint);
                        way.Add(newPoint, new SinglyLinkedList<Point>(newPoint, way[point]));
                    }
        }

        public static Point ChangeDestination(Point start,Point destination)
        {
            int differenceY = (start.Y-destination.Y)/10;
            destination.Y = start.Y + differenceY * 10;
            int differenceX = (Math.Max(destination.X, start.X)-Math.Min(destination.X, start.X))/10;
            if (destination.X <= start.X) { destination.X = start.X - differenceX * 10; }
            else { destination.X = start.X + differenceX * 10; }
            return destination;
        }
    }
}