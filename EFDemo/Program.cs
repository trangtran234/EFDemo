using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var db = new MoviesManagementEntities())
            {
                var actors = (from a in db.ACTORs
                              join c in db.CASTS on a.id equals c.mid
                              join m in db.MOVIEs on c.pid equals m.id
                              where m.name == "Officer 444"
                              select new
                              {
                                  firstName = a.fname,
                                  lastName = a.lname
                              }).ToList();
                Console.WriteLine("Console here");
                foreach (var item in actors)
                {
                    Console.WriteLine("First Name: " + item.firstName);
                    Console.WriteLine("Last Name: " + item.lastName);

                }
            }
        }
    }
}
