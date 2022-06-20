using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snake
{
    internal class Snake : Figure
    {
        Direction direction;
        bool blockUp = false;
        bool blockDown = false;
        bool blockLeft = true;
        bool blockRight = false;
        
        public Snake(Point tail, int length, Direction _direction)
        {
            direction = _direction;
            pList = new List<Point>();

            for(int i = 0; i < length; i++)
            {
                Point p = new Point(tail);
                p.Move(i, direction);
                pList.Add(p);
            }
        }

        internal void Move()
        {
            Point tail = pList.First();
            pList.Remove(tail);
            Point head = GetNextPoint();
            pList.Add(head);

            tail.Clear();
            head.Draw();
        }

        public Point GetNextPoint()
        {
            Point head = pList.Last();
            Point nextPoint = new Point(head);
            nextPoint.Move(1, direction);
            return nextPoint;
        }

        public bool IsHitTail()
        {
            var head = pList.Last();
            for (int i = 0; i < pList.Count - 2; i++)
            {
                if (head.IsHit(pList[i]))
                {
                    return true;
                }
            }
            return false;
        }

        public void HandleKey(ConsoleKey key)
        {
            if (key == ConsoleKey.LeftArrow && !blockLeft)
            {
                blockUp = false;
                blockDown = false;
                blockLeft = false;
                blockRight = true;
                
                direction = Direction.left;
            }
            else if (key == ConsoleKey.RightArrow && !blockRight)
            {
                blockUp = false;
                blockDown = false;
                blockLeft = true;
                blockRight = false;

                direction = Direction.right;
            }
            else if (key == ConsoleKey.UpArrow && !blockUp)
            {
                blockUp = false;
                blockDown = true;
                blockLeft = false;
                blockRight = false;

                direction = Direction.up;
            }
            else if (key == ConsoleKey.DownArrow && !blockDown)
            {
                blockUp = true;
                blockDown = false;
                blockLeft = false;
                blockRight = false;

                direction = Direction.down;
            }
        }

        public bool Eat(Point food)
        {
            Point head = GetNextPoint();
            if (head.IsHit(food))
            {
                food.sym = head.sym;
                food.Draw();
                pList.Add(food);
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
