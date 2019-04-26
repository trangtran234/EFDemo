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
                                      join m in context.MOVIE on c.mid equals m.id
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
                        IList<DIRECTOR> directorList = context.DIRECTORS.Select(d => d).ToList();
                        int count = 0;
                        int index = 0;
                        foreach (var item in directorList)
                        {
                            if (item.MOVIEs.Distinct().Count() >= 500)
                            {
                                index = count + 1;
                                Console.WriteLine("{0}. Director's name: {1}, Number of movies: {2}", index, item.fname + " " + item.lname, item.MOVIEs.Count());
                                count++;
                            }
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
                        var query = context.CASTS
                            .Join(context.ACTORs,
                                ca => ca.pid,
                                actor => actor.id,
                                (ca, actor) => new { ca, actor })
                            .Join(context.MOVIE,
                                cast => cast.ca.mid,
                                movie => movie.id,
                                (cast, movie) => new { cast, movie })
                            .Where(m => m.movie.year_movie == 2010)
                            .GroupBy(c => new { c.cast.ca.role_cast, c.cast.actor.fname, c.cast.actor.lname, c.movie.name })
                            .Where(g => g.Count() > 5);
                            //.select(a => new
                            //{
                            //    actorname = a.key.fname + " " + a.key.lname,
                            //    moviename = a.key.fname,
                            //    number = a.key.role_cast.count()
                            //});

                        Console.WriteLine("Lengh: {0}", query.Count());

                        //foreach (var item in query)
                        //{
                        //    Console.WriteLine("Actor's name: {0}, Movie's name: {1}, Number: {2}", item.actorName, item.movieName, item.number);
                        //}
                        

                        //IList<ACTOR> actorExpectedList = new List<ACTOR>();
                        //foreach (var item in query)
                        //{
                        //    actorExpectedList.Add(context.ACTORs.Where(a => a.id == item.CAST.pid).FirstOrDefault());
                        //}

                        //int lengh = 0;
                        //IList<MOVIE> moviesOfActor = new List<MOVIE>();
                        //foreach (var item in actorExpectedList)
                        //{
                        //    int lenghMovie = item.CASTS.Count();
                        //    if (lenghMovie >= 5)
                        //    {
                        //        lengh++;
                        //        foreach (var element in item.CASTS)
                        //        {
                        //            moviesOfActor.Add(element.MOVIE);
                        //        }
                        //        Console.WriteLine("{0}. Actor's name: {1}, The number of distinct roles: {2}", lengh, item.fname + " " + item.lname, lenghMovie);
                        //        Console.WriteLine("Movie's name: ");
                        //        foreach (var movie in moviesOfActor)
                        //        {
                        //            Console.WriteLine("        + {0}", movie.name);
                        //        }
                        //    }
                        //}
                        //Console.WriteLine("===============================================================");
                        //Console.WriteLine("Lengh: {0}", lengh);

                        break;
                }
                Console.ReadLine();
            }
        }
    }
}
