using System;

namespace WeirdCalculator
{
    class Program
    {
        static void Main(string[] args)
        {
            XDAO xDAO = new XDAO();
            YDAO yDAO = new YDAO();
            ResultDAO resultDAO = new ResultDAO();
            int x=-1;
            int y=-1;
            while(x!=0 & y!=0)
            {
                Console.WriteLine("enter x");
                x = int.Parse(Console.ReadLine());
                Console.WriteLine("enter y");
                y = int.Parse(Console.ReadLine());

                xDAO.Insert(x);
                yDAO.Insert(y);
            }

            resultDAO.Cross();
            resultDAO.Calculate();
        }
    }
}
