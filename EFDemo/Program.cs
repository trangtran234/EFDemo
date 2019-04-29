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
            using (var context = new MoviesManagementEntities())
            {
                int option;
                do
                {
                    Console.WriteLine("--------------------------------------------------------------------------------------------------------------------");
                    Console.WriteLine("MENU");
                    Console.WriteLine("--------------------------------------------------------------------------------------------------------------------");
                    Console.WriteLine("Enter 1: List the first and last names of all the actors who played in the movie 'Officer 444'.");
                    Console.WriteLine("Enter 2: List all directors who directed 500 movies or more.");
                    Console.WriteLine("Enter 3: Find actors that played five or more roles in the same movie during the year 2010.");
                    Console.WriteLine("Enter 0: Exit");
                    Console.WriteLine("--------------------------------------------------------------------------------------------------------------------");
                    Console.Write("Please enter your selection: ");
                    option = Convert.ToInt32(Console.ReadLine());
                    switch (option)
                    {
                        case 0: break;
                        case 1:
                            string expectedMovie = "Officer 444";
                            var actors = context.CASTS
                                .Join(context.ACTORs,
                                    ca => ca.pid,
                                    actor => actor.id,
                                    (ca, actor) => new { ca, actor })
                                .Join(context.MOVIE,
                                    cast => cast.ca.mid,
                                    movie => movie.id,
                                    (cast, movie) => new { cast, movie })
                                .Where(m => m.movie.name == expectedMovie)
                                .Select(s => new
                                {
                                    firstName = s.cast.actor.fname,
                                    lastName = s.cast.actor.lname
                                }).ToList();

                            Console.WriteLine("--------------------------------------------------------------------------------------------------------------------");
                            Console.WriteLine("OPTION 1:");
                            Console.WriteLine("Lengh of result: " + actors.Count());
                            Console.WriteLine("--------------------------------------------------------------------------------------------------------------------");
                            Console.WriteLine("{0,2} {1,-30} {2,-50}", "No.", "First Name", "Last Name");
                            foreach (var item in actors)
                            {
                                Console.WriteLine("{0,2}. {1,-30} {2,-50}",
                                    actors.IndexOf(item),
                                    item.firstName,
                                    item.lastName);
                            }
                            break;

                        case 2:
                            IList<DIRECTOR> directors = context.DIRECTORS.Select(d => d).ToList();
                            int counter = 0;
                            int index = 1;
                            Console.WriteLine("{0,2} {1,-30} {2,-50}", "No.", "Director's name", "Number of movies");
                            foreach (var item in directors)
                            {
                                if (item.MOVIEs.Distinct().Count() >= 500)
                                {
                                    Console.WriteLine("{0,2}. {1,-30} {2,-50}",
                                        index++,
                                        item.fname + " " + item.lname,
                                        item.MOVIEs.Count());
                                    counter++;
                                }
                            }
                            Console.WriteLine("--------------------------------------------------------------------------------------------------------------------");
                            Console.WriteLine("OPTION 2:");
                            Console.WriteLine("Lengh of result: " + counter);
                            Console.WriteLine("--------------------------------------------------------------------------------------------------------------------");
                            break;

                        case 3:
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
                                .GroupBy(g => new
                                {
                                    g.cast.actor.fname,
                                    g.cast.actor.lname,
                                    g.movie.name,
                                    g.cast.ca.role_cast
                                })
                                .Select(s => new
                                {
                                    actorName = s.Key.fname + " " + s.Key.lname,
                                    movieName = s.Key.name,
                                    numbersRole = s.Count(),
                                })
                                .ToList();

                            var result = query.GroupBy(g => new
                            {
                                g.actorName,
                                g.movieName,
                                g.numbersRole
                            })
                                .Where(w => w.Count() >= 5)
                                .Select(s => new
                                {
                                    s.Key.actorName,
                                    s.Key.movieName,
                                    numbersRole = s.Count()
                                }).ToList();
                            Console.WriteLine("--------------------------------------------------------------------------------------------------------------------");
                            Console.WriteLine("OPTION 3:");
                            Console.WriteLine("Lengh of result: {0}", result.Count());
                            Console.WriteLine("--------------------------------------------------------------------------------------------------------------------");

                            Console.WriteLine("{0,2} {1,-30} {2,-50} {3,-2}", "No.", "Actor Name", "Movie Name", "Number of role");
                            foreach (var item in result)
                            {
                                Console.WriteLine("{0,2}. {1,-30} {2,-50} {3,-2}",
                                    result.IndexOf(item),
                                    item.actorName,
                                    item.movieName,
                                    item.numbersRole);
                            }
                            break;
                    }
                } while (option != 0);
            }
        }
    }
}
