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
            Console.Write("Please enter 1, 2, 3, 4, 5, or 6: ");
            int caseSwitch = Convert.ToInt32(Console.ReadLine());

            using (var context = new MoviesManagementEntities())
            {
                switch(caseSwitch)
                {
                    case 1:
                        var actors = (from a in context.ACTORs
                                      join c in context.CASTS on a.id equals c.pid
                                      join m in context.MOVIEs on c.mid equals m.id
                                      where m.name == "Officer 444"
                                      select new
                                      {
                                          firstName = a.fname,
                                          lastName = a.lname
                                      });
                        Console.WriteLine("Question 1");
                        Console.WriteLine("Lengh of result: " + actors.Count());
                        foreach (var item in actors)
                        {
                            Console.WriteLine("First Name: {0}, Last Name: {1}", item.firstName, item.lastName);
                        }
                        break;

                    case 2:
                        var directors = context.getDirectors().ToList();
                        Console.WriteLine("Question 2");
                        Console.WriteLine("Lengh of result: " + directors.Count());
                        foreach (var item in directors)
                        {
                            Console.WriteLine("Director's Firstname: {0}, Director's Lastname: {1}, The number of movie: {2} ", item.lname, item.lname, item.the_number_of_movies);
                        }
                                        
                        break;

                    case 3:
                        var results = context.getActors().ToList();

                        //var results = (from a in context.ACTORs
                        //              join c in context.CASTS on a.id equals c.pid
                        //              join m in context.MOVIEs on c.mid equals m.id
                        //              where m.year_movie == 2010
                        //              group new { m, a, c } by new { m.name, a.fname, a.lname, c.role_cast } into ag
                        //              where ag.Key.role_cast.Count() >= 5
                        //              select new
                        //              {
                        //                  actorName = ag.Key.fname + " " + ag.Key.lname,
                        //                  movieName = ag.Key.name,
                        //                  rolesInTheSameMovie = ag.Key.role_cast.Distinct().Count()
                        //              });

                        Console.WriteLine("Question 3");
                        Console.WriteLine("Lengh of result: " + results.Count());
                        foreach (var item in results)
                        {
                            Console.WriteLine("Actor Name: {0}, Movie Name: {1}, Total roles: {2}", item.actor_name, item.movie_name, item.roles_in_the_same_movie);
                        }
                        break;

                    case 4:
                        Console.WriteLine("Insert");
                        var actor = new ACTOR()
                        {
                            id = 000,
                            fname = "Michael",
                            gender = "M"
                        };
                        context.ACTORs.Add(actor);
                        context.SaveChanges();
                        break;

                    case 5:
                        Console.WriteLine("Update");
                        var act = context.ACTORs.SingleOrDefault(a => a.fname == "Michael");
                        if (act != null)
                        {
                            act.lname = "Fores";
                            context.SaveChanges();
                        }
                        break;

                    case 6:
                        Console.WriteLine("Delete");
                        var rmv = context.ACTORs.SingleOrDefault(a => a.fname == "Michael");
                        if (rmv != null)
                        {
                            context.ACTORs.Remove(rmv);
                            context.SaveChanges();
                        }
                        break;

                }
                Console.ReadLine();
            }
        }
    }
}
